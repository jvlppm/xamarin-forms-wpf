using System;
using System.Windows.Media;

namespace Xamarin.Forms.Platform.WPF
{
    public static class Extensions
    {
        public static void SetPage(this System.Windows.Window window, Xamarin.Forms.Page page)
        {
            window.Content = RendererFactory.Create(page);
        }

        public static SolidColorBrush ToBrush(this Color color)
        {
            if (color == default(Color))
                return null;
            return new SolidColorBrush(color.ToWPFColor());
        }

        public static System.Windows.Media.Color ToWPFColor(this Color color)
        {
            if (color == default(Color))
                return default(System.Windows.Media.Color);

            return System.Windows.Media.Color.FromArgb(
                (byte)(color.A * 255),
                (byte)(color.R * 255),
                (byte)(color.G * 255),
                (byte)(color.B * 255));
        }

        public static System.Windows.TextAlignment ToWPFTextAlignment(this TextAlignment alignment)
        {
            switch(alignment)
            {
                case TextAlignment.Start: return System.Windows.TextAlignment.Left;
                case TextAlignment.Center: return System.Windows.TextAlignment.Center;
                case TextAlignment.End: return System.Windows.TextAlignment.Right;
            }
            throw new NotImplementedException("TextAlignment." + alignment + " is not supported yet.");
        }

        public static System.Windows.HorizontalAlignment ToWPFHorizontalAlignment(this TextAlignment alignment)
        {
            switch (alignment)
            {
                case TextAlignment.Start: return System.Windows.HorizontalAlignment.Left;
                case TextAlignment.Center: return System.Windows.HorizontalAlignment.Center;
                case TextAlignment.End: return System.Windows.HorizontalAlignment.Right;
            }
            throw new NotImplementedException("TextAlignment." + alignment + " is not supported yet.");
        }

        public static System.Windows.VerticalAlignment ToWPFVerticalAlignment(this TextAlignment alignment)
        {
            switch (alignment)
            {
                case TextAlignment.Start: return System.Windows.VerticalAlignment.Top;
                case TextAlignment.Center: return System.Windows.VerticalAlignment.Center;
                case TextAlignment.End: return System.Windows.VerticalAlignment.Bottom;
            }
            throw new NotImplementedException("TextAlignment." + alignment + " is not supported yet.");
        }
    }
}
