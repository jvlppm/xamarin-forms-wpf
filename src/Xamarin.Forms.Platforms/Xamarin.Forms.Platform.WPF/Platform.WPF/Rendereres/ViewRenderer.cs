using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using Xamarin.Forms.Platform.WPF.Rendereres;

[assembly: ExportRenderer(typeof(View), typeof(ViewRenderer<View, object>))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    public class ViewRenderer : ViewRenderer<View, System.Windows.FrameworkElement> { }

    public class ViewRenderer<TModel, T> : VisualElementRenderer<TModel, T>
        where TModel : View
    {
        public ViewRenderer()
        {
            // TODO: Style based renderer, bindings and converters.

            HandleProperty(View.HorizontalOptionsProperty, Handle_HorizontalOptionsProperty);
            HandleProperty(View.VerticalOptionsProperty, Handle_VerticalOptionsProperty);
        }

        protected virtual bool Handle_VerticalOptionsProperty(BindableProperty property)
        {
            switch (Model.VerticalOptions.Alignment)
            {
                case LayoutAlignment.Start:
                    Element.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    break;
                case LayoutAlignment.Center:
                    Element.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    break;
                case LayoutAlignment.End:
                    Element.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                    break;
                case LayoutAlignment.Fill:
                    Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                    break;
            }

            return true;
        }

        protected virtual bool Handle_HorizontalOptionsProperty(BindableProperty property)
        {
            switch (Model.HorizontalOptions.Alignment)
            {
                case LayoutAlignment.Start:
                    Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    break;
                case LayoutAlignment.Center:
                    Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    break;
                case LayoutAlignment.End:
                    Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    break;
                case LayoutAlignment.Fill:
                    Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    break;
            }

            return true;
        }
    }
}
