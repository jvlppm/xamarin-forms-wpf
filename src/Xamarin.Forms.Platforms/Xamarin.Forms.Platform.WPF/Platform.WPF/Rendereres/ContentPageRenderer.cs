using System.Windows.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using Xamarin.Forms.Platform.WPF.Rendereres;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    public class ContentPageRenderer : VisualElementRenderer<ContentPage, UserControl>
    {
        public ContentPageRenderer()
        {
            // Xamarin.Forms: it would be very usefull if ContentPage.View was a bindable property.
            Content = new UserControl();
        }

        protected override void LoadModel(ContentPage model)
        {
            base.LoadModel(model);
            model.ChildAdded += model_ChildAdded;
            model.ChildRemoved += model_ChildRemoved;

            if (model.Content != null)
                Content.Content = RendererFactory.Create(model.Content).Element;
        }

        protected override void UnloadModel(ContentPage model)
        {
            base.UnloadModel(model);
            model.ChildAdded -= model_ChildAdded;
            model.ChildRemoved -= model_ChildRemoved;
        }

        void model_ChildRemoved(object sender, ElementEventArgs e)
        {
        }

        void model_ChildAdded(object sender, ElementEventArgs e)
        {
        }

        protected override void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);
        }
    }
}
