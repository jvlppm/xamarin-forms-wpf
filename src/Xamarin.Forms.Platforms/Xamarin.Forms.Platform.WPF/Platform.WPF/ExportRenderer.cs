using System;

namespace Xamarin.Forms.Platform.WPF
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true)]
    public class ExportRendererAttribute : HandlerAttribute, IRegisterable
    {
        public Type ViewType;
        public Type RendererType;

        public ExportRendererAttribute(Type viewType, Type rendererType)
            : base(viewType, rendererType)
        {
            ViewType = viewType;
            RendererType = rendererType;
        }
    }
}
