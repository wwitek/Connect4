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

namespace Connect4.Mobile.ViewModels
{
    public class GamePageViewModel : BindableBase
    {
        public GamePageViewModel(INavigationService navigationService)
        {
            TouchCommand = new DelegateCommand<object>(OnTouch, CanTouch);
            CreateCommand = new DelegateCommand(OnCreate);
            ResetCommand = new DelegateCommand(OnReset);
            QuitCommand = new DelegateCommand(OnQuit);

            NavigationService = navigationService;
        }

        public event EventHandler<OnMoveCompletedEventArgs> OnMoveCompleted;

        public ICommand CreateCommand { get; }
        public ICommand TouchCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand QuitCommand { get; }

        private INavigationService NavigationService { get; } 

        private void OnCreate()
        {
            Debug.WriteLine("ViewModel created");
        }

        private void OnTouch(object touchedColumn)
        {
            int column = 0;
            if (int.TryParse(touchedColumn.ToString(), out column))
            {
                OnMoveCompletedEventArgs args = new OnMoveCompletedEventArgs { Player = Enums.PlayerColor.Red, MoveId = 1, Column = column, Row = 0 };
                OnMoveCompleted?.Invoke(this, args);
            }
        }

        private bool CanTouch(object touchedColumn)
        {
            return true;
        }

        private void OnReset()
        {
            Debug.WriteLine("Game reset!");
        }

        private void OnQuit()
        {
            NavigationService.GoBackAsync();
        }
    }
}