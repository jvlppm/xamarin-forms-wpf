using System;

namespace Xamarin.Forms.Platform.WPF.Converters
{
    class BooleanToVisibilityConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value as bool? == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value as System.Windows.Visibility? == System.Windows.Visibility.Visible;
        }
    }
}
