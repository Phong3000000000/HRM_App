using System.Globalization;
using Microsoft.Maui.Graphics;

namespace HRMApp.Converters
{
    public class BoolToBorderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected && isSelected)
            {
                // Border màu cam đỏ cho ngày được chọn
                return Color.FromArgb("#FF6B35");
            }
            
            // Border xám nhạt cho ngày bình thường
            return Color.FromArgb("#E0E0E0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}