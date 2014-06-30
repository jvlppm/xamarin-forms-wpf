using System;

namespace Xamarin.Forms.Platform.WPF.Converters
{
    class VisibilityNegationConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as System.Windows.Visibility? == System.Windows.Visibility.Visible) ?
                System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as System.Windows.Visibility? != System.Windows.Visibility.Visible) ?
                System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }
    }
}
