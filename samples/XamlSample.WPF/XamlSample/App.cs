using FormsGallery;
using Xamarin.Forms;

namespace PortableForms
{
    public class App
    {
        // TODO: Page to select demos

        public static Page GetMainPage()
        {
            return new CarouselPage
            {
                Children =
                {
                    new MainPage { MainText = "Hello, Xamarin.Forms !" },
                    new StackLayoutDemoPage()
                }
            };
        }
    }
}
