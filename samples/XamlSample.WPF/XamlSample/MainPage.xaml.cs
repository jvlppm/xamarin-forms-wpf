using Xamarin.Forms;

namespace PortableForms
{
    public partial class MainPage
    {
        public string MainText
        {
            get { return (string)GetValue(MainTextProperty); }
            set { SetValue(MainTextProperty, value); }
        }

        // Using a BindableProperty as the backing store for MainText.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty MainTextProperty =
            BindableProperty.Create("MainText", typeof(string), typeof(MainPage), null);

        public MainPage()
        {
            // Xamarin.Forms: MainPage + IsVisible="False" = InvalidCastException (MainPage to View)
            //IsVisible = false;

            InitializeComponent();
            BindingContext = this;
        }
    }
}
