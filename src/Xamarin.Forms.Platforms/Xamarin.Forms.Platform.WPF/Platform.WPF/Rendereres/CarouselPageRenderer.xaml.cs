[assembly: Xamarin.Forms.Platform.WPF.ExportRenderer(typeof(Xamarin.Forms.CarouselPage), typeof(Xamarin.Forms.Platform.WPF.Rendereres.CarouselPageRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    using System.Linq;
    using Xamarin.Forms.Platform.WPF.Controls;
    using Xamarin.Forms.Platform.WPF.Converters;

    /// <summary>
    /// Interaction logic for CarouselPageRenderer.xaml
    /// </summary>
    public partial class CarouselPageRenderer : PageRenderer
    {
        public CarouselPageRenderer()
        {
            InitializeComponent();
        }
    }
}
