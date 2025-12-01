using HRMApp.ViewModel; // Nhớ using namespace chứa ViewModel

namespace HRMApp.View;

public partial class TrainingDetailPage : ContentPage
{
    // 1. Sửa Constructor để nhận ViewModel từ hệ thống (Dependency Injection)
    public TrainingDetailPage(TrainingDetailViewModel vm)
    {
        InitializeComponent();
        // 2. Gán BindingContext để giao diện (XAML) nhận được dữ liệu
        BindingContext = vm;
    }

    // 3. Override OnAppearing để load dữ liệu mỗi khi trang hiện lên
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Gọi hàm khởi tạo dữ liệu (kiểm tra điểm, load câu hỏi) từ ViewModel
        if (BindingContext is TrainingDetailViewModel vm)
        {
            await vm.InitializeAsync();
        }
    }

    // 4. Thêm hàm xử lý sự kiện nút "Quay về danh sách" (Sửa lỗi XC0002)
    private async void OnBackClicked(object sender, EventArgs e)
    {
        // ".." nghĩa là quay lại trang trước đó trong ngăn xếp điều hướng
        await Shell.Current.GoToAsync("..");
    }
}