using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms.Platform.WPF.Rendereres;

namespace Xamarin.Forms.Platform.WPF
{
    public static class RendererFactory
    {
        internal static IWPFRenderer Create(Element element)
        {
            if (!Forms.IsInitialized)
                throw new InvalidOperationException("Xamarin.Forms not initialized");

            if (element == null)
                throw new NotImplementedException();

            var renderer = Registrar.Registered.GetHandler<IWPFRenderer>(element.GetType());
            if (renderer == null)
                throw new NotImplementedException();
            renderer.Model = element;
            return renderer;
        }

        internal static void ScanForRenderers()
        {
            Registrar.RegisterAll(new []{
                typeof(ExportRendererAttribute)
            });
        }
    }
}
