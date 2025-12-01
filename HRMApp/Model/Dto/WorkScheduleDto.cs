using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HRMApp.Model.Dto
{
    // Class hứng dữ liệu API trả về
    public class WorkScheduleDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("employeeId")]
        public Guid EmployeeId { get; set; }

        [JsonPropertyName("employeeFullName")]
        public string EmployeeFullName { get; set; }

        // 👇 ĐỂ STRING: Tránh lỗi khi Backend trả về DateOnly
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("shiftTemplateId")]
        public Guid ShiftTemplateId { get; set; }

        [JsonPropertyName("shiftName")]
        public string ShiftName { get; set; }

        // 👇 ĐỂ STRING: Tránh lỗi khi Backend trả về TimeSpan
        [JsonPropertyName("shiftStartTime")]
        public string ShiftStartTime { get; set; }

        // 👇 ĐỂ STRING
        [JsonPropertyName("shiftEndTime")]
        public string ShiftEndTime { get; set; }

        [JsonPropertyName("totalWorkingHours")]
        public decimal TotalWorkingHours { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("workDay")]
        public double WorkDay { get; set; }
    }

    // Class bao bọc response (dựa trên cấu trúc OKSingle của Controller)
    public class WorkScheduleResponse
    {
        [JsonPropertyName("meta")]
        public object Meta { get; set; }

        [JsonPropertyName("result")]
        public List<WorkScheduleDto> Result { get; set; }
    }
}