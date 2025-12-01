using HRMApp.Model.Notification;
using HRMApp.Services.Api;
using HRMApp.View.Shared;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ILayout = Microsoft.Maui.ILayout;

namespace HRMApp.Services.Notification
{
    public interface IDeviceNotificationService
    {
        Task<List<DeviceStatusModel>> GetAllDevicesAsync();
        Task<List<DeviceStatusModel>> GetOpenDevicesAsync();
        Task<List<DeviceStatusModel>> GetClosedDevicesAsync();
        Task<DeviceStatusModel> CheckDeviceStatusAsync(string deviceId);
        Task ShowInAppNotificationAsync(SignalRNotification notification);
        Task HandleNotificationAsync(SignalRNotification notification);
        Task HideInAppNotificationAsync();
    }

    public class DeviceNotificationService : IDeviceNotificationService
    {
        private readonly ILocalApi _Api;
        private readonly ISignalRService _signalRService;
        private TopNotificationView? _currentNotificationView;
        private bool _isShowing = false;

        public DeviceNotificationService(ILocalApi Api, ISignalRService signalRService)
        {
            _Api = Api;
            _signalRService = signalRService;

            // Subscribe to the notification event from SignalRService
            _signalRService.OnNotificationReceived += HandleNotificationReceived;
            _signalRService.OnRealTimeNotification += HandleRealTimeNotification;

            // ==========================================================
            // ✅ THÊM DÒNG NÀY ĐỂ HIỆN POPUP KHI UPDATE
            // =Setting (Listen) cho sự kiện Update, và dùng chung hàm xử lý
            // ==========================================================
            _signalRService.OnNotificationUpdated += HandleNotificationReceived;

            Debug.WriteLine(" DeviceNotificationService: Handlers enabled to receive (Create) AND (Update) notifications.");
        }

        public async Task<List<DeviceStatusModel>> GetAllDevicesAsync()
        {
            try
            {
                return await _Api.GetAllDevicesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Failed to get all devices: {ex.Message}");
                return new List<DeviceStatusModel>();
            }
        }

        public async Task<List<DeviceStatusModel>> GetOpenDevicesAsync()
        {
            try
            {
                return await _Api.GetOpenDevicesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Failed to get open devices: {ex.Message}");
                return new List<DeviceStatusModel>();
            }
        }

        public async Task<List<DeviceStatusModel>> GetClosedDevicesAsync()
        {
            try
            {
                return await _Api.GetClosedDevicesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Failed to get closed devices: {ex.Message}");
                return new List<DeviceStatusModel>();
            }
        }

        public async Task<DeviceStatusModel> CheckDeviceStatusAsync(string deviceId)
        {
            try
            {
                return await _Api.CheckDeviceStatusAsync(deviceId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Failed to check device status: {ex.Message}");
                return new DeviceStatusModel { DeviceId = deviceId, IsAppOpen = false };
            }
        }

        public async Task ShowInAppNotificationAsync(SignalRNotification notification)
        {
            try
            {
                // ✅ SỬA LỖI: Dùng Content (vì Body không có trong INotifyPropertyChanged)
                Debug.WriteLine($" Hiển thị thông báo với TopNotificationView: {notification.Title} - {notification.Content}");

                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    try
                    {
                        if (_isShowing)
                        {
                            Debug.WriteLine(" Đang có thông báo khác, bỏ qua thông báo mới");
                            return;
                        }
                        _isShowing = true;
                        _currentNotificationView = new TopNotificationView();
                        await ThemVaoTrangHienTai();
                        var userTapped = await _currentNotificationView.ShowNotificationAsync(notification);
                        if (userTapped)
                        {
                            // Xử lý khi người dùng nhấn vào thông báo
                        }
                        await DonDepNotificationView();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($" Lỗi khi hiển thị thông báo: {ex.Message}");
                        Debug.WriteLine($"📋 Chi tiết lỗi: {ex}");
                        await DonDepNotificationView();
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Thất bại khi hiển thị thông báo trong app: {ex.Message}");
                _isShowing = false;
            }
        }

        private async Task ThemVaoTrangHienTai()
        {
            try
            {
                var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                Debug.WriteLine($"🔍 MainPage type: {mainPage?.GetType().Name}");

                if (mainPage is Shell shell)
                {
                    var currentPage = shell.CurrentPage;
                    Debug.WriteLine($"🔍 Shell.CurrentPage type: {currentPage?.GetType().Name}");

                    if (currentPage is NavigationPage navPage && navPage.CurrentPage is ContentPage contentPage)
                    {
                        await AddToContentPage(contentPage);
                    }
                    else if (currentPage is ContentPage directContentPage)
                    {
                        await AddToContentPage(directContentPage);
                    }
                    else
                    {
                        Debug.WriteLine("❌ Could not find ContentPage in Shell");
                        TryAddToShell(shell);
                    }
                }
                else if (mainPage is ContentPage contentPage)
                {
                    Debug.WriteLine("📄 MainPage is ContentPage");
                    await AddToContentPage(contentPage);
                }
                else
                {
                    Debug.WriteLine($"❌ MainPage type not supported: {mainPage?.GetType().Name}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Error in ThemVaoTrangHienTai: {ex.Message}");
                throw;
            }
        }

        private Task AddToContentPage(ContentPage page)
        {
            try
            {
                Debug.WriteLine($"📄 Adding to ContentPage: {page.GetType().Name}");
                Debug.WriteLine($"🔍 Content type: {page.Content?.GetType().Name}");

                // ✅ SỬA LỖI: Ưu tiên AbsoluteLayout
                var absLayout = FindAbsoluteLayoutInView(page.Content);
                if (absLayout != null)
                {
                    Debug.WriteLine("✅ Tìm thấy AbsoluteLayout. Thêm vào (overlay).");
                    ThemVaoLayout(absLayout);
                }
                else if (page.Content is Layout layout)
                {
                    Debug.WriteLine($"⚠️ Không tìm thấy AbsoluteLayout. Thêm vào {layout.GetType().Name} (sẽ đẩy UI).");
                    ThemVaoLayout(layout);
                }
                else
                {
                    Debug.WriteLine("❌ ContentPage has no Layout content");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Error adding to ContentPage: {ex.Message}");
                throw;
            }
            return Task.CompletedTask;
        }

        private void TryAddToShell(Shell shell)
        {
            try
            {
                Debug.WriteLine("🐚 Attempting to add to Shell");
                if (shell.CurrentPage is ContentPage contentPage && contentPage.Content is Layout layout)
                {
                    ThemVaoLayout(layout);
                }
                else
                {
                    Debug.WriteLine("❌ Shell does not contain a Layout in its CurrentPage");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Error adding to Shell: {ex.Message}");
            }
        }

        private AbsoluteLayout? FindAbsoluteLayoutInView(Microsoft.Maui.Controls.View view)
        {
            if (view is AbsoluteLayout absoluteLayout)
            {
                return absoluteLayout;
            }
            if (view is ContentView contentView && contentView.Content != null)
            {
                return FindAbsoluteLayoutInView(contentView.Content);
            }
            if (view is Layout layout)
            {
                foreach (var child in layout.Children)
                {
                    if (child is Microsoft.Maui.Controls.View childView)
                    {
                        var found = FindAbsoluteLayoutInView(childView);
                        if (found != null)
                            return found;
                    }
                }
            }
            return null;
        }

        private void ThemVaoLayout(Layout layout)
        {
            try
            {
                if (_currentNotificationView is null)
                {
                    Debug.WriteLine(" Lỗi: _currentNotificationView là null khi thêm vào layout.");
                    return;
                }

                // ✅ SỬA LỖI: Ưu tiên logic Overlay
                if (layout is AbsoluteLayout absoluteLayout)
                {
                    // Thêm vào AbsoluteLayout (Nổi lên trên)
                    AbsoluteLayout.SetLayoutBounds(_currentNotificationView, new Rect(0, 0, 1, AbsoluteLayout.AutoSize));
                    AbsoluteLayout.SetLayoutFlags(_currentNotificationView, AbsoluteLayoutFlags.WidthProportional | AbsoluteLayoutFlags.XProportional);
                    absoluteLayout.Children.Add(_currentNotificationView);
                    Debug.WriteLine(" Đã thêm vào AbsoluteLayout (overlay)");
                }
                else if (layout is Grid grid)
                {
                    // Thêm vào Grid (Nổi lên trên)
                    grid.Children.Add(_currentNotificationView);
                    Grid.SetRow(_currentNotificationView, 0);
                    Grid.SetColumn(_currentNotificationView, 0);
                    Grid.SetRowSpan(_currentNotificationView, 1);
                    Grid.SetColumnSpan(_currentNotificationView, grid.ColumnDefinitions.Count > 0 ? grid.ColumnDefinitions.Count : 1);
                    Debug.WriteLine($" Đã thêm vào Grid (overlay)");
                }
                // Fallback (Logic đẩy UI)
                else if (layout is StackLayout stackLayout)
                {
                    stackLayout.Children.Insert(0, _currentNotificationView);
                    Debug.WriteLine(" (Cảnh báo) Đã thêm vào StackLayout (sẽ đẩy UI)");
                }
                else if (layout is VerticalStackLayout verticalStackLayout)
                {
                    verticalStackLayout.Children.Insert(0, _currentNotificationView);
                    Debug.WriteLine(" (Cảnh báo) Đã thêm vào VerticalStackLayout (sẽ đẩy UI)");
                }
                else
                {
                    Debug.WriteLine($" Layout type không được hỗ trợ: {layout.GetType().Name}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Lỗi khi thêm vào layout: {ex.Message}");
                throw;
            }
        }

        private Task DonDepNotificationView()
        {
            try
            {
                Debug.WriteLine(" Bắt đầu dọn dẹp notification view...");
                if (_currentNotificationView != null)
                {
                    var parent = _currentNotificationView.Parent;
                    if (parent is Layout parentLayout)
                    {
                        parentLayout.Children.Remove(_currentNotificationView);
                        Debug.WriteLine($" Đã xóa notification khỏi {parentLayout.GetType().Name}");
                    }
                    else if (parent is ILayout iLayout)
                    {
                        iLayout.Remove(_currentNotificationView);
                        Debug.WriteLine($" Đã xóa notification khỏi ILayout: {parent?.GetType().Name}");
                    }
                    _currentNotificationView = null;
                }
                _isShowing = false;
                Debug.WriteLine(" Đã dọn dẹp notification view hoàn tất");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Lỗi khi dọn dẹp notification: {ex.Message}");
                _isShowing = false;
            }
            return Task.CompletedTask;
        }

        public async Task HandleNotificationAsync(SignalRNotification notification)
        {
            try
            {
                // ✅ SỬA LỖI: Dùng Content
                Debug.WriteLine($" Xử lý thông báo: {notification.Title} - {notification.Content}");
                await ShowInAppNotificationAsync(notification);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Thất bại khi xử lý thông báo: {ex.Message}");
            }
        }
        public async Task HideInAppNotificationAsync()
        {
            if (_currentNotificationView != null)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await _currentNotificationView.HideWithAnimation();
                    await DonDepNotificationView();
                });
            }
        }

        private async void HandleNotificationReceived(SignalRNotification notification)
        {
            try
            {
                Debug.WriteLine($"📨 HandleNotificationReceived: {notification.Title}");
                await HandleNotificationAsync(notification);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Lỗi trong HandleNotificationReceived: {ex.Message}");
            }
        }

        private async void HandleRealTimeNotification(SignalRNotification notification)
        {
            try
            {
                Debug.WriteLine($" HandleRealTimeNotification: {notification.Title}");
                await ShowInAppNotificationAsync(notification);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Lỗi trong HandleRealTimeNotification: {ex.Message}");
            }
        }

        private async Task NavigateToArticleAsync(int articleId)
        {
            try
            {
                await Shell.Current.GoToAsync($"detail?articleId={articleId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($" Thất bại khi điều hướng đến bài viết {articleId}: {ex.Message}");
            }
        }

        private string GetDeviceId()
        {
            const string key = "device_id";
            string? deviceId = Preferences.Get(key, null);
            if (string.IsNullOrEmpty(deviceId))
            {
                deviceId = Guid.NewGuid().ToString();
                Preferences.Set(key, deviceId);
            }
            return deviceId;
        }
    }
}