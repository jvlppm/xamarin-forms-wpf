[assembly: Xamarin.Forms.Platform.WPF.ExportRenderer(typeof(Xamarin.Forms.Page), typeof(Xamarin.Forms.Platform.WPF.Rendereres.PageRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.WPF;
    using Xamarin.Forms.Platform.WPF.Rendereres;

    public class PageRenderer : PageRenderer<Page, System.Windows.Controls.Control> { }

    public class PageRenderer<TModel, TView> : VisualElementRenderer<TModel, TView>
        where TModel : Page
        where TView : System.Windows.Controls.Control
    {
        public PageRenderer()
        {
            HandleProperty(Page.PaddingProperty, Handle_PaddingProperty);
        }

        bool Handle_PaddingProperty(BindableProperty property)
        {
            Padding = Model.Padding.ToWPFThickness();
            return true;
        }
    }
}
