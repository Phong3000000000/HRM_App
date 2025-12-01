using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApp.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isLogin = (bool)value;
            bool isTabLogin = bool.Parse(parameter.ToString());

            if (isLogin == isTabLogin)
                return Color.FromArgb("#34425a");
            return Color.FromArgb("#7d8191");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
