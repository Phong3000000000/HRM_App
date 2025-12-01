using System.Globalization;

namespace HRMApp.Converters
{
    public class StringEqConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // So sánh giá trị đang chọn (value) với tham số (parameter - ví dụ "A", "B")
            return value?.ToString() == parameter?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Nếu được chọn (True) thì trả về tham số ("A", "B"...)
            return (bool)value ? parameter : null;
        }
    }
}