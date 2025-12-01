using HRMApp.View; // Để nhận diện được TrainingListPage

namespace HRMApp.View;

public partial class TaskPage : ContentPage
{
    public TaskPage()
    {
        InitializeComponent();
    }

    // Hàm xử lý khi bấm vào mục Đào tạo
    private async void OnTrainingTapped(object sender, EventArgs e)
    {
        // Hiệu ứng nhấp nháy nhẹ để user biết đã bấm
        if (sender is Frame frame)
        {
            await frame.FadeTo(0.5, 100);
            await frame.FadeTo(1.0, 100);
        }

        // Điều hướng sang trang danh sách khóa học
        // Đảm bảo bạn đã đăng ký route này trong AppShell.xaml.cs
        await Shell.Current.GoToAsync(nameof(TrainingListPage));
    }
}