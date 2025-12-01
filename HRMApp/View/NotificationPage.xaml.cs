using System.Collections.ObjectModel;
using HRMApp.ViewModels;
using HRMApp.Services.Notification;
using HRMApp.Services.Api;
using HRMApp.Helpers;
using HRMApp.Model.Notification;
using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace HRMApp.View
{
    public partial class NotificationPage : ContentPage
    {
        private readonly IDeviceNotificationService _deviceNotificationService;

        // ✅ SỬA ĐỔI: _viewModel sẽ được lấy từ ServiceHelper
        private NotificationViewModel _viewModel;

        // ❌ BƯỚC 1: XÓA 'NotificationViewModel viewModel' KHỎI CONSTRUCTOR
        public NotificationPage(/* NotificationViewModel viewModel */)
        {
            InitializeComponent();

            // ✅ BƯỚC 2: LẤY VIEWMODEL TỪ ServiceHelper
            // Điều này đảm bảo bạn lấy đúng Singleton instance mà AppShell đang dùng
            _viewModel = ServiceHelper.GetService<NotificationViewModel>();

            // ✅ BƯỚC 3: GÁN BINDING CONTEXT BẰNG TAY
            BindingContext = _viewModel;

            _deviceNotificationService = ServiceHelper.GetService<IDeviceNotificationService>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // (Code kiểm tra SignalR của bạn đã đúng)
            var signalRService = ServiceHelper.GetService<ISignalRService>();
            Debug.WriteLine($"🔍 SignalR IsConnected for notification page: {signalRService.IsConnected}");
            if (!signalRService.IsConnected)
            {
                Debug.WriteLine($"⚠️ SignalR not connected, attempting to reconnect...");
                await signalRService.StartConnectionAsync();
            }

            // Logic này giờ sẽ luôn chạy trên ViewModel CHÍNH (Singleton)
            if (_viewModel != null)
            {
                await _viewModel.LoadNotificationsAsync(forceRefresh: false);
            }

            if (_deviceNotificationService != null)
            {
                await _deviceNotificationService.HideInAppNotificationAsync();
            }
        }

        // ❌ BƯỚC 4: XÓA (HOẶC COMMENT) HÀM NÀY
        // Nó không cần thiết vì bạn đã dùng Command trong XAML
        /*
        private async void OnNotificationTapped(object sender, TappedEventArgs e)
        {
            if (sender is Microsoft.Maui.Controls.View view && view.BindingContext is SignalRNotification notification)
            {
                if (_viewModel.ViewNotificationCommand.CanExecute(notification))
                {
                    await _viewModel.ViewNotificationCommand.ExecuteAsync(notification);
                }
            }
        }
        */
    }
}