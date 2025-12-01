using HRMApp.Services.Api;
using HRMApp.Helpers;
using Microsoft.Maui.ApplicationModel;

namespace HRMApp.View
{
    public partial class ReportSelectionPage : ContentPage
    {
        private readonly IHrmApi _api;
        private List<SalaryRecord> _salaryList;
        private SalaryRecord _selectedSalary;
        private Guid _currentEmployeeId;

        public ReportSelectionPage()
        {
            InitializeComponent();
            _api = ServiceHelper.GetService<IHrmApi>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadSalariesAsync();
        }

        private async Task LoadSalariesAsync()
        {
            try
            {
                SetLoading(true);

                // 1. Lấy EmployeeCode từ SecureStorage
                var employeeCode = await SecureStorage.GetAsync("code");
                var employeeIdStr = await SecureStorage.GetAsync("employeeid");

                if (string.IsNullOrEmpty(employeeCode))
                {
                    await DisplayAlert("Lỗi", "Không tìm thấy mã nhân viên. Vui lòng đăng nhập lại.", "OK");
                    return;
                }

                if (Guid.TryParse(employeeIdStr, out Guid empId))
                {
                    _currentEmployeeId = empId;
                }

                // 2. Gọi API lấy danh sách lương
                // Lưu ý: API trả về danh sách đã chốt (processed/locked)
                var response = await _api.GetSalariesAsync(q: employeeCode);

                if (response != null && response.Success && response.Data != null && response.Data.Count > 0)
                {
                    _salaryList = response.Data[0].Result;

                    // Gán vào Picker
                    SalaryPicker.ItemsSource = _salaryList;
                }
                else
                {
                    // Không có dữ liệu hoặc lỗi
                    SalaryPicker.ItemsSource = new List<SalaryRecord>();
                    await DisplayAlert("Thông báo", "Chưa có dữ liệu lương nào được chốt.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Lỗi", $"Không thể tải danh sách lương: {ex.Message}", "OK");
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void OnSalarySelected(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            if (picker.SelectedIndex != -1)
            {
                _selectedSalary = (SalaryRecord)picker.SelectedItem;
                // Không cần xử lý BtnDownloadPayslip.IsEnabled = true ở đây nữa
            }
            else
            {
                _selectedSalary = null;
            }
        }

        private async void OnDownloadPayslipClicked(object sender, EventArgs e)
        {
            if (_selectedSalary == null)
            {
                await DisplayAlert("Thông báo", "Vui lòng chọn kỳ lương trước khi tải.", "OK");
                return; // Dừng lại, không thực hiện tiếp
            }

            try
            {
                SetLoading(true);

                // Gọi API lấy link PDF Phiếu lương
                var response = await _api.GetPayslipReportDownloadUrl(_selectedSalary.Id);

                if (response != null && response.Success && !string.IsNullOrEmpty(response.DownloadUrl))
                {
                    await Launcher.Default.OpenAsync(new Uri(response.DownloadUrl));
                }
                else
                {
                    await DisplayAlert("Lỗi", response?.Message ?? "Không thể tạo báo cáo.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Lỗi", $"Lỗi khi tải báo cáo: {ex.Message}", "OK");
            }
            finally
            {
                SetLoading(false);
            }
        }

        private async void OnDownloadProfileClicked(object sender, EventArgs e)
        {
            // Tái sử dụng logic tải hồ sơ nhân viên
            if (_currentEmployeeId == Guid.Empty) return;

            try
            {
                SetLoading(true);
                var response = await _api.GetProfileReportDownloadUrl(_currentEmployeeId);
                if (response != null && response.Success && !string.IsNullOrEmpty(response.DownloadUrl))
                {
                    await Launcher.Default.OpenAsync(new Uri(response.DownloadUrl));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Lỗi", ex.Message, "OK");
            }
            finally
            {
                SetLoading(false);
            }
        }

        private void SetLoading(bool isLoading)
        {
            LoadingOverlay.IsVisible = isLoading;
        }
    }
}