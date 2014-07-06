namespace Xamarin.Forms.Platform.WPF
{
    using System;

    public static class RendererFactory
    {
        internal static IWPFRenderer Create(Element element)
        {
            if (!Forms.IsInitialized)
                throw new InvalidOperationException("Xamarin.Forms not initialized");

            if (element == null)
                throw new NotImplementedException();

            var renderer = Registrar.Registered.GetHandler<IWPFRenderer>(element.GetType());
            if (renderer != null)
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
