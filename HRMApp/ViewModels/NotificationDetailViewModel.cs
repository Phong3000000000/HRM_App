using CommunityToolkit.Mvvm.ComponentModel;
using HRMApp.Model.Notification;
using HRMApp.Services.Notification;

namespace HRMApp.ViewModels
{
    [QueryProperty(nameof(Notification), "Notification")]
    public partial class NotificationDetailViewModel : ObservableObject
    {
        [ObservableProperty]
        SignalRNotification notification;

        private readonly NotificationStateService _notificationStateService;

        public NotificationDetailViewModel(NotificationStateService notificationStateService)
        {
            _notificationStateService = notificationStateService;
        }

        partial void OnNotificationChanged(SignalRNotification value)
        {
            // Khi nhận được thông báo mới và nó chưa được đọc
            if (value != null && !value.IsRead)
            {
                // Giảm số lượng thông báo chưa đọc ngay lập tức
                _notificationStateService.MarkOneAsRead();
            }
        }
    }
}