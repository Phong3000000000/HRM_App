using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HRMApp.Model.Training;
using HRMApp.Services.Api;
using HRMApp.View;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage; // Cần thêm namespace này cho SecureStorage
using System.Diagnostics; // THÊM DÒNG NÀY ĐỂ DEBUG

namespace HRMApp.ViewModel
{
    public partial class TrainingListViewModel : ObservableObject
    {
        private readonly ILocalApi _api;
        // TODO: Thay bằng ID thật từ UserSession
        // private readonly Guid _employeeId = Guid.Parse("776ec5cf-8349-4eb9-a6ec-73c75a7c58db"); // Không dùng biến này nữa

        [ObservableProperty]
        private ObservableCollection<CourseDto> courses;

        [ObservableProperty]
        private bool isLoading;

        public TrainingListViewModel(ILocalApi api)
        {
            _api = api;
            Courses = new ObservableCollection<CourseDto>();
        }

        [RelayCommand]
        public async Task LoadData()
        {
            if (IsLoading) return;
            IsLoading = true;
            Courses.Clear();

            try
            {
                // === BƯỚC 1: LẤY EMPLOYEE ID TỪ SECURE STORAGE ===
                var employeeIdString = await SecureStorage.GetAsync("employeeid");

                // Debug
                Debug.WriteLine($"[DEBUG] Employee ID String: {employeeIdString}");

                Guid employeeId;
                if (string.IsNullOrEmpty(employeeIdString) || !Guid.TryParse(employeeIdString, out employeeId))
                {
                    await App.Current.MainPage.DisplayAlert("Lỗi", "Không tìm thấy hoặc sai định dạng Employee ID.", "OK");
                    IsLoading = false;
                    return;
                }

                // === BƯỚC 2: GỌI API (SỬA LẠI CHỖ NÀY) ===
                // Lưu ý: 
                // - q: Để null (trừ khi bạn muốn tìm kiếm theo tên)
                // - employeeId: Truyền guid vừa parse được vào đây

                var response = await _api.GetCoursesAsync(
                    q: null,
                    employeeId: employeeId,
                    pageSize: 100
                );


                // Kiểm tra Data có tồn tại và có ít nhất 1 phần tử không
                if (response.Data != null && response.Data.Count > 0)
                {
                    // Lấy phần tử đầu tiên của Data, sau đó mới lấy Result
                    var firstData = response.Data[0];

                    if (firstData.Result != null)
                    {
                        foreach (var course in firstData.Result)
                        {
                            // Logic kiểm tra điểm số (giữ nguyên)
                            try
                            {
                                // Sử dụng employeeId đã lấy từ SecureStorage
                                var scoreRes = await _api.GetScoreAsync(employeeId, course.Id);
                                if (scoreRes.Success && scoreRes.Data != null)
                                {
                                    var score = scoreRes.Data.First();
                                    if (score.Answered > 0)
                                    {
                                        course.StatusDisplay = score.Passed ? $"ĐẠT ({score.ScorePercent}%)" : $"RỚT ({score.ScorePercent}%)";
                                        course.StatusColor = score.Passed ? Colors.Green : Colors.Red;
                                    }
                                    else
                                    {
                                        course.StatusDisplay = "Sẵn sàng thi";
                                        course.StatusColor = Colors.Blue;
                                    }
                                }
                            }
                            catch { }

                            Courses.Add(course);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Lỗi", "Lỗi tải khóa học: " + ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task GoToDetail(CourseDto course)
        {
            if (course == null) return;
            await Shell.Current.GoToAsync(nameof(TrainingDetailPage), new Dictionary<string, object>
            {
                { "CourseObj", course }
            });
        }
    }
}