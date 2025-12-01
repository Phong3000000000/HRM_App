using HRMApp.Helpers;
using HRMApp.Model.Notification;
using HRMApp.Services.Notification;
using HRMApp.View;
using HRMApp.ViewModels;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace HRMApp
{
    public partial class App : Application
    {
        private ISignalRService _signalRService;
        private IDeviceNotificationService _deviceNotificationService;

        public event Action<SignalRNotification> OnAppRealTimeNotification;

        public App()
        {
            InitializeComponent();

            // ⚡ Gán MainPage để tránh lỗi NotImplementedException
            MainPage = new ContentPage
            {
                Content = new ActivityIndicator
                {
                    IsRunning = true,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                },
                BackgroundColor = Colors.White
            };

            // 🔍 Kiểm tra token ngầm
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            var token = await SecureStorage.GetAsync("jwt_token");

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (string.IsNullOrEmpty(token) || IsTokenExpired(token))
                {
                    // ❌ Token trống → vào LoginPage
                    MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    // ✅ Token hợp lệ → vào AppShell (MainPage)
                    var notificationStateService = ServiceHelper.GetService<NotificationStateService>();
                    var notificationViewModel = ServiceHelper.GetService<NotificationViewModel>(); // Added this line
                    MainPage = new AppShell(notificationStateService, notificationViewModel); // Updated to pass both parameters

                    // Khởi chạy SignalR và Device Notification services sau khi MainPage đã được thiết lập
                    _ = Task.Run(InitSignalRAndNotifications);
                }
            });
        }
        protected override async void OnStart()
        {
            var signalRService = ServiceHelper.GetService<ISignalRService>();
            if (signalRService != null)
            {
                await signalRService.StartConnectionAsync();
            }
        }
        public async Task InitSignalRAndNotifications()
        {
            try
            {
                await Task.Delay(500); // Cho services khởi tạo xong

                var services = Handler?.MauiContext?.Services;
                if (services == null) return;

                // Lấy service trên Main Thread (sau khi Handler đã được tạo)
                _signalRService = services.GetService<ISignalRService>();
                _deviceNotificationService = services.GetService<IDeviceNotificationService>();

                if (_signalRService != null)
                {
                    _signalRService.OnNotificationReceived += OnNotificationReceived;
                    _signalRService.OnRealTimeNotification += OnRealTimeNotificationReceived;
                    _signalRService.OnRealTimeNotification += HandleSignalRRealTime;

                    // Khởi động SignalR connection
                    await _signalRService.StartConnectionAsync();
                    Debug.WriteLine("✅ SignalR service initialized and connected");

                    // ❌ ĐÃ XÓA: Dòng code UpdateDeviceStatusAsync(deviceId, true) đã bị xóa
                    // Đây chính là nguyên nhân gây ra lỗi tự động cập nhật trạng thái "mở".
                    // Giờ đây, chỉ MainActivity.OnResume mới có quyền cập nhật trạng thái "mở".
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ InitSignalR error: {ex.Message}");
            }
        }

        private async void OnNotificationReceived(SignalRNotification notification)
        {
            Debug.WriteLine($"📩 Nhận thông báo: {notification.Title} - {notification.Content}");
            if (_deviceNotificationService != null)
            {
                await _deviceNotificationService.ShowInAppNotificationAsync(notification);
            }
            else
            {
                await ShowNotificationAlertFallback(notification);
            }
        }

        private async void OnRealTimeNotificationReceived(SignalRNotification notification)
        {
            Debug.WriteLine($"⚡ Nhận thông báo realtime: {notification.Title} - {notification.Content}");

            if (_deviceNotificationService != null)
            {
                await _deviceNotificationService.ShowInAppNotificationAsync(notification);
            }
            else
            {
                await ShowNotificationAlertFallback(notification);
            }
        }

        private void HandleSignalRRealTime(SignalRNotification notification)
        {
            Debug.WriteLine($"⚡ [App] Realtime: {notification.Type} - {notification.Title}");

            if (notification.Type == "hr")
            {
                Debug.WriteLine("🟢 App xử lý thông báo HR");
            }
            else if (notification.Type == "payroll")
            {
                Debug.WriteLine("🟡 App xử lý thông báo lương");
            }
            else if (notification.Type == "attendance")
            {
                Debug.WriteLine("🔵 App xử lý thông báo chấm công");
            }
            else if (notification.Type == "leave-request")
            {
                Debug.WriteLine("🟠 App xử lý thông báo nghỉ phép");
            }

            // 🟡 BẮN EVENT RA CHO MAINPAGE
            OnAppRealTimeNotification?.Invoke(notification);
        }

        private async Task ShowNotificationAlertFallback(SignalRNotification notification)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
                Current.MainPage.DisplayAlert(notification.Title, notification.Content, "OK"));
        }

        // ❌ ĐÃ XÓA OnSleep, OnResume, OnStart
        // Lý do: Các hàm này xung đột với logic trong MainActivity.cs.
        // Việc quản lý trạng thái (mở/đóng app) phải được thực hiện ở
        // tầng platform (MainActivity.cs) để đảm bảo độ chính xác
        // khi app được khởi động lại từ push notification.

        // protected override async void OnSleep() { ... }
        // protected override async void OnResume() { ... }
        // protected override async void OnStart() { ... }


        private bool IsTokenExpired(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                return jwt.ValidTo <= DateTime.UtcNow;
            }
            catch
            {
                return true;
            }
        }

        private string GetDeviceId()
        {
            const string key = "device_id";
            string deviceId = Preferences.Get(key, null);
            if (string.IsNullOrEmpty(deviceId))
            {
                deviceId = Guid.NewGuid().ToString();
                Preferences.Set(key, deviceId);
            }
            return deviceId;
        }
    }
}