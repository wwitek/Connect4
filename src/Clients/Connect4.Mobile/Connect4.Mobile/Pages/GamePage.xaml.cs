using CocosSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Connect4.Mobile.Pages
{
    public partial class GamePage : ContentPage
    {
        public GamePage(INotifyPropertyChanged viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
