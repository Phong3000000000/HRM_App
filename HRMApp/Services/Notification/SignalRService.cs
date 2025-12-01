using HRMApp.Model.Notification;
using HRMApp.Services.Api;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRMApp.Services.Notification
{
    public interface ISignalRService
    {
        Task StartConnectionAsync();
        Task StopConnectionAsync();
        Task UpdateDeviceStatusAsync(string deviceId, bool isAppOpen);
        event Action<SignalRNotification> OnNotificationReceived;
        event Action<SignalRNotification> OnRealTimeNotification;
        bool IsConnected { get; }
        string ConnectionId { get; }

        /// <summary>
        /// Kích hoạt khi một thông báo bị SỬA (Update)
        /// </summary>
        event Action<SignalRNotification> OnNotificationUpdated;

        /// <summary>
        /// Kích hoạt khi một thông báo bị XÓA (Delete)
        /// </summary>
        event Action<Guid> OnNotificationDeleted;
    }

    public class SignalRService : ISignalRService
    {
        private HubConnection _hubConnection;
        private readonly ILocalApi _Api;
        private readonly string _hubUrl;
        private bool _isConnected;
        private string _connectionId = string.Empty;

        public event Action<SignalRNotification> OnNotificationReceived;
        public event Action<SignalRNotification> OnRealTimeNotification;

        // ✅ THÊM 2 EVENT MỚI
        public event Action<SignalRNotification> OnNotificationUpdated;
        public event Action<Guid> OnNotificationDeleted;

        public bool IsConnected => _isConnected && _hubConnection?.State == HubConnectionState.Connected;
        public string ConnectionId => _connectionId;

        public SignalRService(ILocalApi Api)
        {
            _Api = Api;


            //_hubUrl = "http://192.168.11.129:5162/publicnotificationhub";
            //_hubUrl = "https://vietstockapi.nguyenlethanhphong.io.vn/publicnotificationhub";
            //  _hubUrl = "https://3c6767c9a594.ngrok-free.app/publicnotificationhub";
            //_hubUrl = "https://iotapi.nguyenlethanhphong.io.vn/publicnotificationhub";
            _hubUrl = "http://192.168.1.40:5246/notificationHub";

            // ✅ THÊM: Log ngay khi khởi tạo
            Debug.WriteLine($"🔍 SignalRService constructor called");
            Debug.WriteLine($"🔍 Hub URL: {_hubUrl}");
            Debug.WriteLine($"🔍 API service: {(_Api != null ? "✅ OK" : "❌ NULL")}");

            InitializeConnection();
        }

        private void InitializeConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_hubUrl)
                .WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(30) })
                .Build();

            Debug.WriteLine(" Setting up SignalR listeners...");

            //  Lắng nghe sự kiện từ server với object
            _hubConnection.On<object>("ReceiveHRNotification", (message) =>
            {
                Debug.WriteLine($"Received HR notification: {message}");
                var notification = ParseNotification(message);
                if (notification != null)
                {
                    OnNotificationReceived?.Invoke(notification);
                }
            });


            _hubConnection.On<object>("ReceiveNotification", (message) =>
            {
                Debug.WriteLine($"Received notification: {message}");
                var notification = ParseNotification(message);
                if (notification != null)
                {
                    OnNotificationReceived?.Invoke(notification);
                }
            });

            _hubConnection.On<object>("ReceiveRealTimeNotification", (message) =>
            {
                Debug.WriteLine($" Received real-time notification: {message}");
                var notification = ParseNotification(message);
                if (notification != null)
                {
                    OnRealTimeNotification?.Invoke(notification);
                }
            });

            //  Thêm listener cho tất cả notification methods có thể từ server
            _hubConnection.On<object>("SendNotificationToDevice", (message) =>
            {
                Debug.WriteLine($" Received device notification: {message}");
                var notification = ParseNotification(message);
                if (notification != null)
                {
                    OnRealTimeNotification?.Invoke(notification);
                }
            });

            _hubConnection.On<string>("DeviceConnected", (deviceId) =>
            {
                Debug.WriteLine($" Device connected: {deviceId}");
            });

            _hubConnection.On<string>("DeviceDisconnected", (deviceId) =>
            {
                Debug.WriteLine($" Device disconnected: {deviceId}");
            });


            // ==========================================================
            // ✅ THÊM 2 LISTENER MỚI NÀY
            // ==========================================================

            // Lắng nghe sự kiện SỬA
            // Server gửi 'object' (giống như Create) nên chúng ta dùng ParseNotification
            _hubConnection.On<object>("ReceiveNotificationUpdate", (message) =>
            {
                Debug.WriteLine($"✅ SignalR: Received UPDATE");
                var notification = ParseNotification(message);
                if (notification != null)
                {
                    // Kích hoạt event mới
                    OnNotificationUpdated?.Invoke(notification);
                }
            });

            // Lắng nghe sự kiện XÓA
            // Server gửi 'Guid' (notificationId)
            _hubConnection.On<Guid>("ReceiveNotificationDelete", (notificationId) =>
            {
                Debug.WriteLine($"✅ SignalR: Received DELETE for {notificationId}");
                // Kích hoạt event mới
                OnNotificationDeleted?.Invoke(notificationId);
            });


            // Handle connection events
            _hubConnection.Reconnecting += (error) =>
            {
                Debug.WriteLine($" SignalR reconnecting: {error?.Message}");
                _isConnected = false;
                return Task.CompletedTask;
            };

            _hubConnection.Reconnected += async (connectionId) =>
            {
                Debug.WriteLine($" SignalR reconnected: {connectionId}");
                _isConnected = true;
                _connectionId = connectionId ?? string.Empty;

                // Không tự động cập nhật trạng thái khi kết nối lại
                // var deviceId = GetDeviceId();
                // await UpdateDeviceStatusAsync(deviceId, true);
            };

            _hubConnection.Closed += (error) =>
            {
                Debug.WriteLine($" SignalR connection closed: {error?.Message}");
                _isConnected = false;
                _connectionId = string.Empty;
                return Task.CompletedTask;
            };

            Debug.WriteLine(" SignalR listeners setup completed!");
        }

        //  Updated ParseNotification method for new SignalRNotification structure
        private SignalRNotification ParseNotification(object message)
        {
            try
            {
                Debug.WriteLine($" ParseNotification starting...");

                if (message == null)
                {
                    Debug.WriteLine(" Message is null");
                    return null;
                }

                Debug.WriteLine($" Message type: {message.GetType().FullName}");

                string jsonString = null;

                if (message is string str)
                {
                    jsonString = str;
                    Debug.WriteLine($" Approach 1 (string): {str}");
                }
                else
                {
                    jsonString = JsonSerializer.Serialize(message, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    Debug.WriteLine($" Approach 2 (serialize): {jsonString}");
                }

                if (string.IsNullOrEmpty(jsonString))
                {
                    Debug.WriteLine(" JSON string is empty");
                    return CreateFallbackNotification(message);
                }

                //  Try with flexible number handling
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString
                };

                var notification = JsonSerializer.Deserialize<SignalRNotification>(jsonString, options);

                if (notification != null)
                {
                    Debug.WriteLine($" Successfully parsed notification:");
                    Debug.WriteLine($"  Id: {notification.Id}");
                    Debug.WriteLine($"  UserId: {notification.UserId}");
                    Debug.WriteLine($"  Title: {notification.Title}");
                    Debug.WriteLine($"  Content: {notification.Content}");
                    Debug.WriteLine($"  Type: {notification.Type}");
                    Debug.WriteLine($"  CreatedAt: {notification.CreatedAt}");
                    Debug.WriteLine($"  IsRead: {notification.IsRead}");

                    return notification;
                }
                else
                {
                    Debug.WriteLine(" Deserialized to null");
                    return CreateFallbackNotification(message);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" ParseNotification exception: {ex.Message}");
                Debug.WriteLine($" Exception details: {ex}");
                return CreateFallbackNotification(message);
            }
        }

        //  Updated CreateFallbackNotification method for new SignalRNotification structure
        private SignalRNotification CreateFallbackNotification(object message)
        {
            Debug.WriteLine(" Creating fallback notification...");

            var fallback = new SignalRNotification
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Empty,
                Title = "Thông báo mới",
                Content = message?.ToString() ?? "Có thông báo mới",
                Type = "general",
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            Debug.WriteLine($" Fallback created: {fallback.Title} - {fallback.Content}");
            return fallback;
        }

        // ✅ SỬA LỖI: Đổi (false) thành (true)
        public async Task StartConnectionAsync()
        {
            await StartConnectionAsync(true); // <-- BÁO CHO SERVER BIẾT APP ĐANG MỞ
        }

        public async Task StartConnectionAsync(bool updateDeviceStatus = false)
        {
            try
            {
                // 1. Kết nối
                if (_hubConnection.State == HubConnectionState.Disconnected)
                {
                    await _hubConnection.StartAsync();
                    _isConnected = true;

                    // ✅ LẤY ID NGAY LẬP TỨC TỪ HUB
                    _connectionId = _hubConnection.ConnectionId;
                    Debug.WriteLine($"✅ SignalR Connected! ID: {_connectionId}");
                }

                // 2. Nếu yêu cầu update trạng thái
                if (updateDeviceStatus)
                {
                    var userId = await GetUserIdAsync();
                    if (!string.IsNullOrEmpty(userId))
                    {
                        // ✅ QUAN TRỌNG: Truyền _connectionId vừa lấy được vào đây
                        // Nếu _connectionId rỗng, hàm UpdateDeviceStatusAsync bên dưới sẽ dùng API fallback
                        await UpdateDeviceStatusAsync(userId, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ SignalR Start Error: {ex.Message}");
                _isConnected = false;
            }
        }

        public async Task StopConnectionAsync()
        {
            try
            {
                if (_hubConnection.State == HubConnectionState.Connected)
                {
                    // === SỬA TỪ ĐÂY ===
                    // Lấy UserId thật từ SecureStorage
                    var userId = await GetUserIdAsync();
                    if (!string.IsNullOrEmpty(userId))
                    {
                        // Gửi UserId thật
                        await UpdateDeviceStatusAsync(userId, false);
                    }
                    // === SỬA ĐẾN ĐÂY ===

                    await _hubConnection.StopAsync();
                    _isConnected = false;
                    _connectionId = string.Empty;
                    Debug.WriteLine(" SignalR connection stopped");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Failed to stop SignalR connection: {ex.Message}");
            }
        }

        // ==========================================================
        // ✅ SỬA LỖI GỐC (THIẾU ConnectionId) LÀ Ở ĐÂY
        // ==========================================================
        public async Task UpdateDeviceStatusAsync(string userId, bool isAppOpen)
        {
            // Đảm bảo ConnectionId không rỗng trước khi gọi
            if (string.IsNullOrEmpty(_connectionId) && _hubConnection.State == HubConnectionState.Connected)
            {
                _connectionId = _hubConnection.ConnectionId;
            }

            // Ưu tiên gọi qua Hub (Nhanh hơn, chính xác hơn)
            if (IsConnected && !string.IsNullOrEmpty(_connectionId))
            {
                try
                {
                    await _hubConnection.InvokeAsync("UpdateDeviceStatus", userId, isAppOpen);
                    Debug.WriteLine("📤 Sent status via HUB");
                    return; // Gửi Hub thành công thì return, không cần gọi API nữa
                }
                catch
                {
                    Debug.WriteLine("⚠️ Hub failed, falling back to API...");
                }
            }

            // Backup: Gọi qua API (Chỉ chạy khi Hub lỗi hoặc chưa kết nối)
            // Lúc này nếu _connectionId vẫn null thì đành chịu là "manual"
            var request = new DeviceStatusUpdateRequest
            {
                DeviceId = userId,
                IsAppOpen = isAppOpen,
                ConnectionId = _connectionId // Cái này phải đảm bảo có dữ liệu
            };
            await _Api.UpdateDeviceStatusAsync(request);
            Debug.WriteLine($"📤 Sent status via API (ConnectionId: {_connectionId})");
        }

        // === THÊM HÀM MỚI NÀY ===
        // Hàm helper MỚI: Luôn lấy UserId thật từ SecureStorage
        private async Task<string> GetUserIdAsync()
        {
            try
            {
                var userIdString = await SecureStorage.GetAsync("userid");

                if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out _))
                {
                    // Trả về UserId thật đã lưu khi đăng nhập
                    return userIdString;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi khi đọc SecureStorage 'userid': {ex.Message}");
            }

            Debug.WriteLine("CẢNH BÁO: Không tìm thấy UserId trong SecureStorage khi cập nhật DeviceStatus.");
            return string.Empty;
        }
    }
}