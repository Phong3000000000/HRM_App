using System.Globalization;
using Microsoft.Maui.Graphics;

namespace HRMApp.Converters
{
    public class BoolToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected && isSelected)
            {
                // N?n nh?t cho ngày ???c ch?n
                return Color.FromArgb("#FFF3F0");
            }
            
            // N?n tr?ng cho ngày bình th??ng
            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}