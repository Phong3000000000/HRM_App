// File: HRMApp/ViewModel/QuestionViewModel.cs

using CommunityToolkit.Mvvm.ComponentModel;
using HRMApp.Model.Training;
using Microsoft.Maui.Graphics;

namespace HRMApp.ViewModel
{
    // Class này dùng để quản lý trạng thái của từng câu hỏi trên giao diện
    public partial class QuestionViewModel : ObservableObject
    {
        // Chứa dữ liệu gốc từ API (Nội dung câu hỏi, các đáp án A, B, C, D...)
        public CourseQuestionDto Dto { get; set; }

        // Lưu đáp án người dùng đang chọn ("A", "B", "C", "D")
        [ObservableProperty]
        private string selectedAnswer;

        // True: Chế độ xem kết quả (không cho chọn lại)
        // False: Đang làm bài (cho phép chọn)
        [ObservableProperty]
        private bool isReadOnly;

        // Màu nền của khung câu hỏi (Trắng khi làm bài, Xanh/Đỏ khi xem kết quả)
        [ObservableProperty]
        private Color resultColor = Colors.White;

        // Icon hiển thị đúng/sai (icon_check.png hoặc icon_cross.png)
        [ObservableProperty]
        private string resultIcon;

        // Text hiển thị kết quả: "Chính xác" hoặc "Sai. Đáp án đúng là..."
        [ObservableProperty]
        private string resultText;

        // Màu chữ của kết quả (Xanh hoặc Đỏ)
        [ObservableProperty]
        private Color resultTextColor = Colors.Black;

        // ====================================================================
        // ✅ THÊM CÁC THUỘC TÍNH HELPER CHO RADIOBUTTON BINDING
        // ====================================================================

        public bool IsAnswerA
        {
            get => SelectedAnswer == "A";
            set
            {
                // Chỉ cho phép gán nếu đang trong chế độ thi (IsReadOnly = false)
                if (value && !IsReadOnly)
                    SelectedAnswer = "A";
                // Đảm bảo không cho phép bỏ chọn nếu RadioButton được chọn
                else if (!value && SelectedAnswer == "A" && !IsReadOnly)
                    SelectedAnswer = null;
            }
        }

        public bool IsAnswerB
        {
            get => SelectedAnswer == "B";
            set
            {
                if (value && !IsReadOnly)
                    SelectedAnswer = "B";
                else if (!value && SelectedAnswer == "B" && !IsReadOnly)
                    SelectedAnswer = null;
            }
        }

        public bool IsAnswerC
        {
            get => SelectedAnswer == "C";
            set
            {
                if (value && !IsReadOnly)
                    SelectedAnswer = "C";
                else if (!value && SelectedAnswer == "C" && !IsReadOnly)
                    SelectedAnswer = null;
            }
        }

        public bool IsAnswerD
        {
            get => SelectedAnswer == "D";
            set
            {
                if (value && !IsReadOnly)
                    SelectedAnswer = "D";
                else if (!value && SelectedAnswer == "D" && !IsReadOnly)
                    SelectedAnswer = null;
            }
        }

        // Cần ghi đè phương thức này để thông báo cập nhật các thuộc tính IsAnswerX
        partial void OnSelectedAnswerChanged(string oldValue, string newValue)
        {
            // Thông báo cập nhật cho tất cả các thuộc tính liên quan
            OnPropertyChanged(nameof(IsAnswerA));
            OnPropertyChanged(nameof(IsAnswerB));
            OnPropertyChanged(nameof(IsAnswerC));
            OnPropertyChanged(nameof(IsAnswerD));
        }

    }
}