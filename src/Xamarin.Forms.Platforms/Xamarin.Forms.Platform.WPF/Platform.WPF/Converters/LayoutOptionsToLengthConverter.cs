using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.WPF.Converters
{
    class LayoutOptionsToLengthConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var options = (LayoutOptions)value;
            return new System.Windows.GridLength(1, options.Expands
                        ? System.Windows.GridUnitType.Star
                        : System.Windows.GridUnitType.Auto);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
