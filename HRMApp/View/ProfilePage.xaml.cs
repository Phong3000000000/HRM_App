using HRMApp.Services.Api;
using HRMApp.Helpers;
using Android.Gms.Common.Apis;

namespace HRMApp.View
{
    public partial class ProfilePage : ContentPage
    {
        private readonly IHrmApi _api;
        private readonly ILocalApi _localapi;
        private Guid _currentEmployeeId;
        public ProfilePage()
        {
            InitializeComponent();
            _api = ServiceHelper.GetService<IHrmApi>();
            _localapi = ServiceHelper.GetService<ILocalApi>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadUserInfoAsync();
        }

        private async Task LoadUserInfoAsync()
        {
            var username = await SecureStorage.GetAsync("username");
            var fullname = await SecureStorage.GetAsync("fullname");
            var code = await SecureStorage.GetAsync("code");
            var role = await SecureStorage.GetAsync("role");
            var email = await SecureStorage.GetAsync("email");
            var phone = await SecureStorage.GetAsync("phone");
            var address = await SecureStorage.GetAsync("address");
            var avatarUrl = await SecureStorage.GetAsync("avatar");
            var employeeIdStr = await SecureStorage.GetAsync("employeeid");
            var status = await SecureStorage.GetAsync("status");
            var hiredate = await SecureStorage.GetAsync("hiredate");
            var department = await SecureStorage.GetAsync("departmentname");
            var position = await SecureStorage.GetAsync("positionname");
            // Cần đảm bảo employeeId hợp lệ trước khi sử dụng
            if (Guid.TryParse(employeeIdStr, out Guid employeeId))
            {
                _currentEmployeeId = employeeId;
            }
            else
            {
                // Xử lý trường hợp không có EmployeeId (có thể là lỗi, hoặc cần fetch lại thông tin user)
                await DisplayAlert("Lỗi", "Không tìm thấy mã nhân viên. Vui lòng đăng nhập lại.", "OK");
                // Quay về trang đăng nhập nếu cần
                return;
            }
            // Gán thông tin
            UsernameLabel.Text = username ?? "Chưa có tên đăng nhập";
            StatusLabel.Text = status ?? "OK";
            HiredateLabel.Text = hiredate ?? "Chưa có ngày vào làm";
            FullnameLabel.Text = fullname ?? "Chưa có họ tên";
            Code.Text = code ?? "Chưa có phòng ban";
            RoleLabel.Text = role ?? "Chưa có vai trò";
            EmailLabel.Text = email ?? "Chưa có email";
            PhoneLabel.Text = phone ?? "Chưa có SĐT";
            AddressLabel.Text = address ?? "Chưa có địa chỉ";
            DepartmentLabel.Text = department ?? "Chưa có phòng ban";
            PositionLabel.Text = position ?? "Chưa có có vị trí trong phòng ban";

            // Avatar: hiển thị ảnh nếu có, ngược lại hiển thị chữ cái đầu
            if (!string.IsNullOrEmpty(avatarUrl))
            {
                AvatarImage.IsVisible = true;
                AvatarBorder.IsVisible = false;

                try
                {
                    AvatarImage.Source = ImageSource.FromUri(new Uri(avatarUrl));
                }
                catch
                {
                    // Nếu link avatar không hợp lệ thì fallback về chữ cái đầu
                    AvatarImage.IsVisible = false;
                    AvatarBorder.IsVisible = true;
                    var firstLetter = (!string.IsNullOrEmpty(fullname)
                        ? fullname[0]
                        : (!string.IsNullOrEmpty(username) ? username[0] : 'U')).ToString().ToUpper();
                    AvatarLabel.Text = firstLetter;
                }
            }
            else
            {
                AvatarImage.IsVisible = false;
                AvatarBorder.IsVisible = true;

                var firstLetter = (!string.IsNullOrEmpty(fullname)
                    ? fullname[0]
                    : (!string.IsNullOrEmpty(username) ? username[0] : 'U')).ToString().ToUpper();

                AvatarLabel.Text = firstLetter;
            }
        }


        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Đăng xuất", "Bạn có chắc muốn đăng xuất không?", "Có", "Không");
            if (!confirm)
                return;

            // Xóa toàn bộ dữ liệu lưu
            SecureStorage.Remove("jwt_token");
            SecureStorage.Remove("refresh_token");
            SecureStorage.Remove("expires_at");
            SecureStorage.Remove("username");
            SecureStorage.Remove("fullname");
            SecureStorage.Remove("department");
            SecureStorage.Remove("role");
            SecureStorage.Remove("email");
            SecureStorage.Remove("phone");
            SecureStorage.Remove("address");

            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private async void OnChangePasswordPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePasswordPage());
        }


        private async void OnExportProfileClicked(object sender, EventArgs e)
        {
            if (_currentEmployeeId == Guid.Empty)
            {
                await DisplayAlert("Lỗi", "Không thể xuất thông tin. Thiếu mã nhân viên.", "OK");
                return;
            }

            try
            {
                // 1. Gọi API để lấy download URL
                var response = await _api.GetProfileReportDownloadUrl(_currentEmployeeId);

                if (response != null && response.Success && !string.IsNullOrEmpty(response.DownloadUrl))
                {
                    // 2. Mở URL trong trình duyệt mặc định
                    bool success = await Launcher.Default.OpenAsync(new Uri(response.DownloadUrl));

                    if (success)
                    {
                        // Thông báo thành công (tùy chọn)
                        //await DisplayAlert("Thành công", $"Báo cáo đang được tải xuống từ:\n{response.DownloadUrl}", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Lỗi", "Không thể mở trình duyệt để tải file. Vui lòng kiểm tra lại thiết bị.", "OK");
                    }
                }
                else
                {
                    // Xử lý lỗi trả về từ API
                    await DisplayAlert("Lỗi", response?.Message ?? "Không thể lấy đường dẫn tải file từ API.", "OK");
                }
            }
            catch (ApiException ex)
            {
                // Xử lý lỗi HTTP/API (ví dụ: 401 Unauthorized, 404 Not Found)
                await DisplayAlert("Lỗi API", $"Có lỗi xảy ra khi gọi API: {ex.StatusCode}. Vui lòng thử lại sau.", "OK");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chung
                await DisplayAlert("Lỗi Hệ thống", $"Đã xảy ra lỗi không mong muốn: {ex.Message}", "OK");
            }
        }
    }
}
