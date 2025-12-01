using HRMApp.ViewModels;

namespace HRMApp.View.Shared
{
    public partial class NotificationBadge : ContentView
    {
        public static readonly BindableProperty UnreadCountProperty = 
            BindableProperty.Create(nameof(UnreadCount), typeof(int), typeof(NotificationBadge), 0, 
                propertyChanged: OnUnreadCountChanged);

        public int UnreadCount
        {
            get => (int)GetValue(UnreadCountProperty);
            set => SetValue(UnreadCountProperty, value);
        }

        public string BadgeText => UnreadCount > 99 ? "99+" : UnreadCount.ToString();
        public int BadgeSize => UnreadCount > 99 ? 22 : 18;

        public NotificationBadge()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private static void OnUnreadCountChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is NotificationBadge badge)
            {
                badge.OnPropertyChanged(nameof(BadgeText));
                badge.OnPropertyChanged(nameof(BadgeSize));
            }
        }
    }
}