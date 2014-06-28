using System;
using System.Linq;

namespace Xamarin.Forms.Platform.WPF
{
    public static class Forms
    {
        internal static bool IsInitialized;

        public static void Init()
        {
            if (IsInitialized)
                return;

            // TODO: Use Xamarin.Forms native renderer registration.

            Device.PlatformServices = new PlatformServices();
            RendererFactory.RegisteredRenderers = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                                                   from rend in asm.GetCustomAttributes(typeof(ExportRendererAttribute), true).Cast<ExportRendererAttribute>()
                                                   where typeof(VisualElement).IsAssignableFrom(rend.ViewType) &&
                                                         typeof(IWPFRenderer).IsAssignableFrom(rend.RendererType)
                                                   select (Func<object, Type>)(v =>
                                                    {
                                                        if (!rend.ViewType.IsInstanceOfType(v))
                                                            return null;
                                                        return rend.RendererType;
                                                    })).ToList();
            IsInitialized = true;
        }
    }
}
