using Connect4.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Connect4.Mobile.EventArguments;

namespace Connect4.Mobile.Views
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            StartPageViewModel viewModel = new StartPageViewModel();
            viewModel.OnMoveCompleted += ViewModel_OnMoveCompleted;
            BindingContext = viewModel;
        }

        private void ViewModel_OnMoveCompleted(object sender, OnMoveCompletedEventArgs e)
        {
            MainGrid.AnimateSomething();
        }
    }

    public class TestGrid : Grid
    {
        private StartPageViewModel ViewModel { get; set; }

        public event EventHandler<OnTouchedEventArgs> OnTestTapped;
        public TestGrid()
        {
            var tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.Tapped += Tgr_Tapped;
            this.GestureRecognizers.Add(tgr);
        }

        private void Tgr_Tapped(object sender, EventArgs e)
        {
            OnTouchedEventArgs args = new OnTouchedEventArgs(6);
            OnTestTapped?.Invoke(sender, args);
        }

        public void AnimateSomething()
        {
            this.BackgroundColor = Color.Green;
        }
    }
}
