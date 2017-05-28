﻿using Connect4.Mobile.ViewModels;
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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((GamePageViewModel)BindingContext).OnPreTouchCompleted += GamePage_OnPreTouchCompleted;
            ((GamePageViewModel)BindingContext).OnMoveCompleted += GamePage_OnMoveCompleted;
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