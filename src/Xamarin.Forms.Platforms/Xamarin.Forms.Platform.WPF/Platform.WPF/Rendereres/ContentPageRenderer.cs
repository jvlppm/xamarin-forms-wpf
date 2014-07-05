[assembly: Xamarin.Forms.Platform.WPF.ExportRenderer(typeof(Xamarin.Forms.ContentPage), typeof(Xamarin.Forms.Platform.WPF.Rendereres.ContentPageRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    using System.Windows.Controls;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.WPF;
    using Xamarin.Forms.Platform.WPF.Rendereres;

    public class ContentPageRenderer : PageRenderer<ContentPage, UserControl>
    {
        public ContentPageRenderer()
        {
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
            Content.Content = null;
        }

        void model_ChildAdded(object sender, ElementEventArgs e)
        {
            Content.Content = RendererFactory.Create(e.Element).Element;
        }
    }
}
