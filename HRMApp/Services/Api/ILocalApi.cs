using HRMApp.Model; // Giả định EmployeeInfo, ApiResponse được định nghĩa ở đây
using HRMApp.Model.Dto;
using HRMApp.Model.Notification; // ✅ Thêm using này
using HRMApp.Model.Training;
using HRMApp.View;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRMApp.Services.Api
{
    public interface ILocalApi
    {
        #region Attendance APIs
        [Post("/api/attendance/checkin")]
        Task<ApiResponse> CheckinAsync([Body] CheckinRequest model);

        [Post("/api/attendance/checkout")]
        Task<ApiResponse> CheckoutAsync([Body] CheckoutRequest model);

        [Get("/api/attendance/status/{employeeId}")]
        Task<AttendanceStatusResponse> GetAttendanceStatusAsync(Guid employeeId);
        #endregion

        #region Payroll APIs
        [Get("/api/payroll/performance/{employeeId}")]
        Task<PayrollPerformanceResponse> GetPayrollPerformanceAsync(Guid employeeId, [Query] string? month = null);


        // ✅ THÊM: Phương thức backup để lấy raw response
        [Get("/api/payroll/performance/{employeeId}")]
        Task<HttpResponseMessage> GetPayrollPerformanceRawAsync(Guid employeeId, [Query] string? month = null);

        [Get("/api/payroll/daily/{employeeId}")]
        Task<PayrollDailyResponse> GetPayrollDailyAsync(Guid employeeId, [Query] string? month = null);
        #endregion

        #region Request APIs
        [Post("/api/Request")]
        Task<ApiResponse> CreateRequestAsync([Body] CreateRequestDto model);

        [Get("/api/Request")]
        Task<RequestListResponse> GetRequestsAsync(
            [Query] string? q = null,
            [Query] int current = 1,
            [Query] int pageSize = 20,
            [Query] string sort = "CreatedAt desc"
        );

        [Put("/api/Request/process/{requestId}")]
        Task<ApiResponse> ProcessRequestAsync(Guid requestId, [Body] ProcessRequestDto model);
        #endregion

        #region FCM Token APIs
        // ✅ THÊM API endpoint cho FCM token
        [Post("/api/FcmToken/save")]
        Task<HttpResponseMessage> SaveTokenAsync([Body] FcmToken tokenModel);
        #endregion

        #region Notification APIs
        [Get("/api/notifications/list")]
        Task<NotificationListResponse> GetNotificationsAsync(
            [Query] string? q = null,
            [Query] int current = 1,
            [Query] int pageSize = 20,
            [Query] string sort = "Id desc");

        [Put("/api/notifications/mark-as-read/{notificationId}")]
        Task<Model.ApiResponse<dynamic>> MarkNotificationAsReadAsync(
            Guid notificationId,
            [Query] Guid userId);
        #endregion

        [Post("/api/DeviceStatus/update")]
        Task<ApiResponse> UpdateDeviceStatusAsync([Body] DeviceStatusUpdateRequest request);
        //  Device Status APIs
        [Get("/api/DeviceStatus/all")]
        Task<List<DeviceStatusModel>> GetAllDevicesAsync();

        [Get("/api/DeviceStatus/open")]
        Task<List<DeviceStatusModel>> GetOpenDevicesAsync();

        [Get("/api/DeviceStatus/closed")]
        Task<List<DeviceStatusModel>> GetClosedDevicesAsync();

        [Get("/api/DeviceStatus/check/{deviceId}")]
        Task<DeviceStatusModel> CheckDeviceStatusAsync(string deviceId);


        #region TRAINING API

        // 1. Lấy danh sách Đề thi (Courses)
        [Get("/api/Course")]
        Task<CourseListResponse> GetCoursesAsync(
            [Query] string? q = null,
            [Query] Guid? employeeId = null,
            [Query] int current = 1,
            [Query] int pageSize = 100);

        // 2. Lấy danh sách Câu hỏi của 1 đề
        [Get("/api/CourseQuestions")]
        Task<CourseQuestionListResponse> GetQuestionsAsync(
            [Query] Guid courseId,
            [Query] int pageSize = 100);

        // 3. Nộp bài thi (Submit)
        [Post("/api/CourseResults/bulk-submit")]
        Task<ApiResponse> SubmitBulkAnswersAsync([Body] BulkSubmitRequest request);

        // 4. Xem điểm số (để biết đã làm bài chưa và kết quả thế nào)
        [Get("/api/CourseResults/score")]
        Task<Model.ApiResponse<List<CourseScoreDto>>> GetScoreAsync(
            [Query] Guid employeeId,

            [Query] Guid courseId);

        // 5. Xem chi tiết đáp án đã chọn (để tô màu đúng/sai khi xem lại)
        [Get("/api/CourseResults")]
        Task<CourseResultListResponse> GetCourseResultsAsync(
            [Query] Guid employeeId,
            [Query] Guid courseId,
            [Query] int pageSize = 100);

        #endregion

        // Trong file ILocalApi.cs, thêm vào region WorkSchedule hoặc tạo mới
        #region WorkSchedule APIs

        [Get("/api/workschedule")]
        Task<Model.ApiResponse<List<WorkScheduleResponse>>> GetWorkSchedulesAsync(
          [Query] Guid? employeeId,
          [Query] string from,
          [Query] string to,
          [Query] int pageSize = 100);
       // Task<HttpResponseMessage> GetWorkSchedulesAsync(
       //[Query] Guid? employeeId,
       //[Query] string from,
       //[Query] string to,
       //[Query] int pageSize = 100);

        #endregion


    }

    #region Attendance Models
    public class AttendanceStatusResponse
    {
        public bool Success { get; set; }
        public string Status { get; set; }  // "NotCheckedIn", "CheckedIn", "CheckedOut"
        public string Message { get; set; }
    }

    public class CheckinRequest
    {
        public string EmployeeId { get; set; }
        public string WifiName { get; set; }
        public string Bssid { get; set; }
        public string Shift { get; set; }
    }

    public class CheckoutRequest
    {
        public string EmployeeId { get; set; }
        public string WifiName { get; set; }
        public string Bssid { get; set; }
    }
    #endregion

    #region Payroll Models
    public class PayrollPerformanceResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public PerformanceDataWrapper[] Data { get; set; }
    }

    public class PerformanceDataWrapper
    {
        public PerformanceResult[] Result { get; set; }
    }

    public class PerformanceResult
    {
        public string Month { get; set; }
        public EmployeeInfo ThongTinNhanVien { get; set; }
        public AttendanceSummary ChamCong { get; set; }
        public SalarySummary Luong { get; set; }
    }

    public class AttendanceSummary
    {
        public int SoCongPhanCong { get; set; }
        public decimal SoCongThucTe { get; set; }
        public int SoLanDiMuon { get; set; }
        public int SoLanVeSom { get; set; }
        public int SoLanVang { get; set; }
    }

    public class SalarySummary
    {
        public decimal TongPhuCap { get; set; }
        public decimal TongThuong { get; set; }
        public decimal TongPhat { get; set; }
        public decimal LuongMotNgayCong { get; set; }
        public decimal LuongMotGio { get; set; }
        public decimal SoGioOT { get; set; }
        public decimal TongGioOTThucTe { get; set; }
        public decimal HeSoOT { get; set; }
        public decimal LuongOT { get; set; }
        public decimal Bhxh { get; set; }
        public decimal Bhyt { get; set; }
        public decimal Bhtn { get; set; }
        public decimal BaoHiem { get; set; }
        public decimal LuongThucNhan { get; set; }
    }

    public class PayrollDailyResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public DailyDataWrapper[] Data { get; set; }
    }

    public class DailyDataWrapper
    {
        public PayrollDailyItem[] Result { get; set; }
    }

    public class PayrollDailyItem
    {
        public string Ngay { get; set; }
        public string TrangThai { get; set; }
        public decimal SoCong { get; set; }
        public decimal PhuCap { get; set; }
        public decimal Thuong { get; set; }
        public decimal GioOtDuocDuyet { get; set; }
        public decimal GioOtThucTe { get; set; }
        public decimal LuongOt { get; set; }
        public decimal Phat { get; set; }
        public decimal LuongNgay { get; set; }
        public string GhiChu { get; set; }
    }
    #endregion

    #region Request Models
    public class CreateRequestDto
    {
        public Guid EmployeeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        // Có thể để int hoặc Enum tùy vào cách bạn truyền lên Server
        public int Category { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string? EndTime { get; set; }
    }

    public class ProcessRequestDto
    {
        public string NewStatus { get; set; } = string.Empty;
        public Guid ApproverUserId { get; set; }
        public decimal ApprovedHours { get; set; }
        public decimal Rate { get; set; } = 1.5m;
        public string? Reason { get; set; }
    }

    public class RequestListResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public RequestDataWrapper[] Data { get; set; }
    }

    public class RequestDataWrapper
    {
        public RequestListMeta Meta { get; set; }
        // ✅ QUAN TRỌNG: Sử dụng class từ HRMApp.Model (cái có Enum)
        public List<HRMApp.Model.RequestItem> Result { get; set; }
    }

    public class RequestListMeta
    {
        public int Current { get; set; }
        public int PageSize { get; set; }
        public int Pages { get; set; }
        public int Total { get; set; }
    }

    // ❌ ĐÃ XÓA CLASS RequestItem CŨ Ở ĐÂY ĐỂ TRÁNH XUNG ĐỘT
    #endregion

    #region Employee Models
    public class EmployeeInfo
    {
        public Guid EmployeeId { get; set; }
        public string ContractType { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal InsuranceSalary { get; set; } // ✅ Đảm bảo property này có mặt
    }
    #endregion

    #region Notification Models
    public class NotificationListResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public NotificationDataWrapper[] Data { get; set; }
    }

    public class NotificationDataWrapper
    {
        public NotificationListMeta Meta { get; set; }
        public List<NotificationItem> Result { get; set; }
    }

    public class NotificationListMeta
    {
        public int Current { get; set; }
        public int PageSize { get; set; }
        public int Pages { get; set; }
        public int Total { get; set; }
    }

    public class NotificationItem
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("readAt")]
        public DateTime? ReadAt { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("actorId")]
        public Guid? ActorId { get; set; }

        [JsonPropertyName("actorName")]
        public string ActorName { get; set; } = string.Empty;

        [JsonPropertyName("actionUrl")]
        public string? ActionUrl { get; set; }

        // Convert to SignalRNotification
        public SignalRNotification ToSignalRNotification()
        {
            return new SignalRNotification
            {
                Id = this.Id,
                UserId = this.UserId,
                Type = this.Type,
                Title = this.Title,
                Content = this.Content,
                CreatedAt = this.CreatedAt,
                IsRead = this.ReadAt.HasValue
            };
        }
    }
    #endregion



}