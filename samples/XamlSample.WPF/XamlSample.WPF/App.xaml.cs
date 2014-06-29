using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Xamarin.Forms.Platform.WPF;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Forms.Init();
            var mainWin = new Window { Width = 640, Height = 480 };
            mainWin.SetPage(PortableForms.App.GetMainPage());
            mainWin.Title = "Xamarin.Forms.WPF";
            mainWin.Show();
        }
    }
}
