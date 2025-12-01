using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics; // Cần thiết để dùng Color và Colors

namespace HRMApp.Model
{
    public class DaySchedule
    {
        public string DayOfWeek { get; set; }
        public string Date { get; set; }
        public ObservableCollection<ShiftItem> Shifts { get; set; }

        // ✅ THÊM: Properties để tùy chỉnh border cho ngày được chọn
        public bool IsSelected { get; set; }
        public Color SelectedBorderColor { get; set; } = Color.FromArgb("#FF6B35"); // Màu cam đỏ
        public double SelectedBorderRadius { get; set; } = 12;
        public double SelectedBorderThickness { get; set; } = 2;
        public Color SelectedBackgroundColor { get; set; } = Color.FromArgb("#FFF3F0"); // N
    }

    public class ShiftItem
    {
        public string ShiftName { get; set; }
        public string Status { get; set; }

        // Màu động cho UI
        public Color BgColor { get; set; } = Colors.Transparent;
        public Color BorderColor { get; set; } = Colors.Transparent;
        public Color ShiftTextColor { get; set; } = Color.FromArgb("#1568b2");
        public Color StatusTextColor { get; set; } = Colors.Black;
    }
}