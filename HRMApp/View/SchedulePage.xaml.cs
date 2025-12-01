using HRMApp.ViewModels;

namespace HRMApp.View;

public partial class SchedulePage : ContentPage
{
    private readonly ScheduleViewModel _viewModel;

    public SchedulePage(ScheduleViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    // ✅ THÊM: Load data khi trang xuất hiện
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        // Tải lại dữ liệu mỗi lần vào trang
        await _viewModel.LoadWeekDataAsync();
    }
}