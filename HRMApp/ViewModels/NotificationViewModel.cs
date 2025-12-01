using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HRMApp.Model.Notification;
using HRMApp.Services.Notification;
using HRMApp.Services.Api;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using HRMApp.View;
using System.Collections.Generic;
using System.Linq;

namespace HRMApp.ViewModels
{
    public partial class NotificationViewModel : BaseViewModel
    {
        // Services
        private readonly ISignalRService _signalRService;
        private readonly ILocalApi _localApi;
        private readonly IDeviceNotificationService _deviceNotificationService;
        private readonly NotificationStateService _notificationStateService;

        // Fields
        private int _unreadCount;
        private bool _isLoading;
        private bool _isRefreshing;
        private bool _isDataLoaded = false;
        private DateTime _lastLoadTime = DateTime.MinValue;
        private const double CacheDurationMinutes = 0.1;

        // ============================================================
        // 1. DANH SÁCH DỮ LIỆU
        // ============================================================

        // Danh sách gốc (Source of Truth) - Chứa TẤT CẢ thông báo
        private List<SignalRNotification> _allNotifications = new();

        // Danh sách hiển thị (Binding lên UI) - Đã qua lọc/sort
        public ObservableRangeCollection<SignalRNotification> FilteredNotifications { get; } = new();

        // ============================================================
        // 2. CÁC THUỘC TÍNH LỌC & SEARCH
        // ============================================================
        public ObservableRangeCollection<string> FilterTypes { get; } = new()
        {
            "Tất cả",
            "new",
            "updated",
            "deleted"
        };

        // Loại đang được chọn (Mặc định là "Tất cả")
        private string _selectedFilterType = "Tất cả";
        public string SelectedFilterType
        {
            get => _selectedFilterType;
            set
            {
                if (_selectedFilterType != value)
                {
                    _selectedFilterType = value;
                    OnPropertyChanged();
                    ApplyFilters(); // ⚡ Gọi hàm lọc ngay khi người dùng chọn loại khác
                }
            }
        }
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                    ApplyFilters(); // ⚡ Tự động lọc khi gõ chữ
                }
            }
        }

        private bool _isShowUnreadOnly;
        public bool IsShowUnreadOnly
        {
            get => _isShowUnreadOnly;
            set
            {
                if (_isShowUnreadOnly != value)
                {
                    _isShowUnreadOnly = value;
                    OnPropertyChanged();
                    ApplyFilters(); // ⚡ Tự động lọc khi bấm checkbox
                }
            }
        }

        private bool _isSortDescending = true; // Mặc định mới nhất lên đầu
        public bool IsSortDescending
        {
            get => _isSortDescending;
            set
            {
                if (_isSortDescending != value)
                {
                    _isSortDescending = value;
                    OnPropertyChanged();
                    ApplyFilters(); // ⚡ Tự động sắp xếp lại
                }
            }
        }

        // Các Property cũ
        public int UnreadCount
        {
            get => _unreadCount;
            set { if (_unreadCount != value) { _unreadCount = value; OnPropertyChanged(); } }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { if (_isLoading != value) { _isLoading = value; OnPropertyChanged(); } }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set { if (_isRefreshing != value) { _isRefreshing = value; OnPropertyChanged(); } }
        }

        // Commands
        public ICommand LoadNotificationsCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand ToggleSortCommand { get; } // Command đảo chiều sắp xếp

        // Constructor
        public NotificationViewModel(ISignalRService signalRService, ILocalApi localApi, IDeviceNotificationService deviceNotificationService, NotificationStateService notificationStateService)
        {
            _signalRService = signalRService;
            _localApi = localApi;
            _deviceNotificationService = deviceNotificationService;
            _notificationStateService = notificationStateService;

            LoadNotificationsCommand = new Command(async () => await LoadNotificationsAsync());
            RefreshCommand = new Command(async () => await RefreshNotificationsAsync());

            // Command đảo chiều sort
            ToggleSortCommand = new Command(() => IsSortDescending = !IsSortDescending);

            // SignalR Subscriptions
            _signalRService.OnNotificationReceived += OnNewNotificationReceived;
            _signalRService.OnRealTimeNotification += OnNewNotificationReceived;
            _signalRService.OnNotificationUpdated += OnNotificationUpdated;
            _signalRService.OnNotificationDeleted += OnNotificationDeleted;
        }

        // ============================================================
        // 3. LOGIC LỌC VÀ SẮP XẾP CHÍNH (CORE LOGIC)
        // ============================================================
        private void ApplyFilters()
        {
            // Bắt đầu từ danh sách gốc
            IEnumerable<SignalRNotification> query = _allNotifications;

            // 1. Lọc theo Search Text
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                query = query.Where(n => n.Title != null &&
                                         n.Title.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // 2. Lọc theo trạng thái chưa đọc
            if (IsShowUnreadOnly)
            {
                query = query.Where(n => !n.IsRead);
            }

            // 3. 🟢 THÊM LOGIC LỌC THEO TYPE
            // Nếu không phải là "Tất cả", thì chỉ lấy những item có Type trùng khớp
            if (!string.IsNullOrEmpty(SelectedFilterType) && SelectedFilterType != "Tất cả")
            {
                query = query.Where(n => string.Equals(n.Type, SelectedFilterType, StringComparison.OrdinalIgnoreCase));
            }

            // 4. Sắp xếp
            if (IsSortDescending)
            {
                query = query.OrderByDescending(n => n.CreatedAt);
            }
            else
            {
                query = query.OrderBy(n => n.CreatedAt);
            }

            // Cập nhật ra UI
            FilteredNotifications.ReplaceRange(query);
        }

        // ============================================================
        // 4. XỬ LÝ SIGNALR (REALTIME)
        // ============================================================
        private void OnNewNotificationReceived(SignalRNotification notification)
        {
            if (notification == null) return;
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await _deviceNotificationService.ShowInAppNotificationAsync(notification);

                // Thêm vào danh sách GỐC
                _allNotifications.Add(notification);

                // Tính toán lại UI và số lượng
                RecalculateUnreadCount();
                ApplyFilters(); // ⚡ Quan trọng: Gọi lọc lại để hiển thị đúng vị trí
            });
        }

        private void OnNotificationUpdated(SignalRNotification updatedNotification)
        {
            if (updatedNotification == null) return;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Cập nhật trong danh sách GỐC
                var item = _allNotifications.FirstOrDefault(n => n.Id == updatedNotification.Id);
                if (item != null)
                {
                    item.Title = updatedNotification.Title;
                    item.Content = updatedNotification.Content;
                    item.IsRead = updatedNotification.IsRead;
                    item.ReadAt = updatedNotification.ReadAt;
                }
                else
                {
                    _allNotifications.Add(updatedNotification);
                }

                RecalculateUnreadCount();
                ApplyFilters(); // ⚡ Cập nhật lại danh sách hiển thị
            });
        }

        private void OnNotificationDeleted(Guid notificationId)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                // Xóa khỏi danh sách GỐC
                var item = _allNotifications.FirstOrDefault(n => n.Id == notificationId);
                if (item != null)
                {
                    _allNotifications.Remove(item);
                    RecalculateUnreadCount();
                    ApplyFilters(); // ⚡ Cập nhật lại danh sách hiển thị
                }
            });
        }

        private void RecalculateUnreadCount()
        {
            var newCount = _allNotifications.Count(n => !n.IsRead);
            UnreadCount = newCount;
            _notificationStateService.UnreadCount = newCount;
        }

        // ============================================================
        // 5. LOAD DATA
        // ============================================================
        public async Task LoadNotificationsAsync(bool forceRefresh = false)
        {
            if (!forceRefresh && _isDataLoaded && (DateTime.UtcNow - _lastLoadTime).TotalMinutes < CacheDurationMinutes) return;
            if (IsLoading || IsRefreshing) return;

            try
            {
                if (forceRefresh) IsRefreshing = true; else IsLoading = true;

                var currentUserId = await GetCurrentUserId();
                if (currentUserId == Guid.Empty) return;

                var response = await _localApi.GetNotificationsAsync(currentUserId.ToString(), 1, 100, "Id desc");

                if (response?.Success == true && response.Data?.Any() == true)
                {
                    var dataWrapper = response.Data.FirstOrDefault();
                    if (dataWrapper?.Result != null)
                    {
                        // Cập nhật danh sách GỐC
                        _allNotifications = dataWrapper.Result
                            .Select(n => n.ToSignalRNotification())
                            .ToList();

                        // Cập nhật UI
                        await MainThread.InvokeOnMainThreadAsync(() =>
                        {
                            RecalculateUnreadCount();
                            ApplyFilters(); // ⚡ Lần đầu tiên hiển thị data
                        });

                        _isDataLoaded = true;
                        _lastLoadTime = DateTime.UtcNow;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Lỗi LoadNotificationsAsync: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        public async Task RefreshNotificationsAsync()
        {
            await LoadNotificationsAsync(forceRefresh: true);
        }

        private async Task<Guid> GetCurrentUserId()
        {
            var userIdStr = await SecureStorage.GetAsync("userid");
            return Guid.TryParse(userIdStr, out var uid) ? uid : Guid.Empty;
        }

        [RelayCommand]
        public async Task MarkAsReadAsync(SignalRNotification notification)
        {
            if (notification == null || notification.IsRead) return;

            // Cập nhật Object (sẽ tự reflect lên UI nhờ Binding)
            notification.IsRead = true;
            notification.ReadAt = DateTime.Now;

            RecalculateUnreadCount();
            // ApplyFilters(); // Tuỳ chọn: Có muốn ẩn ngay lập tức khi đang lọc "Chưa đọc" không? Nếu muốn thì bỏ comment.

            try
            {
                var userId = await GetCurrentUserId();
                await _localApi.MarkNotificationAsReadAsync(notification.Id, userId);
            }
            catch { /* Ignore */ }
        }

        [RelayCommand]
        private async Task ViewNotificationAsync(SignalRNotification notification)
        {
            if (notification == null) return;

            // 1. Đánh dấu đã đọc
            if (!notification.IsRead)
            {
                // Gọi trực tiếp hàm MarkAsReadAsync trong cùng ViewModel
                await MarkAsReadAsync(notification);
            }

            // 2. ✅ XỬ LÝ ĐIỀU HƯỚNG (ActionUrl)
            if (!string.IsNullOrEmpty(notification.ActionUrl))
            {
                try
                {
                    Debug.WriteLine($"🚀 Navigating via ActionUrl: {notification.ActionUrl}");
                    await Shell.Current.GoToAsync(notification.ActionUrl);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"❌ Navigation Error: {ex.Message}");
                    // Nếu lỗi, quay về trang chi tiết mặc định
                    await GoToDefaultDetail(notification);
                }
            }
            else
            {
                // Nếu không có Url, mở trang chi tiết mặc định
                await GoToDefaultDetail(notification);
            }
        }

        // Hàm phụ trợ (đặt trong ViewModel luôn)
        private async Task GoToDefaultDetail(SignalRNotification notification)
        {
            await Shell.Current.GoToAsync(nameof(NotificationDetailPage), new Dictionary<string, object>
    {
        { "Notification", notification }
    });
        }
    }
}