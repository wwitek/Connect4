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

namespace Connect4.Mobile.ViewModels
{
    public class GamePageViewModel : BindableBase, INavigationAware
    {
        private IGameAPI GameAPI { get; set; }

        public GamePageViewModel(INavigationService navigationService, IGameAPI api)
        {
            TouchCommand = new DelegateCommand<object>(OnTouch, CanTouch);
            CreateCommand = new DelegateCommand(OnCreate);
            ResetCommand = new DelegateCommand(OnReset);
            QuitCommand = new DelegateCommand(OnQuit);

            NavigationService = navigationService;
            GameAPI = api;
            GameAPI.OnMoveMade += GameAPI_OnMoveMade;
        }

        public event EventHandler<OnMoveCompletedEventArgs> OnMoveCompleted;

        public ICommand CreateCommand { get; }
        public ICommand TouchCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand QuitCommand { get; }

        private INavigationService NavigationService { get; }
        private GameType Type { get; set; }

        private void OnCreate()
        {
            Debug.WriteLine("ViewModel created");
        }

        private void OnTouch(object touchedColumn)
        {
            int column = 0;
            if (int.TryParse(touchedColumn.ToString(), out column))
            {
                GameAPI.TryMove(column);
            }
        }

        private void GameAPI_OnMoveMade(object sender, MoveEventArgs e)
        {
            PlayerColor player = (e.Move.PlayerId == 1) ? PlayerColor.Red : PlayerColor.Yellow;
            int column = e.Move.Column;
            int row = e.Move.Row;

            OnMoveCompletedEventArgs args = new OnMoveCompletedEventArgs { Player = player, MoveId = 1, Column = column, Row = row };
            OnMoveCompleted?.Invoke(this, args);
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            try
            {
                Type = (GameType)parameters["Type"];
                Debug.WriteLine("OnNavigatedFrom - " + Type.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex);
                NavigationService.GoBackAsync();
            }
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {
                Type = (GameType)parameters["Type"];
                Debug.WriteLine("OnNavigatedTo - " + Type.ToString());

                GameAPI.Start(Type);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex);
                NavigationService.GoBackAsync();
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            try
            {
                Type = (GameType)parameters["Type"];
                Debug.WriteLine("OnNavigatingTo - " + Type.ToString());
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception: " + ex);
                NavigationService.GoBackAsync();
            }
        }
    }
}