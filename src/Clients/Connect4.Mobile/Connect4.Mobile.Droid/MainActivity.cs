using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.Practices.Unity;
using Prism.Unity;

namespace Connect4.Mobile.Droid
{
    [Activity(Label = "Connect4.Mobile", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            float statusBarHeight = 0;
            var resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0) statusBarHeight = Resources.GetDimensionPixelSize(resourceId);

            var metrics = Resources.DisplayMetrics;
            float width = metrics.WidthPixels / metrics.Density;
            float height = metrics.HeightPixels / metrics.Density;
            statusBarHeight = statusBarHeight / metrics.Density;

            App.ContentWidth = width;
            App.ContentHeight = (height - statusBarHeight);
            LoadApplication(new App());

            // IMPORTANT: Initialize XFGloss AFTER calling LoadApplication on the Android platform
            XFGloss.Droid.Library.Init(this, bundle);
        }

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IUnityContainer container)
            {
            }
        }
    }
}

