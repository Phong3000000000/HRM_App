using Android.Gms.Common.Apis;
using AndroidX.ConstraintLayout.Core.Parser;
using HRMApp.Helpers;
using HRMApp.Model;
using HRMApp.Model.Notification;
using HRMApp.Services.Api;
using HRMApp.Services.Wifi;
using HRMApp.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Networking;
using Plugin.Firebase.CloudMessaging;
using Refit;
using System.Diagnostics;
using System.Linq;
using System.Text.Json; // Cần thêm để dùng FirstOrDefault()

namespace HRMApp.View;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        //GetToKen();
        GetFCMToken();
    }

    private async void GetToKen()
    {
        await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
        var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
        Console.WriteLine($"FCM token: {token}");
    }

    private async void OnCheckinTapped(object sender, TappedEventArgs e)
    {
        await LoadWifiInfoAsync();
        DialogOverlay.Opacity = 0;
        DialogOverlay.IsVisible = true;
        await DialogOverlay.FadeTo(1, 250, Easing.CubicIn);
    }

    private async void OnCancelDialog(object sender, EventArgs e)
    {
        await DialogOverlay.FadeTo(0, 200, Easing.CubicOut);
        DialogOverlay.IsVisible = false;
    }


    private async void OnNotificationTapped(object sender, TappedEventArgs e)
    {
        // Resolve the required NotificationViewModel using the ServiceHelper
        //var notificationViewModel = ServiceHelper.GetService<NotificationViewModel>();

        // Pass the resolved viewModel to the NotificationPage constructor
        await Navigation.PushAsync(new NotificationPage());
    }

    private async void OnScheduleTapped(object sender, TappedEventArgs e)
    {
        var schedulePage = ServiceHelper.GetService<SchedulePage>();

        await Navigation.PushAsync(schedulePage);
    }

    private async void OnLeaveTapped(object sender, TappedEventArgs e)
    {
        // Sử dụng ServiceHelper để tạo page với DI
        var requestListPage = ServiceHelper.GetService<RequestListPage>();
        await Navigation.PushAsync(requestListPage);
    }

    private async void OnSalaryClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SalarySummaryPage());
    }
    private async void OnNewsClicked(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new NewsListPage());

        await Navigation.PushAsync(new ReportSelectionPage());
    }

    private async Task LoadWifiInfoAsync()
    {
        try
        {
            var wifiService = ServiceHelper.GetService<IWifiService>();
            var (ssid, bssid) = await wifiService.GetWifiInfoAsync();

            WifiNameLabel.Text = $"Tên WiFi: {ssid}";
            WifiBssidLabel.Text = $"Mã BSSID: {bssid}";
        }
        catch (Exception ex)
        {
            WifiNameLabel.Text = "Không lấy được thông tin WiFi";
            WifiBssidLabel.Text = "";
            Console.WriteLine($"{ex.Message}");
        }
    }

    //Check in
    private async void OnConfirmDialog(object sender, EventArgs e)
    {
        // Lấy thông tin nhân viên & wifi
        var employeeId = await SecureStorage.GetAsync("employeeid");
        var wifiName = WifiNameLabel.Text?.Replace("Tên WiFi: ", "") ?? "";
        var bssid = WifiBssidLabel.Text?.Replace("Mã BSSID: ", "") ?? "";

        Console.WriteLine($"🔍 EmployeeId: {employeeId}");
        Console.WriteLine($"🔍 WiFi: {wifiName}");
        Console.WriteLine($"🔍 BSSID: {bssid}");

        try
        {
            var api = ServiceHelper.GetService<ILocalApi>();

            var request = new CheckinRequest
            {
                EmployeeId = employeeId,
                WifiName = wifiName,
                Bssid = bssid,
                Shift = "Auto"
            };

            var response = await api.CheckinAsync(request);

            if (response.Success)
            {
                await DisplayAlert("Thành công", response.Message, "OK");

                // Ẩn dialog và đổi trạng thái
                await DialogOverlay.FadeTo(0, 200, Easing.CubicOut);
                DialogOverlay.IsVisible = false;

                CheckinTile.IsVisible = false;
                CheckoutTile.IsVisible = true;
            }
            else
            {
                await DisplayAlert("❌ Thất bại", response?.Message ?? "Không thể check-in.", "OK");
            }
        }
        catch (Refit.ApiException ex)
        {
            try
            {
                // Parse JSON trả về từ backend
                var error = await ex.GetContentAsAsync<ApiResponse>();

                // Nếu có message từ BE -> hiển thị
                if (error != null && !string.IsNullOrEmpty(error.Message))
                {
                    await DisplayAlert("Thông báo", error.Message, "OK");
                }
                else
                {
                    await DisplayAlert("Lỗi", "Lỗi không xác định từ server.", "OK");
                }
            }
            catch
            {
                // Trường hợp BE không trả JSON đúng định dạng
                await DisplayAlert("Lỗi", ex.Message, "OK");
            }

            Debug.WriteLine($"API Error: {ex.Content}");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
            await DisplayAlert("Lỗi", $"{ex.Message}", "OK");
        }
    }

    //Check out
    private async void OnCheckoutTapped(object sender, TappedEventArgs e)
    {
        bool confirm = await DisplayAlert("Xác nhận", "Bạn muốn Check-out ca làm việc?", "Đồng ý", "Hủy");
        if (!confirm) return;

        try
        {
            var api = ServiceHelper.GetService<ILocalApi>();
            var employeeId = await SecureStorage.GetAsync("employeeid");
            if (string.IsNullOrEmpty(employeeId))
            {
                await DisplayAlert("Lỗi", "Không tìm thấy EmployeeId. Vui lòng đăng nhập lại.", "OK");
                return;
            }

            // 🔍 Lấy WiFi hiện tại
            var wifiService = ServiceHelper.GetService<IWifiService>();
            var (ssid, bssid) = await wifiService.GetWifiInfoAsync();

            var request = new CheckoutRequest
            {
                EmployeeId = employeeId,
                WifiName = ssid,
                Bssid = bssid
            };

            var response = await api.CheckoutAsync(request);

            if (response.Success)
            {
                await DisplayAlert("Thành công", response.Message, "OK");
                CheckinTile.IsVisible = true;
                CheckoutTile.IsVisible = false;
            }
            else
            {
                await DisplayAlert("Thất bại", response.Message, "OK");
            }
        }
        catch (Refit.ApiException ex)
        {
            try
            {
                // Parse JSON lỗi từ BE
                var error = await ex.GetContentAsAsync<ApiResponse>();
                if (error != null && !string.IsNullOrEmpty(error.Message))
                {
                    await DisplayAlert("Thông báo", error.Message, "OK");
                }
                else
                {
                    await DisplayAlert("Lỗi", "Lỗi không xác định từ server.", "OK");
                }
            }
            catch
            {
                await DisplayAlert("Lỗi", ex.Message, "OK");
            }

            Debug.WriteLine($"API Error: {ex.Content}");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Lỗi", $"Không thể kết nối đến server: {ex.Message}", "OK");
        }
    }

    //Kiểm tra xem nhân viên này đã check out chưa nếu đã check in nhưng chưa check out thì hiển thị ô check out
    private async Task CheckAttendanceStatusAsync()
    {
        try
        {
            var employeeId = await SecureStorage.GetAsync("employeeid");
            if (string.IsNullOrEmpty(employeeId)) return;

            var api = ServiceHelper.GetService<ILocalApi>();
            var response = await api.GetAttendanceStatusAsync(Guid.Parse(employeeId));

            if (response.Success)
            {
                switch (response.Status)
                {
                    case "CheckedIn":
                        // Đã check-in nhưng chưa check-out
                        CheckinTile.IsVisible = false;
                        CheckoutTile.IsVisible = true;
                        break;

                    case "CheckedOut":
                        // Đã check-out => có thể check-in lại ngày khác
                        CheckinTile.IsVisible = true;
                        CheckoutTile.IsVisible = false;
                        break;

                    case "Absent":
                        // Nghỉ không phép => chỉ giữ nút Check-in, không cho Checkout
                        CheckinTile.IsVisible = true;
                        CheckoutTile.IsVisible = false;
                        break;

                    case "leave":
                        // Nghỉ có phép => không cho Checkin nửa
                        CheckinTile.IsVisible = true;
                        CheckoutTile.IsVisible = false;
                        break;

                    case "NotCheckedIn":
                    default:
                        // Chưa check-in => hiện nút check-in
                        CheckinTile.IsVisible = true;
                        CheckoutTile.IsVisible = false;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"❌ Lỗi khi kiểm tra trạng thái chấm công: {ex.Message}");
        }
    }

    private bool _isLoadingSalary = false;

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LoadUserProfileAsync();
        await CheckAttendanceStatusAsync(); // kiểm tra trạng thái mỗi lần vào lại trang
        if (!_isLoadingSalary)
        {
            _isLoadingSalary = true;
            try
            {
                await LoadCurrentSalaryAsync();
            }
            finally
            {
                _isLoadingSalary = false;
            }
        }
    }

    private async void LoadUserProfileAsync()
    {
        var fullname = await SecureStorage.GetAsync("fullname");
        var username = await SecureStorage.GetAsync("username");
        var role = await SecureStorage.GetAsync("role");
        var avatarUrl = await SecureStorage.GetAsync("avatar");

        // Hiển thị thông tin cơ bản
        FullnameLabel.Text = fullname ?? "Người dùng";
        RoleLabel.Text = role ?? "";

        // Avatar logic
        if (!string.IsNullOrEmpty(avatarUrl))
        {
            // Có ảnh thật
            AvatarImage.IsVisible = true;
            AvatarBorder.IsVisible = false;
            AvatarImage.Source = ImageSource.FromUri(new Uri(avatarUrl));
        }
        else
        {
            // Không có ảnh → chữ cái đầu
            AvatarImage.IsVisible = false;
            AvatarBorder.IsVisible = true;

            var firstLetter = (!string.IsNullOrEmpty(fullname)
                ? fullname[0]
                : (!string.IsNullOrEmpty(username) ? username[0] : 'U')).ToString().ToUpper();

            AvatarLabel.Text = firstLetter;
        }
    }

    //Load lương hiện tại
    private async Task LoadCurrentSalaryAsync()
    {
        try
        {
            var employeeIdStr = await SecureStorage.GetAsync("employeeid");
            if (string.IsNullOrEmpty(employeeIdStr)) return;

            Guid employeeId = Guid.Parse(employeeIdStr);
            var api = ServiceHelper.GetService<ILocalApi>();

            // Lấy tháng hiện tại
            var now = DateTime.Now;
            string month = now.ToString("yyyy-MM");

            Debug.WriteLine($"🔍 LoadCurrentSalaryAsync: employeeId={employeeId}, month={month}");

            // ✅ SỬ DỤNG RAW RESPONSE để tránh lỗi deserialization
            var rawResponse = await api.GetPayrollPerformanceRawAsync(employeeId, month);
            var jsonContent = await rawResponse.Content.ReadAsStringAsync();

            Debug.WriteLine($"🔍 Raw JSON: {jsonContent}");

            // Parse thủ công chỉ lấy phần cần thiết
            using var jsonDoc = JsonDocument.Parse(jsonContent);
            var root = jsonDoc.RootElement;

            if (root.TryGetProperty("success", out var successProp) && successProp.GetBoolean())
            {
                if (root.TryGetProperty("data", out var dataProp) && dataProp.ValueKind == JsonValueKind.Array)
                {
                    var firstData = dataProp.EnumerateArray().FirstOrDefault();
                    if (firstData.TryGetProperty("result", out var resultProp) && resultProp.ValueKind == JsonValueKind.Array)
                    {
                        var firstResult = resultProp.EnumerateArray().FirstOrDefault();
                        if (firstResult.TryGetProperty("luong", out var luongProp))
                        {
                            if (luongProp.TryGetProperty("luongThucNhan", out var luongThucNhanProp))
                            {
                                var salary = luongThucNhanProp.GetDecimal();

                                // Cập nhật UI
                                var firstDay = new DateTime(now.Year, now.Month, 1);
                                var lastDay = firstDay.AddMonths(1).AddDays(-1);

                                await MainThread.InvokeOnMainThreadAsync(() =>
                                {
                                    SalaryPeriodLabel.Text = $"({firstDay:dd/MM} - {lastDay:dd/MM})";
                                    SalaryAmountLabel.Text = $"{salary:N0} đ";
                                });

                                Debug.WriteLine($"✅ LoadCurrentSalaryAsync successful: {salary:N0} đ");
                                return;
                            }
                        }
                    }
                }
            }

            // Nếu không parse được thì set default
            SetDefaultSalaryDisplay();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"❌ Unexpected error in LoadCurrentSalaryAsync: {ex.Message}");
            SetDefaultSalaryDisplay();
        }
    }

    private async Task ProcessSalaryResponse(PayrollPerformanceResponse apiResponse, DateTime now)
    {
        // ✅ SỬA: Kiểm tra từng bước một cách cẩn thận
        if (apiResponse == null)
        {
            Debug.WriteLine("❌ apiResponse is null");
            SetDefaultSalaryDisplay();
            return;
        }

        if (!apiResponse.Success)
        {
            Debug.WriteLine($"❌ API not successful: {apiResponse.Message}");
            SetDefaultSalaryDisplay();
            return;
        }

        if (apiResponse.Data == null || apiResponse.Data.Length == 0)
        {
            Debug.WriteLine("❌ apiResponse.Data is null or empty");
            SetDefaultSalaryDisplay();
            return;
        }

        var firstDataWrapper = apiResponse.Data.FirstOrDefault();
        if (firstDataWrapper?.Result == null || firstDataWrapper.Result.Length == 0)
        {
            Debug.WriteLine("❌ firstDataWrapper.Result is null or empty");
            SetDefaultSalaryDisplay();
            return;
        }

        var firstResult = firstDataWrapper.Result.FirstOrDefault();
        if (firstResult?.Luong == null)
        {
            Debug.WriteLine("❌ firstResult.Luong is null");
            SetDefaultSalaryDisplay();
            return;
        }

        var luongSummary = firstResult.Luong;

        // ✅ THÀNH CÔNG: Cập nhật UI
        var firstDay = new DateTime(now.Year, now.Month, 1);
        var lastDay = firstDay.AddMonths(1).AddDays(-1);

        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            SalaryPeriodLabel.Text = $"({firstDay:dd/MM} - {lastDay:dd/MM})";
            var salary = luongSummary.LuongThucNhan;
            SalaryAmountLabel.Text = $"{salary:N0} đ";
        });

        Debug.WriteLine($"✅ LoadCurrentSalaryAsync successful: {luongSummary.LuongThucNhan:N0} đ");
    }

    // ✅ THÊM: Helper method để set giá trị mặc định
    private void SetDefaultSalaryDisplay()
    {
        MainThread.InvokeOnMainThreadAsync(() =>
        {
            SalaryPeriodLabel.Text = $"({DateTime.Now:MM/yyyy})";
            SalaryAmountLabel.Text = "0 đ";
        });
    }


    public string GetDeviceId()
    {
        const string key = "device_id";
        string deviceId = Preferences.Get(key, null);
        if (string.IsNullOrEmpty(deviceId))
        {
            deviceId = Guid.NewGuid().ToString();
            Preferences.Set(key, deviceId);
        }
        return deviceId;
    }
    private async void GetFCMToken()
    {
        try
        {
            await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
            var token = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();
            System.Diagnostics.Debug.WriteLine($"FCM token: {token}");

            // --- BẮT ĐẦU SỬA LỖI LOGIC ---

            Guid userIdToSend; // Biến này sẽ lưu ID của user

            var userIdString = await SecureStorage.GetAsync("userid");

            if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out Guid employeeId))
            {
                // 1. ĐÃ TÌM THẤY USERID (user đã đăng nhập)
                // Gán employeeId (là một Guid) để gửi đi
                userIdToSend = employeeId;
                System.Diagnostics.Debug.WriteLine($"Sử dụng UserId từ SecureStorage: {userIdToSend}");
            }
            else
            {
                // 2. KHÔNG TÌM THẤY USERID (user chưa đăng nhập)
                // Chúng ta không nên lưu token nếu không biết của user nào.
                // Logic cũ của bạn (dùng GetDeviceId()) là NGUYÊN NHÂN gây ra
                // lỗi "không thể gửi FCM" ở backend (vì DeviceId không phải là UserId).
                System.Diagnostics.Debug.WriteLine("Không tìm thấy UserId trong SecureStorage. Sẽ không lưu FCM token.");
                return; // Thoát khỏi hàm
            }

            // --- KẾT THÚC SỬA LỖI LOGIC ---

            // Tạo FcmToken VỚI ĐÚNG UserId (là Guid)
            var fcmToken = new FcmToken
            {
                UserId = userIdToSend.ToString(), // <-- Đã sửa: Luôn là UserId thật
                Token = token,
            };

            var api = ServiceHelper.GetService<ILocalApi>();
            var response = await api.SaveTokenAsync(fcmToken);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Đã lưu token cho UserId: {userIdToSend}");
            }
            else
            {
                Debug.WriteLine($"Lưu token thất bại cho UserId: {userIdToSend}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Lỗi khi lấy FCM token: {ex.Message}");
        }
    }
}