using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using Xamarin.Forms.Platform.WPF.Rendereres;

[assembly: ExportRenderer(typeof(Page), typeof(PageRenderer<Page>))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    public class PageRenderer<TModel> : VisualElementRenderer<TModel, System.Windows.Controls.UserControl>
        where TModel : Page
    {
        public PageRenderer()
        {
            Content = new System.Windows.Controls.UserControl();
            HandleProperty(Page.PaddingProperty, Handle_PaddingProperty);
        }

        bool Handle_PaddingProperty(BindableProperty property)
        {
            Padding = Model.Padding.ToWPFThickness();
            return true;
        }
    }
}
