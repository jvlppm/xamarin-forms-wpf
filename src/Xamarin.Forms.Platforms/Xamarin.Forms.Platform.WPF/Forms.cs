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

            Device.PlatformServices = new PlatformServices();
            RendererFactory.ScanForRenderers();
            IsInitialized = true;
        }
    }
}
