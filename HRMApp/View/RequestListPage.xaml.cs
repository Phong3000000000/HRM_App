using HRMApp.Services.Api;
using HRMApp.Helpers;
using System.Collections.ObjectModel;
using HRMApp.Services.Notification;
using HRMApp.Model.Notification;
using System.Diagnostics;
using HRMApp.Model; // 👈 BẮT BUỘC

namespace HRMApp.View;

public partial class RequestListPage : ContentPage
{
    public ObservableCollection<RequestItem> LeaveRequests { get; set; } = new ObservableCollection<RequestItem>();
    private List<RequestItem> _allRequests = new List<RequestItem>();

    private readonly ILocalApi _localApi;
    private ISignalRService _signalRService;

    public RequestListPage()
    {
        InitializeComponent();

        // --- KHỞI TẠO DỮ LIỆU PICKER ---

        // 1. Trạng thái
        pickerStatus.ItemsSource = new List<string> { "Tất cả trạng thái", "Chờ duyệt (Pending)", "Đã duyệt (Approved)", "Từ chối (Rejected)" };
        pickerStatus.SelectedIndex = 0;

        // 2. Loại đơn
        pickerCategory.ItemsSource = new List<string> { "Tất cả loại", "Làm thêm (OT)", "Nghỉ phép (Leave)" };
        pickerCategory.SelectedIndex = 0;

        // 3. ✅ SẮP XẾP (Mới thêm)
        pickerSort.ItemsSource = new List<string>
        {
            "Mới nhất trước",  // Index 0 (Giảm dần)
            "Cũ nhất trước"    // Index 1 (Tăng dần)
        };
        pickerSort.SelectedIndex = 0; // Mặc định là Mới nhất

        // -------------------------------

        _localApi = ServiceHelper.GetService<ILocalApi>();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadRequestsAsync();
        RegisterSignalR();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        UnregisterSignalR();
    }

    private async Task LoadRequestsAsync()
    {
        try
        {
            var employeeIdStr = await SecureStorage.GetAsync("employeeid");
            // Mặc định API tải về đã sắp xếp Mới nhất (CreatedAt desc)
            var response = await _localApi.GetRequestsAsync(q: employeeIdStr, pageSize: 100, sort: "CreatedAt desc");

            if (response.Success && response.Data != null && response.Data.Any())
            {
                var results = response.Data.First().Result;
                _allRequests.Clear();

                foreach (var item in results)
                {
                    _allRequests.Add(item);
                }
                ApplyFilters();
            }
            else
            {
                _allRequests.Clear();
                LeaveRequests.Clear();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Lỗi tải request: {ex.Message}");
        }
    }

    // Sự kiện chung cho tất cả các bộ lọc (bao gồm cả Sort)
    private void OnFilterChanged(object sender, EventArgs e)
    {
        ApplyFilters();
    }

    private void ApplyFilters()
    {
        var query = _allRequests.AsEnumerable();

        // 1. Tìm kiếm
        if (!string.IsNullOrWhiteSpace(txtSearch.Text))
        {
            var keyword = txtSearch.Text.Trim().ToLower();
            query = query.Where(x =>
                            // Tìm trong Tiêu đề (Title)
                            (x.Title != null && x.Title.ToLower().Contains(keyword)) ||
                            // HOẶC Tìm trong Mô tả (Description)
                            (x.Description != null && x.Description.ToLower().Contains(keyword))
                        );
        }

        // 2. Lọc Trạng thái
        int statusIdx = pickerStatus.SelectedIndex;
        if (statusIdx > 0)
        {
            query = query.Where(x =>
            {
                if (statusIdx == 1) return x.Status == RequestStatus.pending;
                if (statusIdx == 2) return x.Status == RequestStatus.approved;
                if (statusIdx == 3) return x.Status == RequestStatus.rejected;
                return true;
            });
        }

        // 3. Lọc Loại đơn
        int catIdx = pickerCategory.SelectedIndex;
        if (catIdx > 0)
        {
            query = query.Where(x =>
            {
                if (catIdx == 1) return x.Category == RequestCategory.ot;
                if (catIdx == 2) return x.Category == RequestCategory.leave;
                return true;
            });
        }

        // 4. Lọc Ngày
        if (chkEnableDate.IsChecked)
        {
            var selectedDate = pickerDate.Date.Date;
            query = query.Where(x => x.FromDate.HasValue && x.FromDate.Value.Date == selectedDate);
        }

        // 5. ✅ LOGIC SẮP XẾP (Mới thêm)
        // Vì CreatedAt là string chuẩn "yyyy-MM-dd...", ta có thể so sánh chuỗi trực tiếp mà không cần Parse
        if (pickerSort.SelectedIndex == 1)
        {
            // Cũ nhất trước (Tăng dần)
            query = query.OrderBy(x => x.CreatedAt);
        }
        else
        {
            // Mới nhất trước (Giảm dần - Mặc định)
            query = query.OrderByDescending(x => x.CreatedAt);
        }

        // 6. Hiển thị kết quả
        LeaveRequests.Clear();
        foreach (var item in query)
        {
            LeaveRequests.Add(item);
        }
    }

    // --- SIGNALR ---
    private void RegisterSignalR()
    {
        try
        {
            _signalRService = ServiceHelper.GetService<ISignalRService>();
            if (_signalRService != null)
            {
                _signalRService.OnNotificationReceived -= HandleRequestUpdate;
                _signalRService.OnNotificationReceived += HandleRequestUpdate;
            }
        }
        catch { }
    }

    private void UnregisterSignalR()
    {
        if (_signalRService != null) _signalRService.OnNotificationReceived -= HandleRequestUpdate;
    }

    private void HandleRequestUpdate(SignalRNotification notification)
    {
        MainThread.BeginInvokeOnMainThread(async () => await LoadRequestsAsync());
    }

    private async void OnAddLeaveClicked(object sender, EventArgs e)
    {
        var addPage = ServiceHelper.GetService<RequestFormPage>();
        await Navigation.PushAsync(addPage);
    }
}