using System.Text.Json.Serialization;
using Microsoft.Maui.Graphics;

namespace HRMApp.Model.Training
{
    // --- 1. Danh sách Khóa học ---
    public class CourseListResponse
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("data")]
        public List<CourseDataWrapper> Data { get; set; } // ✅ Luôn là List
    }

    public class CourseDataWrapper
    {
        [JsonPropertyName("result")]
        public List<CourseDto> Result { get; set; }
    }

    public class CourseDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("classCode")]
        public string ClassCode { get; set; }
        [JsonPropertyName("passThreshold")]
        public int PassThreshold { get; set; }
        [JsonPropertyName("questionCount")]
        public int QuestionCount { get; set; }

        // Thuộc tính UI
        [JsonIgnore]
        public string StatusDisplay { get; set; } = "Sẵn sàng thi";
        [JsonIgnore]
        public Color StatusColor { get; set; } = Colors.Blue;
    }

    // --- 2. Danh sách Câu hỏi ---
    public class CourseQuestionListResponse
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("data")]
        public List<QuestionDataWrapper> Data { get; set; } // ✅ Luôn là List
    }

    public class QuestionDataWrapper
    {
        [JsonPropertyName("result")]
        public List<CourseQuestionDto> Result { get; set; }
    }

    public class CourseQuestionDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("courseId")]
        public Guid CourseId { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("a")]
        public string A { get; set; }
        [JsonPropertyName("b")]
        public string B { get; set; }
        [JsonPropertyName("c")]
        public string C { get; set; }
        [JsonPropertyName("d")]
        public string D { get; set; }
        [JsonPropertyName("correct")]
        public string Correct { get; set; }
    }

    // --- 3. Điểm số (Score) ---
    public class CourseScoreResponse
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        // 🛑 SỬA QUAN TRỌNG: Chuyển thành List để khớp với JSON [ ... ]
        [JsonPropertyName("data")]
        public List<CourseScoreDto> Data { get; set; }
    }

    public class CourseScoreDto
    {
        [JsonPropertyName("totalQuestions")]
        public int TotalQuestions { get; set; }
        [JsonPropertyName("answered")]
        public int Answered { get; set; }
        [JsonPropertyName("correct")]
        public int Correct { get; set; }
        [JsonPropertyName("scorePercent")]
        public decimal ScorePercent { get; set; }
        [JsonPropertyName("passed")]
        public bool Passed { get; set; }
    }

    // --- 4. Kết quả chi tiết ---
    public class CourseResultListResponse
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("data")]
        public List<ResultDataWrapper> Data { get; set; } // ✅ Luôn là List
    }

    public class ResultDataWrapper
    {
        [JsonPropertyName("result")]
        public List<CourseResultDto> Result { get; set; }
    }

    public class CourseResultDto
    {
        [JsonPropertyName("questionId")]
        public Guid QuestionId { get; set; }
        [JsonPropertyName("chosen")]
        public string Chosen { get; set; }
        [JsonPropertyName("isCorrect")]
        public bool IsCorrect { get; set; }
    }

    // --- 5. Request Nộp bài ---
    public class BulkSubmitRequest
    {
        [JsonPropertyName("employeeId")]
        public Guid EmployeeId { get; set; }
        [JsonPropertyName("courseId")]
        public Guid CourseId { get; set; }
        [JsonPropertyName("answers")]
        public List<SubmitAnswerDto> Answers { get; set; }
    }

    public class SubmitAnswerDto
    {
        [JsonPropertyName("questionId")]
        public Guid QuestionId { get; set; }
        [JsonPropertyName("chosen")]
        public string Chosen { get; set; }
    }

    // --- Helper Response ---
    //public class ApiResponse
    //{
    //    [JsonPropertyName("statusCode")]
    //    public int StatusCode { get; set; }
    //    [JsonPropertyName("message")]
    //    public string Message { get; set; }

    //    // Helper để check nhanh
    //    public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;
    //    public bool Success => IsSuccess;
    //}
}