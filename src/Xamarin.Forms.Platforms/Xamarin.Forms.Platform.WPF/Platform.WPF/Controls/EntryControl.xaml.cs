using System.Windows;
using System.Windows.Controls;

namespace Xamarin.Forms.Platform.WPF.Controls
{
    public partial class EntryControl : UserControl
    {
        bool ignoreTextChange;

        #region Dependency Properties
        public Entry Model
        {
            get { return (Entry)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(Entry), typeof(EntryControl), new PropertyMetadata(null, ModelChangedCallback));
        #endregion

        public EntryControl()
        {
            InitializeComponent();
        }

        static void ModelChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EntryControl entryControl = (EntryControl)sender;

            if(e.OldValue != null)
                ((Entry)e.OldValue).TextChanged -= entryControl.EntryModel_TextChanged;

            if (e.NewValue != null)
                ((Entry)e.NewValue).TextChanged += entryControl.EntryModel_TextChanged;
        }

        void EntryModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ignoreTextChange)
                return;

            ignoreTextChange = true;
            TextBox.Text = e.NewTextValue;
            PasswordBox.Password = e.NewTextValue;
            ignoreTextChange = false;
        }

        void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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
    }
}
