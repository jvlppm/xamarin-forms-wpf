[assembly: Xamarin.Forms.Platform.WPF.ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(Xamarin.Forms.Platform.WPF.Controls.EntryRenderer))]
namespace Xamarin.Forms.Platform.WPF.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    public partial class EntryRenderer : Xamarin.Forms.Platform.WPF.Rendereres.ViewRenderer
    {
        bool ignoreTextChange;

        public new Entry Model
        { 
            get { return (Entry)base.Model; }
            set { base.Model = value; }
        }

        public EntryRenderer()
        {
            InitializeComponent();
        }

        protected override void LoadModel(View model)
        {
            ((Entry)model).TextChanged += EntryModel_TextChanged;
            base.LoadModel(model);
        }

        protected override void UnloadModel(View model)
        {
            ((Entry)model).TextChanged -= EntryModel_TextChanged;
            base.UnloadModel(model);
        }

        void EntryModel_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (ignoreTextChange)
                return;

            ignoreTextChange = true;
            TextBox.Text = e.NewTextValue;
            PasswordBox.Password = e.NewTextValue;
            ignoreTextChange = false;
        }

        void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ignoreTextChange)
                return;

            ignoreTextChange = true;
            Model.Text = TextBox.Text;
            PasswordBox.Password = TextBox.Text;
            ignoreTextChange = false;
        }

        void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (ignoreTextChange)
                return;

            ignoreTextChange = true;
            Model.Text = PasswordBox.Password;
            TextBox.Text = PasswordBox.Password;
            ignoreTextChange = false;
        }

        private void Entry_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Model != null)
                Model.SendCompleted();
        }

        private void Entry_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Model != null && e.Key == System.Windows.Input.Key.Return)
            {
                Model.SendCompleted();
                e.Handled = true;
            }
        }

        protected override bool Handle_BackgroundColorProperty(BindableProperty property)
        {
            // Background color is set directly to the TextBox/PasswordBox with bindings.
            return true;
        }
    }
}
