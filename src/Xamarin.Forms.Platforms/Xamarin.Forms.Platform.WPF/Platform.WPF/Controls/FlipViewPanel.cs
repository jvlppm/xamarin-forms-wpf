namespace Xamarin.Forms.Platform.WPF.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class FlipViewPanel : Panel
    {
        protected override Size MeasureOverride(System.Windows.Size availableSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                if (child == null) { continue; }
                child.Measure(availableSize);
            }

            return availableSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                double top = Canvas.GetTop(child);
                double left = Canvas.GetLeft(child);

                left = Double.IsNaN(left) ? 0.0 : left;
                top = Double.IsNaN(top) ? 0.0 : top;

                child.Arrange(new Rect(left, top, finalSize.Width, finalSize.Height));
            }
            return finalSize;
        }
    }
}
