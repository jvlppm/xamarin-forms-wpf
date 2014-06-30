using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.WPF.Converters
{
    class ColorToWPFBrushConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var color = value as Color?;
            if(color == null || color == default(Color))
                return System.Windows.DependencyProperty.UnsetValue;

            return color.Value.ToWPFBrush();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var brush = value as System.Windows.Media.SolidColorBrush;
            if (brush == null)
                return System.Windows.DependencyProperty.UnsetValue;

            return Color.FromRgba(brush.Color.R, brush.Color.G, brush.Color.B, brush.Color.A);
        }
    }
}
