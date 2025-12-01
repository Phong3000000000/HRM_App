using HRMApp.Helpers;
using HRMApp.Model;
using HRMApp.Services.Api;
using System;

namespace HRMApp.View
{
    public partial class ChangePasswordPage : ContentPage
    {
        private readonly IHrmApi _api;
        private bool isOldPasswordHidden = true;

        public ChangePasswordPage()
        {
            InitializeComponent();
            _api = ServiceHelper.GetService<IHrmApi>();
        }

        private void ToggleOldPassword(object sender, EventArgs e)
        {
            isOldPasswordHidden = !isOldPasswordHidden;
            OldPasswordEntry.IsPassword = isOldPasswordHidden;

            var btn = sender as ImageButton;
            btn.Source = isOldPasswordHidden ? "visibility_off.png" : "visibility.png";
        }

        private async void OnConfirmChangeClicked(object sender, EventArgs e)
        {
            string oldPass = OldPasswordEntry.Text?.Trim();
            string newPass = NewPasswordEntry.Text?.Trim();
            string confirmPass = ConfirmPasswordEntry.Text?.Trim();

            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
            {
                await DisplayAlert("Lỗi", "Vui lòng nhập đầy đủ thông tin.", "OK");
                return;
            }

            if (newPass != confirmPass)
            {
                await DisplayAlert("Lỗi", "Mật khẩu xác nhận không khớp.", "OK");
                return;
            }

            try
            {
                var req = new ChangePasswordRequest
                {
                    currentPassword = oldPass,
                    newPassword = newPass
                };

                var res = await _api.ChangePasswordAsync(req);

                if (res.IsSuccessStatusCode)
                {
                    await DisplayAlert("Thành công", "Đổi mật khẩu thành công!", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Thất bại", res.Error?.Content ?? "Đổi mật khẩu thất bại.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Lỗi", ex.Message, "OK");
            }
        }
    }
}
