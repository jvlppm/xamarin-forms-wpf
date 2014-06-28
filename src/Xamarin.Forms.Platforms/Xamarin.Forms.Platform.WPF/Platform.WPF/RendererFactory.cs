using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms.Platform.WPF.Rendereres;

namespace Xamarin.Forms.Platform.WPF
{
    public static class RendererFactory
    {
        internal static List<Func<object, Type>> RegisteredRenderers;

        internal static IWPFRenderer Create(Element element)
        {
            if (!Forms.IsInitialized)
                throw new InvalidOperationException("Xamarin.Forms not initialized");

            if (element == null)
                throw new NotImplementedException();

            Type rendererType = RegisteredRenderers.Select(f => f(element)).Where(r => r != null).LastOrDefault();
            if (rendererType == null)
                throw new NotImplementedException();

            IWPFRenderer renderer = Activator.CreateInstance(rendererType) as IWPFRenderer;
            if (renderer == null)
                throw new NotImplementedException();
            renderer.Model = element;
            return renderer;
        }
    }
}
