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
using Xamarin.Forms;

namespace Connect4.Mobile.ViewModels
{
    public class GamePageViewModel : BindableBase, INavigationAware
    {
        private const int dropTimePerRow = 150;
        private Stopwatch stopwatch = new Stopwatch();
        private int maxDropTime = 0;

        private int _redScore;
        private int _yellowScore;
        private string _status;

        private IGameAPI GameAPI { get; set; }
        private INavigationService NavigationService { get; }
        private GameType GameType { get; set; }
        private bool CanRestartHelpFlag { get; set; }

        public GamePageViewModel(INavigationService navigationService, IGameAPI api, Dimensions dimensions, Colors colors)
        {
            PreTouchCommand = new DelegateCommand<object>(OnPreTouch, CanPreTouch);
            TouchCommand = new DelegateCommand<object>(OnTouch, CanTouch);
            CreateCommand = new DelegateCommand(OnCreate);
            RestartCommand = new DelegateCommand<object>(OnRestart, CanRestart);
            QuitCommand = new DelegateCommand(OnQuit);

            NavigationService = navigationService;
            GameAPI = api;
            GameAPI.MoveMade += GameAPI_OnMoveMade;
            Dimensions = dimensions;
            Colors = colors;
        }

        public Dimensions Dimensions { get; }
        public Colors Colors { get; }
        public int RedScore
        {
            get { return _redScore; }
            set { SetProperty(ref _redScore, value); }
        }
        public int YellowScore
        {
            get { return _yellowScore; }
            set { SetProperty(ref _yellowScore, value); }
        }
        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        public event EventHandler<OnMoveCompletedEventArgs> MoveCompleted;
        public event EventHandler<OnPreTouchCompletedEventArgs> PreTouchCompleted;
        public event EventHandler Restarted;

        public ICommand CreateCommand { get; }
        public ICommand PreTouchCommand { get; }
        public ICommand TouchCommand { get; }
        public ICommand RestartCommand { get; }
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
            bool isLocalPlayer = GameAPI != null && GameAPI.GetCurrentPlayer() != null && GameAPI.GetCurrentPlayer().AllowUserInteraction;

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

            // Restart button can be enabled only during a humand (local player) turn
            CanRestartHelpFlag = (GameAPI.GetCurrentPlayer().AllowUserInteraction) ? false : true;
            if (GameType == GameType.TwoPlayers) CanRestartHelpFlag = true;
            RaiseCanExecuteChangedOnRestart();

            // Check if time since last move is shorter than time of last moves animation
            // If it's shorter, that means the animation is still performing and we need to stop here till it's ended
            // We know how long the animation will last, becasue we saved max drop time
            stopwatch.Stop();
            int additionalDelay = maxDropTime - (int)stopwatch.Elapsed.TotalMilliseconds;
            if (additionalDelay > 0) await Task.Delay(additionalDelay);

            // Start the animation (invoked event should be caught in the UI layer)
            OnMoveCompletedEventArgs args = new OnMoveCompletedEventArgs { Player = player, MoveId = 1, Column = column, Row = row };
            MoveCompleted?.Invoke(this, args);

            // Save the maximum time of ball drop and restart the stopwatch
            // When the next move is made, check if the time massured is less then max drop time
            // If it is, wait additional time to be sure that drop animations are not overlapping eachother.
            maxDropTime = (row + 1) * dropTimePerRow;
            stopwatch.Restart();

            if (e.Move.IsDraw) Status = "It's a draw!";
            else if (e.Move.IsWinner)
            {
                Status = $"Player {e.Move.PlayerId} won!";
                if (e.Move.PlayerId == 1) RedScore++;
                if (e.Move.PlayerId == 2) YellowScore++;
            }
        }

        private bool CanPreTouch(object touchedColumn)
        {
            return CanTouch(touchedColumn);
        }

        private void OnPreTouch(object touchedColumn)
        {
            PlayerColor player = (GameAPI.GetCurrentPlayer().Id == 1) ? PlayerColor.Red : PlayerColor.Yellow;
            OnPreTouchCompletedEventArgs args = new OnPreTouchCompletedEventArgs { Player = player, Column = (int)touchedColumn };
            PreTouchCompleted?.Invoke(this, args);
        }

        private void RaiseCanExecuteChangedOnRestart()
        {
            DelegateCommand<object> restartCommand = RestartCommand as DelegateCommand<object>;
            restartCommand.RaiseCanExecuteChanged();
        }

        private bool CanRestart(object arg)
        {
            // Don't allow to restart druing online or bot players
            return CanRestartHelpFlag;
        }

        private void OnRestart(object arg)
        {
            Debug.WriteLine("Game reset!");

            Status = "";
            GameAPI.Start(GameType);
            Restarted?.Invoke(this, null);
        }

        private void OnQuit()
        {
            NavigateToStart();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {
                var type = (GameType)parameters["Type"];
                GameType = type;

                IProxy proxy = null;
                if (GameType.Equals(GameType.Online))
                {
                    proxy = (IProxy)parameters["Proxy"];
                }

                GameAPI.Start(GameType, proxy);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex);
                NavigateToStart();
            }
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        public void NavigateToStart()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                NavigationService.NavigateAsync("Start", null);
            });
        }
    }
}