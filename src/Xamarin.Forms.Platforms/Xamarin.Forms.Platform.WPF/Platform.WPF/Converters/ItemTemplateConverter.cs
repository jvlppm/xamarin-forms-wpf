namespace Xamarin.Forms.Platform.WPF.Converters
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class ItemTemplateConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var template = value as Xamarin.Forms.DataTemplate;
            var uc = new FrameworkElementFactory(typeof(UserControl));
            if (template != null)
            {
                uc.SetBinding(UserControl.ContentProperty, new MultiBinding
                {
                    Converter = new CellToViewConverter(),
                    Bindings =
                    {
                        new Binding(),
                        new Binding { Source = template }
                    }
                });
            }
            else
            {
                uc.SetBinding(UserControl.ContentProperty, new Binding
                {
                    Converter = new ModelToViewConverter()
                });
            }

            return new DataTemplate { VisualTree = uc };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
