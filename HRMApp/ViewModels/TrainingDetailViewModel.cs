// File: HRMApp/ViewModel/TrainingDetailViewModel.cs

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HRMApp.Model.Training;
using HRMApp.Services.Api;
using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;

namespace HRMApp.ViewModel
{
    [QueryProperty(nameof(Course), "CourseObj")]
    public partial class TrainingDetailViewModel : ObservableObject
    {
        private readonly ILocalApi _api;
        // ✅ SỬA: Dùng ID của nhân viên Huỳnh Thanh Sơn (776ec5cf-8349-4eb9-a6ec-73c75a7c58db) 
        // để có dữ liệu bài đã làm trong dbhrm.sql.
        private Guid _employeeId;

        [ObservableProperty] private CourseDto course;
        [ObservableProperty] private ObservableCollection<QuestionViewModel> questions;
        [ObservableProperty] private bool isLoading;
        [ObservableProperty] private bool isReviewMode;
        [ObservableProperty] private string scoreSummary;

        public TrainingDetailViewModel(ILocalApi api)
        {
            _api = api;
            Questions = new ObservableCollection<QuestionViewModel>();
        }

        public async Task InitializeAsync()
        {
            string eIdString = await SecureStorage.Default.GetAsync("employeeid");
            if (Guid.TryParse(eIdString, out Guid parsedId))
            {
                _employeeId = parsedId;
            }

            if (Course == null) return;
            IsLoading = true;
            try
            {
                // 1. Lấy điểm số
                var scoreRes = await _api.GetScoreAsync(_employeeId, Course.Id);

                // ✅ Đã sửa: Kiểm tra List có dữ liệu hay không
                if (scoreRes.Data != null && scoreRes.Data.Count > 0)
                {
                    // ✅ LẤY PHẦN TỬ ĐẦU TIÊN [0]
                    var s = scoreRes.Data[0];

                    // Kiểm tra xem đã trả lời câu nào chưa
                    if (s.Answered > 0)
                    {
                        // Đã làm bài -> Xem kết quả
                        IsReviewMode = true;
                        ScoreSummary = $"Kết quả: {s.Correct}/{s.TotalQuestions} câu đúng ({s.ScorePercent}%) - {(s.Passed ? "ĐẠT" : "RỚT")}";
                        await LoadReviewData();
                    }
                    else
                    {
                        // Có data nhưng chưa làm -> Thi
                        IsReviewMode = false;
                        ScoreSummary = "";
                        await LoadExamData();
                    }
                }
                else
                {
                    // Chưa có data -> Thi
                    IsReviewMode = false;
                    ScoreSummary = "";
                    await LoadExamData();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Lỗi Init", ex.Message, "OK");
            }
            finally { IsLoading = false; }
        }

        private async Task LoadExamData()
        {
            var res = await _api.GetQuestionsAsync(Course.Id, 100);

            // ✅ Đã sửa: Check Data là List và có phần tử đầu
            if (res.Data != null && res.Data.Count > 0)
            {
                Questions.Clear();
                // Lấy Result từ phần tử đầu tiên
                var questionList = res.Data[0].Result;
                if (questionList != null)
                {
                    foreach (var q in questionList)
                    {
                        // ✅ Gán IsReadOnly = false để cho phép chọn đáp án
                        Questions.Add(new QuestionViewModel { Dto = q, IsReadOnly = false });
                    }
                }
            }
        }

        private async Task LoadReviewData()
        {
            var qRes = await _api.GetQuestionsAsync(Course.Id, 100);
            var rRes = await _api.GetCourseResultsAsync(_employeeId, Course.Id, 100);

            // ✅ Đã sửa: Check Data cho cả 2 API
            if (qRes.Data != null && qRes.Data.Count > 0 &&
                rRes.Data != null && rRes.Data.Count > 0)
            {
                Questions.Clear();
                var questionsList = qRes.Data[0].Result;
                var resultsList = rRes.Data[0].Result;

                if (questionsList == null || resultsList == null) return;

                foreach (var q in questionsList)
                {
                    var myAns = resultsList.FirstOrDefault(x => x.QuestionId == q.Id);
                    var vm = new QuestionViewModel
                    {
                        Dto = q,
                        IsReadOnly = true,
                        // ✅ Đã sửa: Gán SelectedAnswer để kích hoạt logic hiển thị đáp án đã chọn
                        SelectedAnswer = myAns?.Chosen
                    };

                    if (myAns != null)
                    {
                        if (myAns.IsCorrect)
                        {
                            vm.ResultColor = Color.FromArgb("#E8F5E9");
                            vm.ResultIcon = "icon_check.png";
                            vm.ResultText = "Chính xác";
                            vm.ResultTextColor = Colors.Green;
                        }
                        else
                        {
                            vm.ResultColor = Color.FromArgb("#FFEBEE");
                            // TODO: Bạn cần có file icon_cross.png trong thư mục Resources/Images
                            vm.ResultIcon = "icon_cross.png";
                            vm.ResultText = $"Sai. Đáp án đúng: {q.Correct}";
                            vm.ResultTextColor = Colors.Red;
                        }
                    }
                    Questions.Add(vm);
                }
            }
        }

        [RelayCommand]
        public async Task Submit()
        {
            if (IsReviewMode) return;
            bool confirm = await App.Current.MainPage.DisplayAlert("Nộp bài", "Bạn có chắc chắn muốn nộp?", "Có", "Không");
            if (!confirm) return;

            IsLoading = true;
            try
            {
                var req = new BulkSubmitRequest
                {
                    EmployeeId = _employeeId,
                    CourseId = Course.Id,
                    Answers = Questions
                        .Where(q => !string.IsNullOrEmpty(q.SelectedAnswer))
                        .Select(q => new SubmitAnswerDto { QuestionId = q.Dto.Id, Chosen = q.SelectedAnswer })
                        .ToList()
                };

                var res = await _api.SubmitBulkAnswersAsync(req);

                // Sử dụng IsSuccess từ class ApiResponse vừa thêm
                if (res.IsSuccess)
                {
                    await App.Current.MainPage.DisplayAlert("Thành công", "Đã nộp bài!", "OK");
                    await InitializeAsync();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Lỗi", res.Message, "OK");
                }
            }
            catch (Exception ex) { await App.Current.MainPage.DisplayAlert("Lỗi Submit", ex.Message, "OK"); }
            finally { IsLoading = false; }
        }

        // ====================================================================
        // ✅ THÊM LỆNH CHO NÚT LÀM LẠI BÀI (RESET STATUS)
        // ====================================================================

        //[RelayCommand]
        //public async Task ResetStatus()
        //{
        //    bool confirm = await App.Current.MainPage.DisplayAlert("Xác nhận", "Bạn có muốn làm lại bài thi này? Kết quả cũ sẽ bị reset trạng thái.", "Có", "Không");
        //    if (!confirm) return;

        //    IsLoading = true;
        //    try
        //    {
        //        // Giả định bạn có một API để Reset trạng thái học/thi của nhân viên
        //        // Nếu không có, bạn cần thêm API này vào backend .NET Core của mình.
        //        // Ở đây tôi dùng API giả định 'ResetTrainingStatusAsync' trong ILocalApi
        //        var res = await _api.ResetTrainingStatusAsync(_employeeId, Course.Id);

        //        if (res.IsSuccess)
        //        {
        //            await App.Current.MainPage.DisplayAlert("Thành công", "Bài thi đã được reset! Bạn có thể làm lại.", "OK");
        //            await InitializeAsync(); // Load lại để chuyển sang chế độ làm bài
        //        }
        //        else
        //        {
        //            await App.Current.MainPage.DisplayAlert("Lỗi Reset", res.Message, "OK");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await App.Current.MainPage.DisplayAlert("Lỗi Reset", ex.Message, "OK");
        //    }
        //    finally
        //    {
        //        IsLoading = false;
        //    }
        //}
    }
}