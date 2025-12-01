using Android.Media.TV;
using HRMApp.Model;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HRMApp.Services.Api
{
    public interface IHrmApi
    {
        [Post("/api/Auth/login")]
        Task<LoginResponse> LoginAsync([Body] LoginRequest request);

        [Post("/api/auth/register")]
        Task<ApiResponse> RegisterAsync([Body] RegisterRequest request);

        // Test JWT token (có header Authorization)
        [Get("/api/test/protected")]
        Task<ApiResponse> TestProtectedAsync(); // ✅ Không cần Header

        [Post("/api/Auth/change-password")]
        Task<Refit.ApiResponse<string>> ChangePasswordAsync([Body] ChangePasswordRequest request);

        #region REPORT APIs

        // ✅ THÊM API endpoint để lấy URL PDF hồ sơ nhân viên
        [Get("/api/PdfReport/profile-report-pdf-mobile/{employeeId}")]
        Task<ReportDownloadResponse> GetProfileReportDownloadUrl(
            [AliasAs("employeeId")] Guid employeeId,
            [Query] string returnType = "url");

        #endregion

        #region SALARY REPORT APIs

        // 1. API Tìm kiếm danh sách lương (để lấy SalaryId)
        // GET /api/Salary?q={employeeCode}&sort=PayrollRun.Period desc
        [Get("/api/Salary")]
        Task<SalarySearchResponse> GetSalariesAsync(
            [Query] string q,
            [Query] int current = 1,
            [Query] int pageSize = 20,
            [Query] string sort = "PayrollRun.Period desc");

        // 2. API Tải báo cáo lương (Payslip)
        // Giả sử endpoint là /api/PdfReport/payslip-report-pdf-mobile/{salaryId}
        // Bạn cần xác nhận endpoint chính xác từ Backend của bạn
        [Get("/api/PdfReport/payslip-report-pdf-mobile/{salaryId}")]
        Task<ReportDownloadResponse> GetPayslipReportDownloadUrl(
            [AliasAs("salaryId")] Guid salaryId,
            [Query] string returnType = "url");

        #endregion
    }
    public class ReportDownloadResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("downloadUrl")]
        public string DownloadUrl { get; set; } = string.Empty;

        [JsonPropertyName("fileName")]
        public string FileName { get; set; } = string.Empty;
    }


    public class SalarySearchResponse
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("data")]
        public List<SalaryDataWrapper> Data { get; set; }
    }

    public class SalaryDataWrapper
    {
        [JsonPropertyName("result")]
        public List<SalaryRecord> Result { get; set; }
    }


    //tạo pdf bảng lương
    public class SalaryRecord
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; } // Đây là SalaryId cần lấy

        [JsonPropertyName("period")]
        public string Period { get; set; } // Ví dụ: "2025-10"

        [JsonPropertyName("gross")]
        public decimal Gross { get; set; }

        [JsonPropertyName("net")]
        public decimal Net { get; set; }

        // Các trường khác nếu cần hiển thị
    }
}
