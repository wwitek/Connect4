using Connect4.Mobile.ViewModels;
using System.ComponentModel;
using Connect4.Mobile.EventArguments;
using Xamarin.Forms;
using System;

namespace Connect4.Mobile.Views
{
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
            BoardGrid.SizeChanged += OnGridSizeChanged;
        }

        private void OnGridSizeChanged(object sender, EventArgs e)
        {
            double _screenWidth = ((Grid)sender).Width;
            double _screenHeight = ((Grid)sender).Height;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((GamePageViewModel)BindingContext).PreTouchCompleted += GamePage_OnPreTouchCompleted;
            ((GamePageViewModel)BindingContext).MoveCompleted += GamePage_OnMoveCompleted;
            ((GamePageViewModel)BindingContext).Restarted += GamePage_Restarted;
        }

        private void GamePage_Restarted(object sender, EventArgs e)
        {
            GameView.Restart();
        }

        private void GamePage_OnPreTouchCompleted(object sender, OnPreTouchCompletedEventArgs e)
        {
            GameView.PreTouch(e.Player, e.Column);
        }

        private void GamePage_OnMoveCompleted(object sender, OnMoveCompletedEventArgs e)
        {
            GameView.MoveBall(e.Player, e.Column, e.Row);
        }
    }
}