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

namespace Connect4.Mobile.ViewModels
{
    public class GamePageViewModel : BindableBase, INavigationAware
    {
        private const int dropTimePerRow = 150;
        private Stopwatch stopwatch = new Stopwatch();
        private int maxDropTime = 0;

        private IGameAPI GameAPI { get; set; }
        private INavigationService NavigationService { get; }
        private GameType GameType { get; set; }

        public GamePageViewModel(INavigationService navigationService, IGameAPI api, Dimensions dimensions)
        {
            PreTouchCommand = new DelegateCommand<object>(OnPreTouch, CanPreTouch);
            TouchCommand = new DelegateCommand<object>(OnTouch, CanTouch);
            CreateCommand = new DelegateCommand(OnCreate);
            ResetCommand = new DelegateCommand(OnReset);
            QuitCommand = new DelegateCommand(OnQuit);

            NavigationService = navigationService;
            GameAPI = api;
            GameAPI.OnMoveMade += GameAPI_OnMoveMade;
            Dimensions = dimensions;
        }

        public Dimensions Dimensions { get; set; }

        public event EventHandler<OnMoveCompletedEventArgs> OnMoveCompleted;
        public event EventHandler<OnPreTouchCompletedEventArgs> OnPreTouchCompleted;

        public ICommand CreateCommand { get; }
        public ICommand PreTouchCommand { get; }
        public ICommand TouchCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand QuitCommand { get; }

        private void OnCreate()
        {
            Debug.WriteLine("ViewModel created");
        }

        private bool CanTouch(object touchedColumn)
        {
            // If the time since the beinging of the animation is smaller than max drop time
            bool isNotAnimating = (maxDropTime - (int)stopwatch.ElapsedMilliseconds <= 0);

            // Don't allow to touch when game was finished
            bool isRunningOrNew = GameAPI != null && (GameAPI.GetGameState() == GameState.New || GameAPI.GetGameState() == GameState.Running);

            // Don't allow to touch druing online or bot players
            bool isLocalPlayer = GameAPI != null && GameAPI.GetCurrentPlayer().AllowUserInteraction;

            return isLocalPlayer && isRunningOrNew && isNotAnimating;
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

            // Check if time since last move is shorter than time of last moves animation
            // If it's shorter, that means the animation is still performing and we need to stop here till it's ended
            // We know how long the animation will last, becasue we saved max drop time
            stopwatch.Stop();
            int additionalDelay = maxDropTime - (int)stopwatch.Elapsed.TotalMilliseconds;
            if (additionalDelay > 0) await Task.Delay(additionalDelay);

            // Start the animation (invoked event should be caught in the UI layer)
            OnMoveCompletedEventArgs args = new OnMoveCompletedEventArgs { Player = player, MoveId = 1, Column = column, Row = row };
            OnMoveCompleted?.Invoke(this, args);

            // Save the maximum time of ball drop and restart the stopwatch
            // When the next move is made, check if the time massured is less then max drop time
            // If it is, wait additional time to be sure that drop animations are not overlapping eachother.
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
            GameAPI.Start(GameType);
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

                GameType = type;
                GameAPI.Start(GameType);
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