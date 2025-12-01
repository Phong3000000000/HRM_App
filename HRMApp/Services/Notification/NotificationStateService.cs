using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HRMApp.Model.Notification;
using HRMApp.Services.Notification;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HRMApp.Services.Notification
{
    public class NotificationStateService : INotifyPropertyChanged
    {
        private int _unreadCount;

        // ❌ BƯỚC 1: KHÔNG CẦN signalRService Ở ĐÂY NỮA
        // private readonly ISignalRService _signalRService; 

        public int UnreadCount
        {
            get => _unreadCount;
            set
            {
                if (_unreadCount != value)
                {
                    _unreadCount = value;
                    OnPropertyChanged();
                }
            }
        }

        // ✅ BƯỚC 2: SỬA LẠI CONSTRUCTOR (Xóa tham số)
        public NotificationStateService(/* ISignalRService signalRService */)
        {
            // ❌ BƯỚC 3: XÓA BỎ TẤT CẢ LOGIC LẮNG NGHE
            // _signalRService = signalRService;
            // _signalRService.OnNotificationReceived += OnNotificationReceived;
            // _signalRService.OnRealTimeNotification += OnNotificationReceived;
        }

        // ❌ BƯỚC 4: XÓA HÀM XỬ LÝ NÀY
        /*
        private void OnNotificationReceived(SignalRNotification notification)
        {
            // Tăng số lượng khi nhận được thông báo mới
            UnreadCount++;
        }
        */

        // (Các hàm Decrement, Reset, MarkOneAsRead... của bạn giữ nguyên)
        public void DecrementUnreadCount()
        {
            if (UnreadCount > 0)
            {
                UnreadCount--;
            }
        }

        public void ResetUnreadCount()
        {
            UnreadCount = 0;
        }

        public void MarkOneAsRead()
        {
            if (UnreadCount > 0)
            {
                UnreadCount--;
            }
        }

        // (OnPropertyChanged giữ nguyên)
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}