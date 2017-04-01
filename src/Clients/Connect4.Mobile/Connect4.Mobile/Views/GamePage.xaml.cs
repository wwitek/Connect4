using CocosSharp;
using Connect4.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Mobile.EventArguments;
using Xamarin.Forms;

namespace Connect4.Mobile.Views
{
    public partial class GamePage : ContentPage
    {
        public GamePage(INotifyPropertyChanged viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            ((GamePageViewModel)viewModel).OnMoveCompleted += GamePage_OnMoveCompleted;
        }

        private void GamePage_OnMoveCompleted(object sender, OnMoveCompletedEventArgs e)
        {
            GameView.MoveBall(e.Column, e.Row);
        }
    }
}