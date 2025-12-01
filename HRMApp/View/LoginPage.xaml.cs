using HRMApp.Helpers;
using HRMApp.ViewModels;

namespace HRMApp.View;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();

        BindingContext = ServiceHelper.GetService<LoginViewModel>();

        //MessagingCenter.Subscribe<object>(this, "HideLoginPopup", (sender) => this.IsVisible = false);
        //MessagingCenter.Subscribe<object>(this, "OpenLoginPopup", (sender) => ThisPopup.IsVisible = true);
    }

    private void Close_Clicked(object sender, EventArgs e)
    {
        MessagingCenter.Send<object>(this, "HideLoginPopup");
    }
  

}