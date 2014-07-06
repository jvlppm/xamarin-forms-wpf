namespace Xamarin.Forms.Platform.WPF.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Data;

    class CellToViewConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var item = values.Length > 0 ? values[0] as Element : null;
            if (item == null)
                return null;

            var template = values.Length > 1 ? values[1] as DataTemplate : null;
            if(template != null)
            {
                var templatedItem = template.CreateContent() as Element;
                templatedItem.BindingContext = item;
                item = templatedItem;
            }

            return RendererFactory.Create(item).Element;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
