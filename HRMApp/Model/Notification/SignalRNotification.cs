using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace HRMApp.Model.Notification
{
    public class SignalRNotification : INotifyPropertyChanged
    {
        // ✅ BƯỚC 1: Implement INotifyPropertyChanged (để báo cho UI)
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // ✅ BƯỚC 2: Tạo backing fields cho TẤT CẢ properties
        private Guid _id;
        private Guid _userId;
        private string _type = string.Empty;
        private string _title = string.Empty;
        private string _content = string.Empty;
        private DateTime _createdAt = DateTime.Now;
        private bool _isRead;
        private DateTime? _readAt;
        private string? _actionUrl;

        // ✅ BƯỚC 3: Sửa lại TẤT CẢ properties để gọi SetProperty

        [JsonPropertyName("id")]
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [JsonPropertyName("userId")]
        public Guid UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        [JsonPropertyName("type")]
        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        [JsonPropertyName("title")]
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value); // <-- SỬA LỖI Ở ĐÂY
        }

        [JsonPropertyName("content")]
        public string Content
        {
            get => _content;
            set
            {
                // Gọi SetProperty và cũng cập nhật 'Body'
                if (SetProperty(ref _content, value)) // <-- SỬA LỖI Ở ĐÂY
                {
                    OnPropertyChanged(nameof(Body)); // Thông báo Body cũng thay đổi
                }
            }
        }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt
        {
            get => _createdAt;
            set => SetProperty(ref _createdAt, value);
        }

        [JsonPropertyName("isRead")]
        public bool IsRead
        {
            get => _isRead;
            set => SetProperty(ref _isRead, value); // <-- Code này của bạn đã gần đúng
        }

        // ✅ THÊM PROPERTY ReadAt
        [JsonPropertyName("readAt")]
        public DateTime? ReadAt
        {
            get => _readAt;
            set => SetProperty(ref _readAt, value);
        }

        [JsonPropertyName("actionUrl")]
        public string? ActionUrl
        {
            get => _actionUrl;
            set => SetProperty(ref _actionUrl, value);
        }

        [JsonIgnore]
        public string Body
        {
            get => Content; // Body là alias của Content
            set => Content = value;
        }
    }
}