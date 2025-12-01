using HRMApp.ViewModels;

namespace HRMApp.View;

public partial class NotificationDetailPage : ContentPage
{
    public NotificationDetailPage(NotificationDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}