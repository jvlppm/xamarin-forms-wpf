using System;

namespace Xamarin.Forms.Platform.WPF
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple=true)]
    public class ExportRendererAttribute : Attribute
    {
        public Type ViewType;
        public Type RendererType;

        public ExportRendererAttribute(Type viewType, Type rendererType)
        {
            ViewType = viewType;
            RendererType = rendererType;
        }
    }
}
