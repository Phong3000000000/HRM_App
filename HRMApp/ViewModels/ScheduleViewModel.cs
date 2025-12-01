using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HRMApp.Model;
using HRMApp.Model.Dto;
using HRMApp.Services.Api;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HRMApp.ViewModels
{
    public partial class ScheduleViewModel : ObservableObject
    {
        private readonly ILocalApi _api;
        private DateTime _weekStart;

        [ObservableProperty]
        private string weekRange = string.Empty; // ✅ THÊM default value

        [ObservableProperty]
        private ObservableCollection<DaySchedule> days = new(); // ✅ THÊM default value

        [ObservableProperty]
        private bool isLoading;

        // ✅ THÊM: Property cho DatePicker
        [ObservableProperty]
        private DateTime selectedDate = DateTime.Today;

        public ScheduleViewModel(ILocalApi api)
        {
            _api = api;

            // Khởi tạo với tuần hiện tại
            SetWeekFromDate(DateTime.Today);
            
            // Gọi method async đúng cách
            _ = LoadWeekDataAsync();
        }

        // ✅ THÊM: Method helper để tính tuần từ ngày được chọn
        private void SetWeekFromDate(DateTime selectedDate)
        {
            // Tính toán để luôn bắt đầu từ Thứ 2 của tuần chứa ngày được chọn
            var diff = (7 + (selectedDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            _weekStart = selectedDate.AddDays(-1 * diff).Date;
            
            Debug.WriteLine($"📅 Ngày được chọn: {selectedDate:dd/MM/yyyy}");
            Debug.WriteLine($"📅 Thứ 2 của tuần: {_weekStart:dd/MM/yyyy}");
        }

        // ✅ SỬA: Cập nhật khi SelectedDate thay đổi
        partial void OnSelectedDateChanged(DateTime value)
        {
            Debug.WriteLine($"🔄 SelectedDate changed to: {value:dd/MM/yyyy}");
            SetWeekFromDate(value);
            _ = LoadWeekDataAsync();
        }

        [RelayCommand]
        public async Task NextWeek() // ✅ SỬA: Thành async Task
        {
            _weekStart = _weekStart.AddDays(7);
            
            // ✅ THÊM: Cập nhật SelectedDate để DatePicker đồng bộ
            SelectedDate = _weekStart.AddDays(3); // Chọn Thứ 5 của tuần mới
            
            await LoadWeekDataAsync();
        }

        [RelayCommand]
        public async Task PrevWeek() // ✅ SỬA: Thành async Task
        {
            _weekStart = _weekStart.AddDays(-7);
            
            // ✅ THÊM: Cập nhật SelectedDate để DatePicker đồng bộ
            SelectedDate = _weekStart.AddDays(3); // Chọn Thứ 5 của tuần mới
            
            await LoadWeekDataAsync();
        }

        // ✅ THÊM: Command cho nút "Hôm nay"
        [RelayCommand]
        public async Task GoToToday()
        {
            var today = DateTime.Today;
            
            // Cập nhật SelectedDate sẽ tự động trigger OnSelectedDateChanged
            SelectedDate = today;
        }

        // ✅ SỬA: Thành async Task thay vì async void
        public async Task LoadWeekDataAsync()
        {
            if (IsLoading) return;
            IsLoading = true;

            try
            {
                Debug.WriteLine("🔄 Bắt đầu tải dữ liệu lịch làm việc...");

                // 1. Xác định ngày bắt đầu và kết thúc tuần (Thứ 2 -> Chủ nhật)
                var weekEnd = _weekStart.AddDays(6);
                WeekRange = $"{_weekStart:dd/MM/yyyy} - {weekEnd:dd/MM/yyyy}";

                Debug.WriteLine($"📅 Khoảng thời gian: {WeekRange}");

                // 2. Lấy EmployeeId từ SecureStorage
                var empIdStr = await SecureStorage.GetAsync("employeeid");
                if (string.IsNullOrEmpty(empIdStr))
                {
                    Debug.WriteLine("Không tìm thấy employeeid trong SecureStorage");
                    await Application.Current?.MainPage?.DisplayAlert("Lỗi", "Không tìm thấy thông tin nhân viên. Vui lòng đăng nhập lại.", "OK");
                    return;
                }

                Debug.WriteLine($"EmployeeId: {empIdStr}");

                // 3. Chuẩn bị tham số gọi API (Format yyyy-MM-dd)
                var fromStr = _weekStart.ToString("yyyy-MM-dd");
                var toStr = weekEnd.ToString("yyyy-MM-dd");

                Debug.WriteLine($"🔍 Gọi API với từ {fromStr} đến {toStr}");

                // ✅ SỬA: Gọi API và thêm debug logs
                var response = await _api.GetWorkSchedulesAsync(Guid.Parse(empIdStr), fromStr, toStr, 100);

                Debug.WriteLine($"API Response - Success: {response?.Success}");
                Debug.WriteLine($"API Response - Data count: {response?.Data?.Count}");

                List<WorkScheduleDto> apiSchedules = new List<WorkScheduleDto>();

                // 🟢 QUAN TRỌNG: Xử lý cấu trúc JSON dạng mảng [ { "result": [...] } ]
                if (response != null && response.Success && response.Data != null && response.Data.Count > 0)
                {
                    // Lấy phần tử đầu tiên trong mảng Data
                    var dataWrapper = response.Data.FirstOrDefault();
                    Debug.WriteLine($"DataWrapper null: {dataWrapper == null}");

                    if (dataWrapper != null && dataWrapper.Result != null)
                    {
                        apiSchedules = dataWrapper.Result;
                        Debug.WriteLine($"Số lượng schedules từ API: {apiSchedules.Count}");
                        
                        // ✅ THÊM: Log chi tiết từng item
                        foreach (var schedule in apiSchedules)
                        {
                            Debug.WriteLine($"   - {schedule.Date}: {schedule.ShiftName} ({schedule.ShiftStartTime} - {schedule.ShiftEndTime})");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("DataWrapper hoặc Result null");
                    }
                }
                else
                {
                    Debug.WriteLine("Response không thành công hoặc Data null/empty");
                }

                // 4. Map dữ liệu API vào giao diện 7 ngày
                var tempDays = new ObservableCollection<DaySchedule>();

                // ✅ SỬA: Thay đổi cách tạo DaySchedule trong LoadWeekDataAsync method
                // Thay đổi phần này:
                for (int i = 0; i < 7; i++)
                {
                    var currentDate = _weekStart.AddDays(i);
                    var shifts = new ObservableCollection<ShiftItem>();

                    // Chuẩn hóa ngày hiện tại thành chuỗi yyyy-MM-dd để so sánh
                    string currentDateStr = currentDate.ToString("yyyy-MM-dd");

                    Debug.WriteLine($"Xử lý ngày {currentDateStr} ({GetVietnameseDay(currentDate.DayOfWeek)})");

                    // ✅ SỬA: Tìm các ca làm việc trong ngày này (So sánh chuỗi chính xác)
                    var schedulesToday = apiSchedules
                        .Where(s => !string.IsNullOrEmpty(s.Date) && s.Date == currentDateStr)
                        .ToList();

                    Debug.WriteLine($"Tìm thấy {schedulesToday.Count} ca làm việc");

                    if (schedulesToday.Any())
                    {
                        // Có lịch làm việc
                        foreach (var item in schedulesToday)
                        {
                            Debug.WriteLine($"   ➕ Thêm ca: {item.ShiftName}");

                            // Parse giờ từ chuỗi string để hiển thị đẹp (VD: 08:00 - 17:00)
                            string timeDisplay = FormatTimeRange(item.ShiftStartTime, item.ShiftEndTime);

                            shifts.Add(new ShiftItem
                            {
                                ShiftName = $"{item.ShiftName}\n[{timeDisplay}]",
                                Status = "Đã xếp lịch",

                                // Màu xanh lá nhạt cho ngày đi làm
                                BgColor = Color.FromArgb("#E5F7EB"),
                                StatusTextColor = Color.FromArgb("#1568b2"),
                                BorderColor = Colors.Transparent,
                                ShiftTextColor = Colors.Black
                            });
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"   ➕ Thêm ngày nghỉ");

                        // Không có lịch (Ngày nghỉ)
                        shifts.Add(new ShiftItem
                        {
                            ShiftName = "Không có lịch",
                            Status = "Nghỉ",

                            // Màu xám cho ngày nghỉ
                            BgColor = Color.FromArgb("#F0F0F0"),
                            StatusTextColor = Colors.Gray,
                            ShiftTextColor = Colors.Gray,
                            BorderColor = Colors.Transparent
                        });
                    }

                    // ✅ THÊM: Kiểm tra ngày được chọn
                    bool isSelectedDate = currentDate.Date == SelectedDate.Date;

                    // Thêm ngày vào danh sách hiển thị
                    tempDays.Add(new DaySchedule
                    {
                        DayOfWeek = GetVietnameseDay(currentDate.DayOfWeek),
                        Date = currentDate.ToString("dd/MM"), // ✅ SỬA: Bỏ dấu chấm text
                        Shifts = shifts,

                        // ✅ THÊM: Thuộc tính cho border tùy chỉnh
                        IsSelected = isSelectedDate,
                        SelectedBorderColor = Color.FromArgb("#FF6B35"), // Màu cam đỏ
                        SelectedBorderRadius = 12,
                        SelectedBorderThickness = 2,
                        SelectedBackgroundColor = Color.FromArgb("#FFF3F0") // Nền nhạt
                    });
                }

                Debug.WriteLine($"✅ Tạo được {tempDays.Count} ngày cho UI");

                // ✅ SỬA: Cập nhật lên giao diện trên MainThread
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Days.Clear();
                    foreach (var day in tempDays)
                    {
                        Days.Add(day);
                    }
                });

                Debug.WriteLine($"✅ Đã cập nhật UI với {Days.Count} ngày");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Lỗi tải lịch: {ex.Message}");
                Debug.WriteLine($"❌ StackTrace: {ex.StackTrace}");
                
                // ✅ SỬA: Hiển thị lỗi cho user
                await Application.Current?.MainPage?.DisplayAlert("Lỗi", $"Không thể tải lịch làm việc: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
                Debug.WriteLine($"🏁 Hoàn thành LoadWeekDataAsync. IsLoading = {IsLoading}");
            }
        }

        // Helper: Chuyển đổi tên thứ sang tiếng Việt
        private string GetVietnameseDay(DayOfWeek day)
        {
            return day switch
            {
                DayOfWeek.Monday => "Thứ 2",
                DayOfWeek.Tuesday => "Thứ 3",
                DayOfWeek.Wednesday => "Thứ 4",
                DayOfWeek.Thursday => "Thứ 5",
                DayOfWeek.Friday => "Thứ 6",
                DayOfWeek.Saturday => "Thứ 7",
                DayOfWeek.Sunday => "CN",
                _ => ""
            };
        }

        // Helper: Format giờ hiển thị từ chuỗi (VD input: "08:00:00" -> output: "08:00")
        private string FormatTimeRange(string startStr, string endStr)
        {
            try
            {
                if (TimeSpan.TryParse(startStr, out var start) && TimeSpan.TryParse(endStr, out var end))
                {
                    return $"{start:hh\\:mm} - {end:hh\\:mm}";
                }
                return $"{startStr} - {endStr}";
            }
            catch
            {
                return $"{startStr} - {endStr}";
            }
        }

    
    }
}