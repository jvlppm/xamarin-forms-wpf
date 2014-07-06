[assembly: Xamarin.Forms.Platform.WPF.ExportRenderer(typeof(Xamarin.Forms.VisualElement), typeof(Xamarin.Forms.Platform.WPF.Rendereres.VisualElementRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.WPF;

    public class VisualElementRenderer : VisualElementRenderer<VisualElement, System.Windows.FrameworkElement> { }

    public class VisualElementRenderer<TModel, TView> : ElementRenderer<TModel, TView>
        where TModel : VisualElement
    {
        #region Constructors
        public VisualElementRenderer()
        {
            HandleProperty(VisualElement.BackgroundColorProperty, Handle_BackgroundColorProperty);
            HandleProperty(VisualElement.IsEnabledProperty, Handle_IsEnabledProperty);
            HandleProperty(VisualElement.IsVisibleProperty, Handle_IsVisibleProperty);
            HandleProperty(VisualElement.InputTransparentProperty, Handle_InputTransparentProperty);
            HandleProperty(VisualElement.OpacityProperty, Handle_OpacityProperty);
        }
        #endregion

        #region Property Handlers
        protected virtual bool Handle_BackgroundColorProperty(BindableProperty property)
        {
            Background = Model.BackgroundColor.ToWPFBrush();
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

        protected virtual bool Handle_InputTransparentProperty(BindableProperty property)
        {
            IsHitTestVisible = !Model.InputTransparent;
            return true;
        }

        protected virtual bool Handle_OpacityProperty(BindableProperty property)
        {
            Opacity = Model.Opacity;
            return true;
        }
        #endregion
    }
}
