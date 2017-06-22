using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Connect4.Mobile.EventArguments;
using Prism.Navigation;
using Prism.Commands;
using Connect4.Domain.Enums;
using XFGloss;
using Connect4.Mobile.Utilities;

namespace Connect4.Mobile.ViewModels
{
    public class StartPageViewModel : BindableBase
    {
        public StartPageViewModel(INavigationService navigationService, Dimensions dimensions, Colors colors)
        {
            StartOnePlayerGameCommand = new DelegateCommand(StartOnePlayerGame);
            StartTwoPlayersGameCommand = new DelegateCommand(StartTwoPlayersGame);
            StartOnlineGameCommand = new DelegateCommand(StartOnlineGame);

            NavigationService = navigationService;
            Dimensions = dimensions;
        }

        public Dimensions Dimensions { get; }
        public ICommand StartOnePlayerGameCommand { get; }
        public ICommand StartTwoPlayersGameCommand { get; }
        public ICommand StartOnlineGameCommand { get; }

        private INavigationService NavigationService { get; }

        public void StartOnePlayerGame()
        {
            NavigationParameters param = new NavigationParameters();
            param.Add("Type", GameType.SinglePlayer);
            NavigationService.NavigateAsync("Game", param);
        }

        public void StartTwoPlayersGame()
        {
            NavigationParameters param = new NavigationParameters();
            param.Add("Type", GameType.TwoPlayers);
            NavigationService.NavigateAsync("Game", param);
        }

        public void StartOnlineGame()
        {
            NavigationParameters param = new NavigationParameters();
            param.Add("Type", GameType.Online);
            NavigationService.NavigateAsync("Online", param);
        }
    }
}