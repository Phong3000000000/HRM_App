using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using HRMApp.Helpers;
using HRMApp.Model.Notification;
using HRMApp.Services.Api;
using HRMApp.Services.Notification;
using Plugin.Firebase.CloudMessaging;
using System.Diagnostics;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Android;
using Debug = System.Diagnostics.Debug;
using Microsoft.Maui.Storage; // <-- THÊM USING NÀY

namespace HRMApp
{
    [Activity(Theme = "@style/Maui.SplashTheme",
             MainLauncher = true,
             LaunchMode = LaunchMode.SingleTop,
             ConfigurationChanges = ConfigChanges.ScreenSize
                                  | ConfigChanges.Orientation
                                  | ConfigChanges.UiMode
                                  | ConfigChanges.ScreenLayout
                                  | ConfigChanges.SmallestScreenSize
                                  | ConfigChanges.Density)]
    [IntentFilter(new[] { "FLUTTER_NOTIFICATION_CLICK" }, Categories = new[] { "android.intent.category.DEFAULT" })]
    public class MainActivity : MauiAppCompatActivity
    {
        private ISignalRService _signalRService;
        private bool _openedFromNotificationTap = false;
        private bool _userInteractedWithApp = false;
        private const int LOCATION_PERMISSION_REQUEST_CODE = 1001;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestLocationPermissions();
            CreateNotificationChannelIfNeeded();
            HandleIntent(Intent);
        }

        // ... (Code xin quyền: RequestLocationPermissions, OnRequestPermissionsResult, ShowPermissionExplanationDialog giữ nguyên) ...
        #region Permission Handling
        private void RequestLocationPermissions()
        {
            var permissions = new[]
            {
                Manifest.Permission.AccessFineLocation,
                Manifest.Permission.AccessCoarseLocation
            };

            var permissionsNeeded = permissions.Where(permission =>
                ContextCompat.CheckSelfPermission(this, permission) != Permission.Granted).ToArray();

            if (permissionsNeeded.Length > 0)
            {
                Debug.WriteLine("🔐 Yêu cầu quyền location để đọc Wi-Fi BSSID");
                ActivityCompat.RequestPermissions(this, permissionsNeeded, LOCATION_PERMISSION_REQUEST_CODE);
            }
            else
            {
                Debug.WriteLine("✅ Đã có quyền location");
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            if (requestCode == LOCATION_PERMISSION_REQUEST_CODE)
            {
                for (int i = 0; i < permissions.Length; i++)
                {
                    if (grantResults[i] == Permission.Granted)
                    {
                        Debug.WriteLine($"✅ Quyền {permissions[i]} đã được cấp");
                    }
                    else
                    {
                        Debug.WriteLine($"❌ Quyền {permissions[i]} bị từ chối");
                        ShowPermissionExplanationDialog();
                    }
                }
            }
        }
        private void ShowPermissionExplanationDialog()
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetTitle("Cần quyền truy cập vị trí");
            builder.SetMessage("Ứng dụng cần quyền truy cập vị trí để đọc thông tin Wi-Fi (BSSID) nhằm xác định vị trí làm việc của bạn.");
            builder.SetPositiveButton("Cài đặt", (s, e) =>
            {
                var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
                var uri = Android.Net.Uri.FromParts("package", PackageName, null);
                intent.SetData(uri);
                StartActivity(intent);
            });
            builder.SetNegativeButton("Bỏ qua", new EventHandler<DialogClickEventArgs>((s, e) => { }));
            builder.Show();
        }
        #endregion

        // === HÀM NÀY ĐÃ ĐƯỢC SỬA ===
        protected override async void OnResume()
        {
            base.OnResume();
            Debug.WriteLine("🔄 MainActivity OnResume");

            try
            {
                _signalRService ??= ServiceHelper.GetService<ISignalRService>();

                if (_signalRService != null)
                {
                    if (!_signalRService.IsConnected)
                    {
                        await _signalRService.StartConnectionAsync();
                    }

                    var userId = await GetUserIdAsync();
                    if (!string.IsNullOrEmpty(userId))
                    {
                        // 🔥 LUÔN LUÔN cập nhật thành OPEN khi OnResume
                        // Vì OnResume = app đang được hiển thị cho user
                        await _signalRService.UpdateDeviceStatusAsync(userId, true);
                        Debug.WriteLine($"✅ OnResume: Device (UserId: {userId}) marked as OPEN");

                        // Backup với REST API để đảm bảo
                        var api = ServiceHelper.GetService<ILocalApi>();
                        await api.UpdateDeviceStatusAsync(new DeviceStatusUpdateRequest
                        {
                            DeviceId = userId,
                            IsAppOpen = true
                        });
                        Debug.WriteLine($"📤 OnResume: Device marked as OPEN via REST API");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ MainActivity OnResume error: {ex.Message}");
            }
        }
        protected override async void OnStop()
        {
            base.OnStop();
            Debug.WriteLine("🛑 MainActivity OnStop - App might be killed");

            try
            {
                var userId = await GetUserIdAsync();
                if (!string.IsNullOrEmpty(userId))
                {
                    _signalRService ??= ServiceHelper.GetService<ISignalRService>();

                    // Gửi qua cả SignalR và REST API để đảm bảo
                    if (_signalRService?.IsConnected == true)
                    {
                        await _signalRService.UpdateDeviceStatusAsync(userId, false);
                        Debug.WriteLine($"📤 OnStop: Device marked as CLOSED via SignalR");
                    }

                    // Backup với REST API
                    var api = ServiceHelper.GetService<ILocalApi>();
                    await api.UpdateDeviceStatusAsync(new DeviceStatusUpdateRequest
                    {
                        DeviceId = userId,
                        IsAppOpen = false
                    });
                    Debug.WriteLine($"📤 OnStop: Device marked as CLOSED via REST API");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ OnStop error: {ex.Message}");
            }
        }
        // === HÀM NÀY ĐÃ ĐƯỢC SỬA ===
        protected override async void OnPause()
        {
            base.OnPause();
            Debug.WriteLine("🟡 OnPause CALLED");
            Preferences.Set("last_pause_time", DateTime.UtcNow.ToBinary());

            try
            {
                // SỬA Ở ĐÂY: Lấy UserId thật
                var userId = await GetUserIdAsync();
                if (string.IsNullOrEmpty(userId))
                {
                    Debug.WriteLine("⚠️ Không tìm thấy UserId (OnPause), bỏ qua.");
                    return;
                }

                _signalRService ??= ServiceHelper.GetService<ISignalRService>();

                if (_signalRService?.IsConnected == true)
                {
                    Debug.WriteLine($"📤 Sending CLOSED via SignalR for {userId}");
                    await _signalRService.UpdateDeviceStatusAsync(userId, false);
                }
                else
                {
                    // SỬA LOGIC DỰ PHÒNG (API)
                    Debug.WriteLine("⚠️ SignalR not connected, fallback to REST");
                    var Api = ServiceHelper.GetService<ILocalApi>();

                    await Api.UpdateDeviceStatusAsync(new DeviceStatusUpdateRequest
                    {
                        DeviceId = userId, // Gửi UserId thật
                        IsAppOpen = false
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" OnPause error: {ex.Message}");
            }
        }

        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
            _userInteractedWithApp = true;
            Debug.WriteLine("👆 User interaction detected");
        }

        // === HÀM NÀY ĐÃ ĐƯỢC SỬA ===
        protected override async void OnDestroy()
        {
            base.OnDestroy();
            Debug.WriteLine("MainActivity OnDestroy");

            try
            {
                // SỬA Ở ĐÂY: Lấy UserId thật
                var userId = await GetUserIdAsync();
                if (string.IsNullOrEmpty(userId)) return;

                _signalRService ??= ServiceHelper.GetService<ISignalRService>();

                if (_signalRService?.IsConnected == true)
                {
                    await _signalRService.UpdateDeviceStatusAsync(userId, false);
                    Debug.WriteLine($"Device (UserId: {userId}) marked as CLOSED (OnDestroy)");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"MainActivity OnDestroy error: {ex.Message}");
            }
        }



        // ... (Code HandleIntent, CreateNotificationChannelIfNeeded giữ nguyên) ...
        #region Intent and Channel
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            HandleIntent(intent);
        }
        private void HandleIntent(Intent intent)
        {
            try
            {
                FirebaseCloudMessagingImplementation.OnNewIntent(intent);

                if (intent?.Action == "FLUTTER_NOTIFICATION_CLICK")
                {
                    Debug.WriteLine("📣 App được mở từ thông báo Firebase");
                    _openedFromNotificationTap = true; // Đánh dấu để OnResume biết
                    if (intent?.Extras != null)
                    {
                        foreach (var key in intent.Extras.KeySet())
                        {
                            var value = intent.Extras.GetString(key);
                            Debug.WriteLine($"🔑 {key}: {value}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Lỗi xử lý intent: {ex.Message}");
            }
        }
        private void CreateNotificationChannelIfNeeded()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                CreateNotificationChannel();
            }
        }
        private void CreateNotificationChannel()
        {
            var channelId = $"{PackageName}.general";
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            var channel = new NotificationChannel(channelId, "General", NotificationImportance.Default);
            notificationManager.CreateNotificationChannel(channel);

            FirebaseCloudMessagingImplementation.ChannelId = channelId;
        }
        #endregion

        // === HÀM GetDeviceId() ĐÃ ĐƯỢC SỬA THÀNH GetUserIdAsync() ===
        private async Task<string> GetUserIdAsync()
        {
            try
            {
                // Key "userid" phải khớp với key bạn dùng khi lưu lúc đăng nhập
                var userIdString = await SecureStorage.GetAsync("userid");
                if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out _))
                {
                    return userIdString;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi khi đọc SecureStorage 'userid': {ex.Message}");
            }

            Debug.WriteLine("CẢNH BÁO: Không tìm thấy UserId trong SecureStorage (MainActivity).");
            return string.Empty;
        }
    }
}