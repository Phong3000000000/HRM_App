using Android.Gms.Common.Apis;
using HRMApp.Helpers;
using HRMApp.Model;
using HRMApp.Services.Api;
using HRMApp.Services.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Google.Android.DataTransport.Cct.Internal;
using static Android.Net.Wifi.WifiEnterpriseConfig;

namespace HRMApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IHrmApi _api;

        //Vaidation cho đăng ký
        public string RegisterEmailError { get; set; }
        public string RegisterPasswordError { get; set; }
        public string RegisterConfirmPasswordError { get; set; }
        //Validation cho đăng nhập
        public string LoginEmailError { get; set; }
        public string LoginUsernameError { get; set; }
        public string LoginPasswordError { get; set; }


        //public LoginPopupViewModel()
        //{
        //    //_api = ServiceHelper.GetService<IArticleApi>());
        //    _api = RestService.For<IArticleApi>("http://192.168.11.227:5162");
        //}


        public LoginViewModel(IHrmApi api)
        {
            _api = api;
            _ = LoadSavedCredentialsAsync();
        }


        // Tự động load thông tin đăng nhập đã lưu (nếu có)
        private async Task LoadSavedCredentialsAsync()
        {
            var remember = await SecureStorage.GetAsync("remember_me");

            if (remember == "true")
            {
                var savedUsername = await SecureStorage.GetAsync("saved_username");
                var savedPassword = await SecureStorage.GetAsync("saved_password");

                if (!string.IsNullOrEmpty(savedUsername))
                    LoginUsername = savedUsername;
                if (!string.IsNullOrEmpty(savedPassword))
                    LoginPassword = savedPassword;

                RememberMe = true; // ✅ để checkbox auto tick
            }
        }



        // Property binding
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private bool isLoginMode = true;
        public bool IsLoginMode
        {
            get => isLoginMode;
            set { isLoginMode = value; OnPropertyChanged(nameof(IsLoginMode)); }
        }

        private string loginEmail;
        public string LoginEmail
        {
            get => loginEmail;
            set { loginEmail = value; OnPropertyChanged(nameof(LoginEmail)); }
        }
        private string loginUsername;
        public string LoginUsername
        {
            get => loginUsername;
            set { loginUsername = value; OnPropertyChanged(nameof(LoginUsername)); }
        }

        private string loginPassword;
        public string LoginPassword
        {
            get => loginPassword;
            set { loginPassword = value; OnPropertyChanged(nameof(LoginPassword)); }
        }

        private string registerUsername;
        public string RegisterUsername
        {
            get => registerUsername;
            set { registerUsername = value; OnPropertyChanged(nameof(RegisterUsername)); }
        }

        private string registerEmail;
        public string RegisterEmail
        {
            get => registerEmail;
            set { registerEmail = value; OnPropertyChanged(nameof(RegisterEmail)); }
        }

        private string registerPassword;
        public string RegisterPassword
        {
            get => registerPassword;
            set { registerPassword = value; OnPropertyChanged(nameof(RegisterPassword)); }
        }

        private string registerConfirmPassword;
        public string RegisterConfirmPassword
        {
            get => registerConfirmPassword;
            set { registerConfirmPassword = value; OnPropertyChanged(nameof(RegisterConfirmPassword)); }
        }

        private bool rememberMe;
        public bool RememberMe
        {
            get => rememberMe;
            set { rememberMe = value; OnPropertyChanged(nameof(RememberMe)); }
        }




        // Command
        public ICommand ShowLoginCommand => new Command(() => IsLoginMode = true);
        public ICommand ShowRegisterCommand => new Command(() => IsLoginMode = false);
        public ICommand SimulateCaptchaCommand => new Command(() => App.Current.MainPage.DisplayAlert("Captcha", "Giả lập xác nhận!", "OK"));

        public ICommand LoginCommand => new Command(async () => await LoginAsync());
        public ICommand RegisterCommand => new Command(async () => await RegisterAsync());
        //public ICommand FacebookLoginCommand => new Command(async () => await FacebookLoginAsync());

      
   
        private async Task LoginAsync()
        {
            // Reset lỗi cũ
            LoginEmailError = LoginPasswordError = string.Empty;
            OnPropertyChanged(nameof(LoginEmailError));
            OnPropertyChanged(nameof(LoginPasswordError));

            bool hasError = false;

            // 1. Validate Email
            //if (string.IsNullOrWhiteSpace(LoginEmail) || !IsValidEmail(LoginEmail))
            //{
            //    LoginEmailError = "Chưa đúng định dạng Email";
            //    OnPropertyChanged(nameof(LoginEmailError));
            //    hasError = true;
            //}
            if (string.IsNullOrWhiteSpace(LoginUsername))
            {
                LoginUsernameError = "Chưa nhập username";
                OnPropertyChanged(nameof(LoginUsernameError));
                hasError = true;
            }

            // 2. Validate Password
            if (string.IsNullOrWhiteSpace(LoginPassword))
            {
                LoginPasswordError = "Chưa nhập mật khẩu";
                OnPropertyChanged(nameof(LoginPasswordError));
                hasError = true;
            }

            if (hasError)
                return;

  
            try
            {
                var result = await _api.LoginAsync(new LoginRequest
                {
                    Username = LoginUsername,
                    Password = LoginPassword
                });

                if (result != null && result.Success && result.Data != null)
                {
                    await SecureStorage.SetAsync("jwt_token", result.Data.Access_Token);
                    await SecureStorage.SetAsync("refresh_token", result.Data.Refresh_Token);
                    await SecureStorage.SetAsync("expires_at", result.Data.Expires_At);

                    var user = result.Data.User;
                    // 🔹 Lưu thông tin user để hiển thị sau này
                    await SecureStorage.SetAsync("userid", user.Id ?? "");
                    await SecureStorage.SetAsync("username", user.Username ?? "");
                    await SecureStorage.SetAsync("fullname", user.Employee.Full_Name ?? "");
                    await SecureStorage.SetAsync("code", user.Employee.Code ?? "");
                    await SecureStorage.SetAsync("department", user.Employee?.Code ?? ""); // mã NV hoặc phòng ban
                    await SecureStorage.SetAsync("role", user.Role?.Name ?? "");
                    await SecureStorage.SetAsync("email", user.Employee?.Email ?? "");
                    await SecureStorage.SetAsync("phone", user.Employee?.Phone ?? "");
                    await SecureStorage.SetAsync("address", user.Employee?.Address ?? "");
                    await SecureStorage.SetAsync("avatar", user.Employee?.Avatar_Url ?? "");
                    await SecureStorage.SetAsync("employeeid", user.Employee?.Id?? "");
                    await SecureStorage.SetAsync("status", user.Employee?.Status ?? "");
                    await SecureStorage.SetAsync("hiredate", user.Employee?.Hire_Date ?? "");
                    await SecureStorage.SetAsync("departmentname", user.Employee?.Department_name ?? "");
                    await SecureStorage.SetAsync("positionname", user.Employee?.Position_name ?? "");
                    AppShell.LoggedInUsername = user.Username;
                    AppShell.LoggedInEmail = user.Employee?.Email;

                    AppShell.IsLoggedIn = true;

                    // Điều hướng về AppShell
                    //MainThread.BeginInvokeOnMainThread(() =>
                    //{
                    //    Application.Current.MainPage = new AppShell();
                    //});
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Lỗi", result?.Message ?? "Không đăng nhập được", "OK");
                }


                LoginPassword = LoginEmail = LoginEmailError = LoginPasswordError = string.Empty;
                OnPropertyChanged(nameof(LoginEmail));
                OnPropertyChanged(nameof(LoginPassword));
                OnPropertyChanged(nameof(LoginEmailError));
                OnPropertyChanged(nameof(LoginPasswordError));

                //AppShell.IsLoggedIn = true;

                // ✅ Nếu người dùng chọn "Nhớ đăng nhập"
                if (RememberMe)
                {
                    await SecureStorage.SetAsync("saved_username", LoginUsername);
                    await SecureStorage.SetAsync("saved_password", LoginPassword);
                    await SecureStorage.SetAsync("remember_me", "true");  // ✅ thêm dòng này
                }
                else
                {
                    SecureStorage.Remove("saved_username");
                    SecureStorage.Remove("saved_password");
                    SecureStorage.Remove("remember_me");  // ✅ thêm dòng này
                }



                // Ensure you have a valid instance of NotificationViewModel to pass to AppShell
                var notificationViewModel = ServiceHelper.GetService<NotificationViewModel>();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    var signalRService = ServiceHelper.GetService<ISignalRService>();
                    var notificationStateService = new NotificationStateService();
                    Application.Current.Windows[0].Page = new AppShell(notificationStateService, notificationViewModel);
                });
            }
            catch (ApiException apiEx)
            {
                Debug.WriteLine($"❌ API lỗi: {apiEx.StatusCode}");
                await App.Current.MainPage.DisplayAlert("Lỗi", "Sai tài khoản hoặc mật khẩu", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"💥 Lỗi hệ thống: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Lỗi", "Sai tài khoản hoặc mật khẩu", "OK");
            }
 
        
        }

        private async Task RegisterAsync()
        {
            // Reset lỗi cũ
            RegisterEmailError = RegisterPasswordError = RegisterConfirmPasswordError = string.Empty;
            OnPropertyChanged(nameof(RegisterEmailError));
            OnPropertyChanged(nameof(RegisterPasswordError));
            OnPropertyChanged(nameof(RegisterConfirmPasswordError));

            bool hasError = false;

            // 1. Validate Email
            if (string.IsNullOrWhiteSpace(RegisterEmail) || !IsValidEmail(RegisterEmail))
            {
                RegisterEmailError = "Chưa đúng định dạng Email";
                OnPropertyChanged(nameof(RegisterEmailError));
                hasError = true;
            }

            // 2. Validate Password
            if (string.IsNullOrWhiteSpace(RegisterPassword) || RegisterPassword.Length < 6)
            {
                RegisterPasswordError = "Mật khẩu phải dài từ 6 ký tự trở lên.";
                OnPropertyChanged(nameof(RegisterPasswordError));
                hasError = true;
            }

            // 3. Validate Confirm Password
            if (RegisterPassword != RegisterConfirmPassword)
            {
                RegisterConfirmPasswordError = "Mật khẩu xác nhận và mật khẩu không giống nhau.";
                OnPropertyChanged(nameof(RegisterConfirmPasswordError));
                hasError = true;
            }

            if (hasError)
                return;

            // Gọi API
            try
            {
                var res = await _api.RegisterAsync(new RegisterRequest
                {
                    Username = RegisterUsername,
                    Email = RegisterEmail,
                    Password = RegisterPassword
                });

                await App.Current.MainPage.DisplayAlert("Thành công", res.Message, "OK");
                //Làm mới đăng ký
                RegisterUsername = RegisterEmail = RegisterPassword = RegisterConfirmPassword = RegisterEmailError = RegisterPasswordError = RegisterConfirmPasswordError = string.Empty;
                OnPropertyChanged(nameof(RegisterUsername));
                OnPropertyChanged(nameof(RegisterEmail));
                OnPropertyChanged(nameof(RegisterPassword));
                OnPropertyChanged(nameof(RegisterConfirmPassword));
                OnPropertyChanged(nameof(RegisterEmailError));
                OnPropertyChanged(nameof(RegisterPasswordError));
                OnPropertyChanged(nameof(RegisterConfirmPasswordError));

                IsLoginMode = true;

            }
            catch (ApiException ex)
            {
                Debug.WriteLine($"Lỗi: {ex.Message}");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch { return false; }
        }


    }
}
