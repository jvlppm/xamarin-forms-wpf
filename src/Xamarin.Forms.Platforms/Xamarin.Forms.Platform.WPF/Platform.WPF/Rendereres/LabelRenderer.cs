using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;
using Xamarin.Forms.Platform.WPF.Rendereres;

[assembly: ExportRenderer(typeof(Label), typeof(LabelRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    public class LabelRenderer : ViewRenderer<Label, System.Windows.Controls.TextBlock>
    {
        System.Windows.Media.FontFamily DefaultFontFamily;
        double DefaultFontSize;

        public LabelRenderer()
        {
            Content = new System.Windows.Controls.TextBlock();
            DefaultFontFamily = Content.FontFamily;
            DefaultFontSize = Content.FontSize;
            HandleProperty(Label.TextProperty, Handle_TextProperty);
            HandleProperty(Label.FontProperty, Handle_FontProperty);
            HandleProperty(Label.TextColorProperty, Handle_ColorProperty);
            HandleProperty(Label.BackgroundColorProperty, Handle_ColorProperty);
            HandleProperty(Label.XAlignProperty, Handle_Alignment);
            HandleProperty(Label.YAlignProperty, Handle_Alignment);
            HandleProperty(Label.LineBreakModeProperty, Handle_LineBreakMode);
        }

        protected virtual bool Handle_TextProperty(BindableProperty property)
        {
            Content.Text = Model.Text;
            return true;
        }

        protected virtual bool Handle_FontProperty(BindableProperty property)
        {
            Content.FontFamily = Model.Font.FontFamily == null ? DefaultFontFamily : new System.Windows.Media.FontFamily(Model.Font.FontFamily);
            var fontSize = Model.Font.GetWPFSize();
            Content.FontSize = fontSize > 0 ? fontSize : DefaultFontSize;
            Content.FontWeight = Model.Font.FontAttributes == FontAttributes.Bold ? System.Windows.FontWeights.Bold : System.Windows.FontWeights.Normal;
            return true;
        }

        protected virtual bool Handle_ColorProperty(BindableProperty property)
        {
            if (property == Label.TextColorProperty && Model.TextColor != default(Color))
                Content.Foreground = Model.TextColor.ToWPFBrush();
            if (property == Label.BackgroundColorProperty && Model.BackgroundColor != default(Color))
                Content.Background = Model.BackgroundColor.ToWPFBrush();
            return true;
        }

        protected virtual bool Handle_Alignment(BindableProperty property)
        {
            if (property == Label.XAlignProperty)
                Content.TextAlignment = Model.XAlign.ToWPFTextAlignment();
            if (property == Label.YAlignProperty)
                Content.VerticalAlignment = Model.YAlign.ToWPFVerticalAlignment();
            return true;
        }

        protected virtual bool Handle_LineBreakMode(BindableProperty property)
        {
            switch (Model.LineBreakMode)
            {
                case LineBreakMode.NoWrap:
                    Content.TextWrapping = System.Windows.TextWrapping.NoWrap;
                    Content.TextTrimming = System.Windows.TextTrimming.None;
                    break;
                case LineBreakMode.WordWrap:
                    Content.TextWrapping = System.Windows.TextWrapping.WrapWithOverflow;
                    Content.TextTrimming = System.Windows.TextTrimming.None;
                    break;
                case LineBreakMode.CharacterWrap:
                    Content.TextWrapping = System.Windows.TextWrapping.Wrap;
                    Content.TextTrimming = System.Windows.TextTrimming.None;
                    break;
                case LineBreakMode.HeadTruncation: // TODO: LineBreakMode.HeadTruncation
                case LineBreakMode.MiddleTruncation: // TODO: LineBreakMode.MiddleTruncation
                case LineBreakMode.TailTruncation:
                    Content.TextWrapping = System.Windows.TextWrapping.NoWrap;
                    Content.TextTrimming = System.Windows.TextTrimming.CharacterEllipsis;
                    break;
            }
            return true;
        }
    }
}
