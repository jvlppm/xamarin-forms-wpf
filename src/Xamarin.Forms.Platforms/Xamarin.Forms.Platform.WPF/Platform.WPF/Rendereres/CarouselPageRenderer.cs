[assembly: Xamarin.Forms.Platform.WPF.ExportRenderer(typeof(Xamarin.Forms.CarouselPage), typeof(Xamarin.Forms.Platform.WPF.Rendereres.CarouselPageRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    using System.Linq;
    using Xamarin.Forms.Platform.WPF.Controls;
    using Xamarin.Forms.Platform.WPF.Converters;

    class CarouselPageRenderer : PageRenderer<CarouselPage, FlipView>
    {
        public CarouselPageRenderer()
        {
            Content = new FlipView();
            HandleProperty(CarouselPage.ItemsSourceProperty, Handle_ItemsSourceProperty);
            HandleProperty(CarouselPage.SelectedItemProperty, Handle_SelectedItemProperty);
        }

        protected virtual bool Handle_ItemsSourceProperty(BindableProperty property)
        {
            Content.ItemsSource = Model.Children;
            return true;
        }

        protected virtual bool Handle_SelectedItemProperty(BindableProperty property)
        {
            Content.SelectedItem = Model.SelectedItem ?? Model.Children.FirstOrDefault();
            return true;
        }
    }
}
