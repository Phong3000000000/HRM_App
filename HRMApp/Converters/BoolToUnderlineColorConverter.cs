using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApp.Converters
{
    public class BoolToUnderlineColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isLogin = (bool)value;
            bool isTabLogin = bool.Parse(parameter?.ToString());

            return isLogin == isTabLogin
                ? Color.FromArgb("#007ACC") // active: xanh
                : Color.FromArgb("#cccccc"); // inactive: xám
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
