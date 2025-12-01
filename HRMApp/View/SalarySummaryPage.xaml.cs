using HRMApp.Helpers;
using HRMApp.Services.Api;
using Microsoft.Maui.Controls;
using Refit;
using System;
using System.Threading.Tasks;
using System.Linq;

#if ANDROID
using Android.Widget;
using Android.Views;
#endif

namespace HRMApp.View;

public partial class SalarySummaryPage : ContentPage
{
    private readonly ILocalApi _api;

    // 🔹 Thuộc tính binding (Tổng hợp)
    public DateTime SelectedMonth { get; set; } = DateTime.Now;
    public string CurrentPeriod { get; set; }
    public string AssignedDays { get; set; }
    public string ActualDays { get; set; }
    public string LateCount { get; set; }
    public string EarlyLeaveCount { get; set; }
    public string LeaveCount { get; set; }

    public string TotalSalary { get; set; }
    public string Reward { get; set; }
    public string FineOther { get; set; }
    public string OvertimeSalary { get; set; }
    public string Allowance { get; set; }
    public string Insurance { get; set; }
    public string InsuranceSalaryAmount { get; set; }
    public string NetSalary { get; set; }
    public string AdvanceSalary { get; set; } = "0";
    public string FinalSalary { get; set; }

    // 🔹 Thuộc tính binding (Chi tiết mới)
    public string LuongMotNgayCong { get; set; }
    public string LuongMotGio { get; set; }
    public string SoGioOT { get; set; }
    public string SoGioOTThucTe { get; set; }

    public string HeSoOT { get; set; }
    public string BhxhRate { get; set; } // Tỷ lệ BHXH
    public string BhytRate { get; set; } // Tỷ lệ BHYT
    public string BhtnRate { get; set; } // Tỷ lệ BHTN


    public SalarySummaryPage()
    {
        InitializeComponent();
        _api = ServiceHelper.GetService<ILocalApi>();
        BindingContext = this;

        UpdatePeriodLabel();
        _ = LoadDataAsync();
    }

    private void UpdatePeriodLabel()
    {
        var firstDay = new DateTime(SelectedMonth.Year, SelectedMonth.Month, 1);
        var lastDay = firstDay.AddMonths(1).AddDays(-1);
        CurrentPeriod = $"{firstDay:dd/MM/yyyy} - {lastDay:dd/MM/yyyy}";
        OnPropertyChanged(nameof(CurrentPeriod));
    }

    private void ResetData()
    {
        AssignedDays = "0";
        ActualDays = "0";
        LateCount = "0";
        LeaveCount = "0";
        TotalSalary = "0";
        Reward = "0";
        FineOther = "0";
        OvertimeSalary = "0";
        Allowance = "0";
        Insurance = "0";
        NetSalary = "0";
        AdvanceSalary = "0";
        FinalSalary = "0";
        InsuranceSalaryAmount = "0";

        // Reset các trường chi tiết
        LuongMotNgayCong = "0";
        LuongMotGio = "0";
        SoGioOT = "0";
        HeSoOT = "0";
        BhxhRate = "0.00%";
        BhytRate = "0.00%";
        BhtnRate = "0.00%";

        UpdateUIProperties();
    }

    private void UpdateUIProperties()
    {
        OnPropertyChanged(nameof(AssignedDays));
        OnPropertyChanged(nameof(ActualDays));
        OnPropertyChanged(nameof(LateCount));
        OnPropertyChanged(nameof(EarlyLeaveCount));
        OnPropertyChanged(nameof(LeaveCount));
        OnPropertyChanged(nameof(TotalSalary));
        OnPropertyChanged(nameof(Reward));
        OnPropertyChanged(nameof(FineOther));
        OnPropertyChanged(nameof(OvertimeSalary));
        OnPropertyChanged(nameof(Allowance));
        OnPropertyChanged(nameof(Insurance));
        OnPropertyChanged(nameof(InsuranceSalaryAmount)); 
        OnPropertyChanged(nameof(NetSalary));
        OnPropertyChanged(nameof(FinalSalary));
        OnPropertyChanged(nameof(AdvanceSalary));

        // Cập nhật các thuộc tính chi tiết mới
        OnPropertyChanged(nameof(LuongMotNgayCong));
        OnPropertyChanged(nameof(LuongMotGio));
        OnPropertyChanged(nameof(SoGioOT));
        OnPropertyChanged(nameof(SoGioOTThucTe));
        OnPropertyChanged(nameof(HeSoOT));
        OnPropertyChanged(nameof(BhxhRate));
        OnPropertyChanged(nameof(BhytRate));
        OnPropertyChanged(nameof(BhtnRate));
    }


    private async Task LoadDataAsync()
    {
        try
        {
            var employeeIdStr = await SecureStorage.GetAsync("employeeid");
            if (string.IsNullOrEmpty(employeeIdStr))
            {
                await DisplayAlert("Lỗi", "Không tìm thấy mã nhân viên", "OK");
                return;
            }

            Guid employeeId = Guid.Parse(employeeIdStr);
            string month = SelectedMonth.ToString("yyyy-MM");

            var apiResponse = await _api.GetPayrollPerformanceAsync(employeeId, month);

            if (!apiResponse.Success || apiResponse.Data?.FirstOrDefault()?.Result?.FirstOrDefault() == null)
            {
                await DisplayAlert("Thông báo", "Không có dữ liệu tháng này", "OK");
                ResetData();
                return;
            }

            var result = apiResponse.Data.FirstOrDefault().Result.FirstOrDefault();
            var luong = result.Luong;

            // 🧾 Gán dữ liệu Chấm công
            AssignedDays = result.ChamCong.SoCongPhanCong.ToString();
            ActualDays = result.ChamCong.SoCongThucTe.ToString("N3");
            LateCount = result.ChamCong.SoLanDiMuon.ToString();
            EarlyLeaveCount = result.ChamCong.SoLanVeSom.ToString();
            LeaveCount = result.ChamCong.SoLanVang.ToString();

            // Tính Lương gộp (Gross Salary)
            decimal luongCoBanLamDuoc = luong.LuongMotNgayCong * result.ChamCong.SoCongThucTe;
            decimal luongGop = luongCoBanLamDuoc + luong.TongPhuCap + luong.TongThuong + luong.LuongOT - luong.TongPhat;
            decimal luongThucNhanCuoi = luong.LuongThucNhan;


            // ÁNH XẠ VÀ LÀM TRÒN
            TotalSalary = Math.Round(luongGop, 0).ToString("N0");
            Reward = Math.Round(luong.TongThuong, 0).ToString("N0");
            FineOther = Math.Round(luong.TongPhat, 0).ToString("N0");
            OvertimeSalary = Math.Round(luong.LuongOT, 0).ToString("N0");
            Allowance = Math.Round(luong.TongPhuCap, 0).ToString("N0");
            Insurance = Math.Round(luong.BaoHiem, 0).ToString("N0");

            // Chi tiết mới
            LuongMotNgayCong = Math.Round(luong.LuongMotNgayCong, 0).ToString("N0");
            LuongMotGio = Math.Round(luong.LuongMotGio, 0).ToString("N0");
            SoGioOT = luong.SoGioOT.ToString("N1");
            HeSoOT = luong.HeSoOT.ToString("N1");
            SoGioOTThucTe = luong.TongGioOTThucTe.ToString("N1");


            // Tỷ lệ Bảo hiểm (Nhân 100 và giữ 2 số thập phân)
            InsuranceSalaryAmount = Math.Round(result.ThongTinNhanVien.InsuranceSalary, 0).ToString("N0");
            BhxhRate = (luong.Bhxh * 100).ToString("N2") + "%";
            BhytRate = (luong.Bhyt * 100).ToString("N2") + "%";
            BhtnRate = (luong.Bhtn * 100).ToString("N2") + "%";

            // Lương ròng và Ứng lương
            NetSalary = Math.Round(luongThucNhanCuoi, 0).ToString("N0");
            decimal advanceAmount = 0;
            AdvanceSalary = advanceAmount.ToString("N0");
            FinalSalary = Math.Round(luongThucNhanCuoi - advanceAmount, 0).ToString("N0");

            // 🔁 Cập nhật UI
            UpdateUIProperties();
        }
        catch (ApiException ex)
        {
            await DisplayAlert("Lỗi API", $"Lỗi: {ex.StatusCode} - {ex.Content ?? ex.Message}", "OK");
            ResetData();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Lỗi", $"Lỗi không xác định: {ex.Message}", "OK");
            ResetData();
        }
    }

    // 🔹 Chuyển tháng bằng picker
    private async void OnMonthChanged(object sender, DateChangedEventArgs e)
    {
        SelectedMonth = e.NewDate;
        UpdatePeriodLabel();
        await LoadDataAsync();
    }

    // 🔹 Nút chuyển tháng (trái/phải)
    private async void OnPrevMonthClicked(object sender, EventArgs e)
    {
        SelectedMonth = SelectedMonth.AddMonths(-1);
        MonthPicker.Date = SelectedMonth;
        UpdatePeriodLabel();
        await LoadDataAsync();
    }

    private async void OnNextMonthClicked(object sender, EventArgs e)
    {
        SelectedMonth = SelectedMonth.AddMonths(1);
        MonthPicker.Date = SelectedMonth;
        UpdatePeriodLabel();
        await LoadDataAsync();
    }

    // 🔹 Khi bấm “Xem chi tiết”
    private async void OnDetailClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SalaryDetailPage(SelectedMonth));
    }

    //khi bấm chon tháng
    private void OnPickMonthClicked(object sender, EventArgs e)
    {
#if ANDROID
        if (MonthPicker?.Handler?.PlatformView is Android.Widget.EditText nativeEditText)
        {
            nativeEditText.PerformClick();
        }
#else
        MonthPicker.Focus();
#endif
    }
}