using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using Xamarin.Forms.Platform.WPF.Controls;
using Xamarin.Forms.Platform.WPF.Rendereres;

[assembly: ExportRenderer(typeof(StackLayout), typeof(StackLayoutRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    class StackLayoutRenderer : ViewRenderer<StackLayout, StackLayoutControl>
    {
        
        public StackLayoutRenderer()
        {
            Content = new StackLayoutControl();
            HandleProperty(StackLayout.OrientationProperty, Handle_OrientationProperty);
        }

        protected override void LoadModel(StackLayout model)
        {
            Content.ItemsSource = model.Children;
            base.LoadModel(model);
        }

        protected override void UnloadModel(StackLayout model)
        {
            Content.ItemsSource = null;
            base.UnloadModel(model);
        }

        bool Handle_OrientationProperty(BindableProperty property)
        {
            Content.SetOrientation(Model.Orientation.ToWPFOrientation());
            return true;
        }
    }
}
