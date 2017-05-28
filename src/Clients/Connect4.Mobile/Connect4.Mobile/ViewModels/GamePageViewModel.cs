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
        private const int dropTimePerRow = 150;
        private Stopwatch stopwatch = new Stopwatch();
        private int maxDropTime = 0;

        private IGameAPI GameAPI { get; set; }

        public GamePageViewModel(INavigationService navigationService, IGameAPI api)
        {
            PreTouchCommand = new DelegateCommand<object>(OnPreTouch, CanPreTouch);
            TouchCommand = new DelegateCommand<object>(OnTouch, CanTouch);
            CreateCommand = new DelegateCommand(OnCreate);
            ResetCommand = new DelegateCommand(OnReset);
            QuitCommand = new DelegateCommand(OnQuit);

            NavigationService = navigationService;
            GameAPI = api;
            GameAPI.OnMoveMade += GameAPI_OnMoveMade;
        }

        public event EventHandler<OnMoveCompletedEventArgs> OnMoveCompleted;
        public event EventHandler<OnPreTouchCompletedEventArgs> OnPreTouchCompleted;

        public ICommand CreateCommand { get; }
        public ICommand PreTouchCommand { get; }
        public ICommand TouchCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand QuitCommand { get; }

        private INavigationService NavigationService { get; }

        private void OnCreate()
        {
            Debug.WriteLine("ViewModel created");
        }

        private bool CanTouch(object touchedColumn)
        {
            return ((GameAPI != null && (GameAPI.GetGameState() == GameState.New || GameAPI.GetGameState() == GameState.Running))
                && GameAPI.GetCurrentPlayer().AllowUserInteraction);
        }

        private void OnTouch(object touchedColumn)
        {
            int column = 0;
            if (int.TryParse(touchedColumn.ToString(), out column))
            {
                GameAPI.TryMove(column);
            }
        }

        private async void GameAPI_OnMoveMade(object sender, MoveEventArgs e)
        {
            PlayerColor player = (e.Move.PlayerId == 1) ? PlayerColor.Red : PlayerColor.Yellow;
            int column = e.Move.Column;
            int row = e.Move.Row;

            stopwatch.Stop();
            int additionalDelay = maxDropTime - (int)stopwatch.Elapsed.TotalMilliseconds;
            if (additionalDelay > 0) await Task.Delay(additionalDelay);

            OnMoveCompletedEventArgs args = new OnMoveCompletedEventArgs { Player = player, MoveId = 1, Column = column, Row = row };
            OnMoveCompleted?.Invoke(this, args);

            maxDropTime = (row + 1) * dropTimePerRow;
            stopwatch.Restart();
        }

        private bool CanPreTouch(object touchedColumn)
        {
            return CanTouch(touchedColumn);
        }

        private void OnPreTouch(object touchedColumn)
        {
            PlayerColor player = (GameAPI.GetCurrentPlayer().Id == 1) ? PlayerColor.Red : PlayerColor.Yellow;
            OnPreTouchCompletedEventArgs args = new OnPreTouchCompletedEventArgs { Player = player, Column = (int)touchedColumn };
            OnPreTouchCompleted?.Invoke(this, args);
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
                var type = (GameType)parameters["Type"];
                Debug.WriteLine("OnNavigatedFrom - " + type.ToString());
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
                var type = (GameType)parameters["Type"];
                Debug.WriteLine("OnNavigatedTo - " + type.ToString());

                GameAPI.Start(type);
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
                var type = (GameType)parameters["Type"];
                Debug.WriteLine("OnNavigatingTo - " + type.ToString());
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception: " + ex);
                NavigationService.GoBackAsync();
            }
        }
    }
}