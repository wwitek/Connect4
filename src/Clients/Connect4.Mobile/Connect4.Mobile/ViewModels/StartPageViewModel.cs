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
        INavigationService _navigationService { get; }

        public ICommand StartOnePlayerGameCommand { get; }
        public ICommand StartTwoPlayersGameCommand { get; }
        public ICommand StartOnlineGameCommand { get; }

        public StartPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            StartOnePlayerGameCommand = new DelegateCommand(StartOnePlayerGame);
        }

        public void StartOnePlayerGame()
        {
            _navigationService.NavigateAsync("Game");
        }
    }
}