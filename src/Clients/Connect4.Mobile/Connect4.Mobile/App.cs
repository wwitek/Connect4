using Connect4.Mobile.Pages;
using Connect4.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Connect4.Mobile
{
    public class App : Application
    {
        public static float ContentHeight { get; set; }
        public static float ContentWidth { get; set; }

        public App()
        {
            GamePageViewModel viewModel = new GamePageViewModel();
            MainPage = new GamePage(viewModel);
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
