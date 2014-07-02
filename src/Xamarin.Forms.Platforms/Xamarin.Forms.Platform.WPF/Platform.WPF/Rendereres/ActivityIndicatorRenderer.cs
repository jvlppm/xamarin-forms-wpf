[assembly: Xamarin.Forms.Platform.WPF.ExportRenderer(typeof(Xamarin.Forms.ActivityIndicator), typeof(Xamarin.Forms.Platform.WPF.Rendereres.ActivityIndicatorRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.WPF;
    using Xamarin.Forms.Platform.WPF.Rendereres;

    public class ActivityIndicatorRenderer : ViewRenderer<ActivityIndicator, System.Windows.Controls.ProgressBar>
    {
        System.Windows.Media.Brush DefaultColor;
        System.Windows.Media.Brush DefaultBackgroundColor;

        public ActivityIndicatorRenderer()
        {
            Content = new System.Windows.Controls.ProgressBar { IsIndeterminate = true };
            DefaultColor = Content.Foreground;
            DefaultBackgroundColor = Content.BorderBrush;
            HandleProperty(ActivityIndicator.IsRunningProperty, Handle_IsRunningProperty);
            HandleProperty(ActivityIndicator.ColorProperty, Handle_ColorProperty);
        }

        protected virtual bool Handle_IsRunningProperty(BindableProperty property)
        {
            Content.Visibility = Model.IsRunning ? System.Windows.Visibility.Visible : System.Windows.Visibility.Hidden;
            return true;
        }

        protected virtual bool Handle_ColorProperty(BindableProperty property)
        {
            Content.Foreground = Model.Color.ToWPFBrush() ?? DefaultColor;
            return true;
        }

        protected override bool Handle_BackgroundColorProperty(BindableProperty property)
        {
            Content.BorderBrush = Model.BackgroundColor.ToWPFBrush() ?? DefaultBackgroundColor;
            return true;
        }
    }
}
