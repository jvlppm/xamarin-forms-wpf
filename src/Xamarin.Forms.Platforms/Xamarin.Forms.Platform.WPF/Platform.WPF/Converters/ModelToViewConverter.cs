using System;

namespace Xamarin.Forms.Platform.WPF.Converters
{
    class ModelToViewConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ren = RendererFactory.Create((Element)value);
            if(ren != null)
                return ren.Element;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
