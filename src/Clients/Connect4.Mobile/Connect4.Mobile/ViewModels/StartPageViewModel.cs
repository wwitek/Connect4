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

namespace Connect4.Mobile.ViewModels
{
    public class StartPageViewModel : BindableBase
    {
        public StartPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            StartOnePlayerGameCommand = new DelegateCommand(StartOnePlayerGame);
        }

        public ICommand StartOnePlayerGameCommand { get; }
        public ICommand StartTwoPlayersGameCommand { get; }
        public ICommand StartOnlineGameCommand { get; }

        private INavigationService NavigationService { get; }

        public void StartOnePlayerGame()
        {
            NavigationService.NavigateAsync("Game");
        }
    }
}