using HRMApp.Model;
using HRMApp.Services.Api;
using Microsoft.Maui.ApplicationModel;
using Refit;
using System;
using System.Diagnostics;

namespace HRMApp.View;

public partial class RequestFormPage : ContentPage
{
    private readonly ILocalApi _localApi;
    private readonly TimeSpan WorkEndTime = new TimeSpan(17, 0, 0); // 5:00 PM

    public RequestFormPage(ILocalApi localApi)
    {
        InitializeComponent();
        _localApi = localApi;

        // Khởi tạo các giá trị mặc định
        FromDatePicker.Date = DateTime.Today;
        ToDatePicker.Date = DateTime.Today;
        OvertimeDatePicker.Date = DateTime.Today;
    }

    private void OnCategoryChanged(object sender, EventArgs e)
    {
        var selectedCategory = CategoryPicker.SelectedItem?.ToString();

        // Hiển thị/ẩn các frame tương ứng
        LeaveFrame.IsVisible = selectedCategory == "Leave";
        OvertimeFrame.IsVisible = selectedCategory == "Overtime";

        // Cập nhật text button
        SubmitButton.Text = selectedCategory == "Leave"
            ? "Gửi yêu cầu nghỉ phép"
            : "Gửi yêu cầu làm thêm giờ";

        // Enable/disable submit button
        SubmitButton.IsEnabled = !string.IsNullOrEmpty(selectedCategory);
    }

    private void OnOvertimeHoursChanged(object sender, TextChangedEventArgs e)
    {
        CalculateOvertimeTime();
    }

    private void CalculateOvertimeTime()
    {
        if (double.TryParse(OvertimeHoursEntry.Text, out double hours) && hours > 0)
        {
            var startTime = WorkEndTime; // Bắt đầu từ 17:00
            var endTime = startTime.Add(TimeSpan.FromHours(hours));

            CalculatedTimeLabel.Text = $"Từ: {startTime:hh\\:mm} Đến: {endTime:hh\\:mm}";
        }
        else
        {
            CalculatedTimeLabel.Text = "Từ: -- Đến: --";
        }
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var selectedCategory = CategoryPicker.SelectedItem?.ToString();

        if (string.IsNullOrEmpty(selectedCategory))
        {
            await DisplayAlert("Lỗi", "Vui lòng chọn loại yêu cầu", "OK");
            return;
        }

        // Validate theo từng loại
        if (!await ValidateInput(selectedCategory))
            return;

        ((Button)sender).IsEnabled = false;

        try
        {
            // ✅ SỬA: Xử lý nullable SecureStorage
            string? employeeIdString = await SecureStorage.GetAsync("employeeid");
            if (string.IsNullOrEmpty(employeeIdString) || !Guid.TryParse(employeeIdString, out Guid employeeId))
            {
                await DisplayAlert("Lỗi", "Không tìm thấy ID nhân viên. Vui lòng đăng nhập lại.", "OK");
                return;
            }

            // ✅ THÊM DEBUG: In thông tin trước khi gửi
            Debug.WriteLine("=== THÔNG TIN TẠO YÊU CẦU ===");
            Debug.WriteLine($"EmployeeId: {employeeId}");
            Debug.WriteLine($"Category: {selectedCategory}");

            var requestDto = selectedCategory == "Leave"
                ? CreateLeaveRequest(employeeId)
                : CreateOvertimeRequest(employeeId);

            // ✅ THÊM DEBUG: In DTO được gửi đi
            Debug.WriteLine($"DTO được gửi:");
            Debug.WriteLine($"  EmployeeId: {requestDto.EmployeeId}");
            Debug.WriteLine($"  Title: {requestDto.Title}");
            Debug.WriteLine($"  Description: {requestDto.Description}");
            Debug.WriteLine($"  Category: {requestDto.Category}");
            Debug.WriteLine($"  FromDate: {requestDto.FromDate:yyyy-MM-dd}");
            Debug.WriteLine($"  ToDate: {requestDto.ToDate?.ToString("yyyy-MM-dd") ?? "null"}");
            Debug.WriteLine($"  StartTime: {requestDto.StartTime}");
            Debug.WriteLine($"  EndTime: {requestDto.EndTime}");

            var response = await _localApi.CreateRequestAsync(requestDto);

            if (response.Success)
            {
                string successMsg = selectedCategory == "Leave"
                    ? "Đã gửi yêu cầu nghỉ phép thành công."
                    : "Đã gửi yêu cầu làm thêm giờ thành công.";

                await DisplayAlert("Thành công", response.Message ?? successMsg, "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Lỗi", response.Message ?? "Yêu cầu thất bại.", "OK");
            }
        }
        catch (ApiException apiEx)
        {
            string errorMsg = $"Lỗi API: {apiEx.StatusCode}";
            try
            {
                // ✅ THAY ĐỔI: Parse JSON error để lấy message từ server
                if (apiEx.HasContent && !string.IsNullOrEmpty(apiEx.Content))
                {
                    Debug.WriteLine($"Chi tiết lỗi từ server: {apiEx.Content}");
                    
                    // Thử parse JSON error với format từ server
                    using var document = System.Text.Json.JsonDocument.Parse(apiEx.Content);
                    var root = document.RootElement;
                    
                    // Kiểm tra các field có thể chứa message
                    if (root.TryGetProperty("message", out var messageElement))
                    {
                        errorMsg = messageElement.GetString() ?? errorMsg;
                    }
                    else if (root.TryGetProperty("Message", out var capitalMessageElement))
                    {
                        errorMsg = capitalMessageElement.GetString() ?? errorMsg;
                    }
                    else
                    {
                        // Fallback: thử deserialize thành ApiResponse
                        var errorResponse = System.Text.Json.JsonSerializer.Deserialize<ApiResponse>(apiEx.Content);
                        errorMsg = errorResponse?.Message ?? errorMsg;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Không thể parse lỗi: {ex.Message}");
                // Giữ nguyên errorMsg mặc định nếu không parse được
            }

            await DisplayAlert("Lỗi API", errorMsg, "OK");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Lỗi chung: {ex.Message}");
            await DisplayAlert("Lỗi kết nối", $"Không thể kết nối đến máy chủ.\n{ex.Message}", "OK");
        }
        finally
        {
            ((Button)sender).IsEnabled = true;
        }
    }

    private async Task<bool> ValidateInput(string category)
    {
        if (string.IsNullOrWhiteSpace(ReasonEditor.Text))
        {
            await DisplayAlert("Lỗi", "Vui lòng nhập lý do", "OK");
            return false;
        }

        if (category == "Leave")
        {
            if (FromSessionPicker.SelectedItem == null ||
                ToSessionPicker.SelectedItem == null)
            {
                await DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ thông tin nghỉ phép", "OK");
                return false;
            }
        }
        else if (category == "Overtime")
        {
            if (string.IsNullOrWhiteSpace(OvertimeHoursEntry.Text) ||
                !double.TryParse(OvertimeHoursEntry.Text, out double hours) ||
                hours <= 0)
            {
                await DisplayAlert("Lỗi", "Vui lòng nhập số giờ OT hợp lệ", "OK");
                return false;
            }
        }

        return true;
    }

    private CreateRequestDto CreateLeaveRequest(Guid employeeId)
    {
        // ✅ SỬA: Xử lý null safety
        string? fromSession = FromSessionPicker.SelectedItem?.ToString();
        string? toSession = ToSessionPicker.SelectedItem?.ToString();
        
        if (fromSession == null || toSession == null)
        {
            throw new InvalidOperationException("Vui lòng chọn đầy đủ thông tin buổi nghỉ");
        }
        
        string startTime = fromSession == "Sáng" ? "08:00:00" : "13:00:00";
        string endTime = toSession == "Chiều" ? "17:00:00" : "12:00:00";

        return new CreateRequestDto
        {
            EmployeeId = employeeId,
            Title = "Yêu cầu nghỉ phép",
            Description = ReasonEditor.Text ?? string.Empty,
            Category = 1,
            // ✅ SỬA: Chuyển DateTime thành DateOnly để phù hợp với server
            FromDate = DateOnly.FromDateTime(FromDatePicker.Date),
            ToDate = DateOnly.FromDateTime(ToDatePicker.Date),
            StartTime = startTime,
            EndTime = endTime
        };
    }

    private CreateRequestDto CreateOvertimeRequest(Guid employeeId)
    {
        if (!double.TryParse(OvertimeHoursEntry.Text, out double hours))
        {
            throw new InvalidOperationException("Số giờ OT không hợp lệ");
        }
        
        var startTime = WorkEndTime; // 17:00:00
        var endTime = startTime.Add(TimeSpan.FromHours(hours));

        return new CreateRequestDto
        {
            EmployeeId = employeeId,
            Title = $"Làm thêm {hours} giờ",
            Description = ReasonEditor.Text ?? string.Empty,
            Category = 0,
            // ✅ SỬA: Chuyển DateTime thành DateOnly để phù hợp với server
            FromDate = DateOnly.FromDateTime(OvertimeDatePicker.Date),
            ToDate = DateOnly.FromDateTime(OvertimeDatePicker.Date),
            StartTime = startTime.ToString(@"hh\:mm\:ss"),
            EndTime = endTime.ToString(@"hh\:mm\:ss")
        };
    }
}