using Connect4.Mobile.Views;
using Connect4.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using System.Diagnostics;
using Prism.Unity;

namespace Connect4.Mobile
{
    public class App : PrismApplication
    {
        public static float ContentHeight { get; set; }
        public static float ContentWidth { get; set; }

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("MainNavigationPage/Start");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainNavigationPage>();
            Container.RegisterTypeForNavigation<StartPage, StartPageViewModel>("Start");
            Container.RegisterTypeForNavigation<GamePage, GamePageViewModel>("Game");
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}