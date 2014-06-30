using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.WPF;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(Xamarin.Forms.Platform.WPF.Rendereres.EntryRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    class EntryRenderer : ViewRenderer<Entry, Controls.EntryControl>
    {
        public EntryRenderer()
        {
            Content = new Controls.EntryControl();
        }

        protected override void LoadModel(Entry model)
        {
            base.LoadModel(model);
            Content.Model = model;
        }

        protected override bool Handle_BackgroundColorProperty(BindableProperty property)
        {
            return true;
        }
    }
}
