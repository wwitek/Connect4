using Connect4.Mobile.ViewModels;
using System.ComponentModel;
using Connect4.Mobile.EventArguments;
using Xamarin.Forms;

namespace Connect4.Mobile.Views
{
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((GamePageViewModel)BindingContext).OnMoveCompleted += GamePage_OnMoveCompleted;
        }

        private void GamePage_OnMoveCompleted(object sender, OnMoveCompletedEventArgs e)
        {
            GameGrid.MoveBall(e.Player, e.Column, e.Row);
        }
    }
}