using System.Globalization;
using Microsoft.Maui.Controls.Shapes;

namespace HRMApp.Converters
{
    public class RadiusToShapeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double radius)
            {
                return new RoundRectangle
                {
                    CornerRadius = new CornerRadius(radius)
                };
            }
            
            // Default radius
            return new RoundRectangle
            {
                CornerRadius = new CornerRadius(12)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}