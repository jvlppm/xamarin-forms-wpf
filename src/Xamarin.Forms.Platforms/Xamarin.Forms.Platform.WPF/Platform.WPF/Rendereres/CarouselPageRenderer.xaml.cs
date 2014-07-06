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
        public new CarouselPage Model
        {
            get { return (CarouselPage)base.Model; }
            set { base.Model = value; }
        }

        public CarouselPageRenderer()
        {
            InitializeComponent();
            HandleProperty(CarouselPage.ItemsSourceProperty, Handle_ItemsSourceProperty);
            HandleProperty(CarouselPage.SelectedItemProperty, Handle_SelectedItemProperty);
        }

        protected virtual bool Handle_ItemsSourceProperty(BindableProperty property)
        {
            FlipView.ItemsSource = Model.Children;
            return true;
        }

        protected virtual bool Handle_SelectedItemProperty(BindableProperty property)
        {
            FlipView.SelectedItem = Model.SelectedItem ?? Model.Children.FirstOrDefault();
            return true;
        }
    }
}
