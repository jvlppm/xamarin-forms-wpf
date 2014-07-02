using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using PortableForms;

namespace XamlSample.Android
{
    [Activity(Label = "XamlSample.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AndroidActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            Forms.Init(this, bundle);
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetPage(App.GetMainPage());
        }
    }
}

