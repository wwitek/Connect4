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
        public StartPage(StartPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
