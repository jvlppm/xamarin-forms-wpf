using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using Xamarin.Forms.Platform.WPF.Rendereres;

[assembly: ExportRenderer(typeof(View), typeof(ViewRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    public class ViewRenderer : VisualElementRenderer<View, object>
    {

    }

    public class ViewRenderer<TModel, T> : VisualElementRenderer<TModel, T>
        where TModel : View
    {
        public ViewRenderer()
        {
            // TODO: Style based renderer, bindings and converters.

            HandleProperty(View.HorizontalOptionsProperty, Handle_HorizontalOptionsProperty);
            HandleProperty(View.VerticalOptionsProperty, Handle_VerticalOptionsProperty);
            HandleProperty(View.BackgroundColorProperty, Handle_BackgroundColorProperty);
            HandleProperty(View.IsEnabledProperty, Handle_IsEnabledProperty);
            HandleProperty(View.IsVisibleProperty, Handle_IsVisibleProperty);
        }

        protected virtual bool Handle_VerticalOptionsProperty(BindableProperty property)
        {
            // TODO: Understand LayoutOptions.Expands / Fill.

            if (Model.VerticalOptions.Expands || Model.VerticalOptions.Alignment == LayoutAlignment.Fill)
                Element.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            else
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
                }
            }

            return true;
        }

        protected virtual bool Handle_HorizontalOptionsProperty(BindableProperty property)
        {
            if (Model.HorizontalOptions.Expands || Model.HorizontalOptions.Alignment == LayoutAlignment.Fill)
                Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            else
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
                        Element.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        break;
                }
            }

            return true;
        }

        protected virtual bool Handle_BackgroundColorProperty(BindableProperty property)
        {
            Background = Model.BackgroundColor.ToBrush();
            return true;
        }

        protected virtual bool Handle_IsEnabledProperty(BindableProperty property)
        {
            IsEnabled = Model.IsEnabled;
            return true;
        }

        protected virtual bool Handle_IsVisibleProperty(BindableProperty property)
        {
            Visibility = Model.IsVisible ? System.Windows.Visibility.Visible
                                         : System.Windows.Visibility.Hidden;
            return true;
        }
    }
}
