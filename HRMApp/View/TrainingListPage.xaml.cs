using HRMApp.ViewModel; // Nhớ dòng này

namespace HRMApp.View;

public partial class TrainingListPage : ContentPage
{
    // 1. Sửa Constructor: Thêm tham số vm để nhận ViewModel từ hệ thống
    public TrainingListPage(TrainingListViewModel vm)
    {
        InitializeComponent();
        // Gán ViewModel vào BindingContext để giao diện nhận được dữ liệu
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // 2. Sửa ép kiểu: Phải là TrainingListViewModel (của trang danh sách)
        if (BindingContext is TrainingListViewModel vm)
        {
            // Gọi lệnh LoadData để tải danh sách đề thi và cập nhật trạng thái
            await vm.LoadDataCommand.ExecuteAsync(null);
        }
    }

    // Hàm này thừa ở trang danh sách (thường trang List là trang gốc hoặc dùng nút Back của điện thoại)
    // Bạn có thể xóa nếu không dùng nút "Quay về" nào trên giao diện trang này.
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}