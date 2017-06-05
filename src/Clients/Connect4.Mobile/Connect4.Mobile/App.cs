using Connect4.Mobile.Views;
using Connect4.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Diagnostics;
using Prism.Unity;
using Microsoft.Practices.Unity;
using Microsoft.AspNet.SignalR.Client;
using Connect4.API;
using Connect4.Domain.Factories;
using Connect4.Domain.Interfaces.Factories;
using Connect4.Domain.AI;
using Connect4.Mobile.Utilities;

namespace Connect4.Mobile
{
    public class App : PrismApplication
    {
        public static float ContentHeight { get; set; }
        public static float ContentWidth { get; set; }
        public static Dimensions Dimensions { get; private set; }

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("MainNavigationPage/Start");
        }

        protected override void RegisterTypes()
        {
            Dimensions dimensions = new Dimensions(ContentWidth, ContentHeight, 7, 6);
            Container.RegisterInstance(dimensions);
            Dimensions = dimensions;

            Container.RegisterType<IFieldFactory, FieldFactory>();
            Container.RegisterType<IBoardFactory, BoardFactory>();
            Container.RegisterType<IGameFactory, GameFactory>();
            Container.RegisterType<IBoardEvaluation, BasicBoardEvaluation>();
            Container.RegisterType<IPlayerFactory, PlayerFactory>();
            Container.RegisterType<IGameAPI, GameAPI>();

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