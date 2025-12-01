using HRMApp.Services.Api;
using HRMApp.Helpers;
using Refit;
using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using System.Linq; // Thêm using System.Linq để dùng OrderByDescending

namespace HRMApp.View;

public partial class SalaryDetailPage : ContentPage
{
    private readonly ILocalApi _api;
    private readonly DateTime _month;
    public ObservableCollection<PayrollDailyItem> SalaryDetails { get; set; } = new();

    public SalaryDetailPage(DateTime month)
    {
        InitializeComponent();
        _api = ServiceHelper.GetService<ILocalApi>();
        _month = month;

        // Cập nhật tiêu đề trang
        this.Title = $"Chi tiết lương tháng {_month:MM/yyyy}";

        BindingContext = this;

        _ = LoadDailySalaryAsync();
    }

    private async Task LoadDailySalaryAsync()
    {
        try
        {
            var employeeIdStr = await SecureStorage.GetAsync("employeeid");
            if (string.IsNullOrEmpty(employeeIdStr))
            {
                await DisplayAlert("Lỗi", "Không tìm thấy mã nhân viên.", "OK");
                return;
            }

            Guid employeeId = Guid.Parse(employeeIdStr);
            string monthStr = _month.ToString("yyyy-MM");

            // 🧭 Gọi API BE (Sử dụng kiểu trả về PayrollDailyResponse mới)
            var apiResponse = await _api.GetPayrollDailyAsync(employeeId, monthStr);

            // Kiểm tra cấu trúc response wrapper
            if (!apiResponse.Success || apiResponse.Data == null || apiResponse.Data.Length == 0 || apiResponse.Data[0].Result == null)
            {
                await DisplayAlert("Thông báo", "Không có dữ liệu chi tiết lương tháng này.", "OK");
                SalaryDetails.Clear();
                return;
            }

            var dailyItems = apiResponse.Data[0].Result;

            SalaryDetails.Clear();
            foreach (var item in dailyItems)
            {
                // Ánh xạ đúng tên thuộc tính theo DTO đã cập nhật (từ BE)
                SalaryDetails.Add(new PayrollDailyItem
                {
                    Ngay = item.Ngay,
                    TrangThai = item.TrangThai,
                    SoCong = item.SoCong,      // Mới: SoCong
                    PhuCap = item.PhuCap,
                    Thuong = item.Thuong,
                    GioOtDuocDuyet = item.GioOtDuocDuyet,
                    GioOtThucTe = item.GioOtThucTe,
                    LuongOt = item.LuongOt,

                    Phat = item.Phat,
                    LuongNgay = item.LuongNgay, // Mới: LuongNgay
                    GhiChu = item.GhiChu
                });
            }

            // Sắp xếp theo ngày giảm dần (tùy chọn để hiển thị dễ nhìn)
            var sortedDetails = SalaryDetails.OrderByDescending(d => DateTime.Parse(d.Ngay)).ToList();

            SalaryDetails.Clear();
            foreach (var item in sortedDetails)
            {
                SalaryDetails.Add(item);
            }
        }
        catch (ApiException ex)
        {
            await DisplayAlert("Lỗi API", $"Lỗi: {ex.StatusCode} - {ex.Content ?? ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Lỗi", $"Lỗi không xác định: {ex.Message}", "OK");
        }
    }
}