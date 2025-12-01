using System;
using Microsoft.Maui.Graphics;
using System.Text.Json.Serialization;

namespace HRMApp.Model
{
    public class RequestItem
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RequestCategory Category { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RequestStatus Status { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Date { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        // ✅ SỬA 1: Đổi sang string để hứng dữ liệu từ Server (tránh lỗi Deserializing)
        public string CreatedAt { get; set; }

        // ✅ SỬA 2: Thêm cái này để hiển thị ra giao diện cho đẹp (HH:mm dd/MM/yyyy)
        public string CreatedAtDisplay
        {
            get
            {
                if (DateTime.TryParse(CreatedAt, out var dt))
                {
                    return dt.ToString("HH:mm dd/MM/yyyy");
                }
                return CreatedAt; // Nếu lỗi thì hiện nguyên gốc
            }
        }

        // --- CÁC PROPERTY HỖ TRỢ HIỂN THỊ ---

        public string StatusDisplay => Status switch
        {
            RequestStatus.pending => "Chờ duyệt",
            RequestStatus.approved => "Đã duyệt",
            RequestStatus.rejected => "Từ chối",
            RequestStatus.cancelled => "Đã hủy",
            _ => "Khác"
        };

        public Color StatusColor => Status switch
        {
            RequestStatus.pending => Colors.Orange,
            RequestStatus.approved => Colors.Green,
            RequestStatus.rejected => Colors.Red,
            RequestStatus.cancelled => Colors.Gray,
            _ => Colors.Black
        };

        public string DisplayDate
        {
            get
            {
                if (FromDate.HasValue) return FromDate.Value.ToString("dd/MM/yyyy");
                if (Date.HasValue) return Date.Value.ToString("dd/MM/yyyy");
                return "N/A";
            }
        }

        public string DisplayDateRange
        {
            get
            {
                if (Category == RequestCategory.ot) return DisplayDate;

                if (FromDate.HasValue)
                {
                    if (!ToDate.HasValue || FromDate.Value.Date == ToDate.Value.Date)
                    {
                        return FromDate.Value.ToString("dd/MM/yyyy");
                    }
                    return $"Từ {FromDate.Value:dd/MM} đến {ToDate.Value:dd/MM/yyyy}";
                }
                return DisplayDate;
            }
        }
    }
}