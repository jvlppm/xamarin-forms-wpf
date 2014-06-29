using FormsGallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PortableForms
{
    public class App
    {
        // TODO: Page to select demos

        public static Page GetMainPage()
        {
            return new StackLayoutDemoPage();
            //return new MainPage { MainText = "Hello, Xamarin.Forms !" };
        }
    }
}
