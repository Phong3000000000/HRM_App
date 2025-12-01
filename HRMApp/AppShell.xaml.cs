using HRMApp.View;
using HRMApp.Services.Notification;
using System.ComponentModel;
using HRMApp.ViewModels;

namespace HRMApp
{
    public partial class AppShell : Shell
    {
        public static bool IsLoggedIn { get; set; } = false;
        public static string LoggedInUsername { get; set; } = "";
        public static string LoggedInEmail { get; set; } = "";

        private readonly NotificationStateService _notificationState;
        private readonly NotificationViewModel _notificationViewModel;

        public AppShell(NotificationStateService notificationState, NotificationViewModel notificationViewModel)
        {
            InitializeComponent();

            _notificationState = notificationState;
            _notificationViewModel = notificationViewModel;

            BindingContext = _notificationState;
            _notificationState.PropertyChanged += OnNotificationStateChanged;

            // --- ĐĂNG KÝ ROUTE CHO CÁC TRANG ---

            // 1. Route cho phần Thông báo (Cũ)
            Routing.RegisterRoute("NotificationPage", typeof(NotificationPage));
            Routing.RegisterRoute(nameof(NotificationDetailPage), typeof(NotificationDetailPage));

            // 2. Route cho phần Đào tạo (MỚI THÊM VÀO)
            Routing.RegisterRoute(nameof(TrainingListPage), typeof(TrainingListPage));
            Routing.RegisterRoute(nameof(TrainingDetailPage), typeof(TrainingDetailPage));
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Gọi tải thông báo ngay khi AppShell xuất hiện
            if (_notificationViewModel != null)
            {
                await _notificationViewModel.LoadNotificationsAsync();
            }
        }

        private void OnNotificationStateChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NotificationStateService.UnreadCount))
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    int count = _notificationState.UnreadCount;

                    if (count > 0)
                    {
                        NotificationTab.Icon = "notification_badge.png";
                        NotificationTab.Title = $"Thông báo ({count})";
                    }
                    else
                    {
                        NotificationTab.Icon = "notification.png";
                        NotificationTab.Title = "Thông báo";
                    }
                });
            }
        }
    }
}