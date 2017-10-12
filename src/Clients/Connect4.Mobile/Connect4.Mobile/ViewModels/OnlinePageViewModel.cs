using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Connect4.Mobile.EventArguments;
using Prism.Navigation;
using Connect4.Domain.Enums;
using Microsoft.AspNet.SignalR.Client;
using Connect4.API;
using Connect4.Domain.EventArguments;
using Connect4.Mobile.Enums;
using Connect4.Mobile.Utilities;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Entities.Players;
using Connect4.Mobile.Communication;
using System.Threading;
using Xamarin.Forms;

namespace Connect4.Mobile.ViewModels
{
    public class OnlinePageViewModel : BindableBase, INavigationAware
    {
        public OnlinePageViewModel(INavigationService navigationService, Dimensions dimensions, IProxy proxy)
        {
            CancelCommand = new DelegateCommand(OnCancel);

            NavigationService = navigationService;
            Dimensions = dimensions;
            Proxy = proxy;
            Proxy.GameStarted += Proxy_GameStarted;
        }

        private void Proxy_GameStarted(object sender, OnlineGameStartedArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                NavigationParameters param = new NavigationParameters();
                param.Add("Type", GameType.Online);
                param.Add("Proxy", Proxy);
                NavigationService.NavigateAsync("Game", param);
            });
        }

        private INavigationService NavigationService { get; }
        public ICommand CancelCommand { get; }
        public Dimensions Dimensions { get; }
        public IProxy Proxy { get; }

        private void OnCancel()
        {
            NavigationService.NavigateAsync("Start", null);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            Proxy.CancelRequest();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Proxy.GameRequest();
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }
    }
}
