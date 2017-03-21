using Connect4.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Connect4.Mobile.Views
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            BindingContext = new StartPageViewModel();
        }
    }

    public class TestGrid : Grid
    {
        public event EventHandler OnTestTapped;
        public TestGrid()
        {
            var tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.Tapped += Tgr_Tapped;
            this.GestureRecognizers.Add(tgr);
        }
        private void Tgr_Tapped(object sender, EventArgs e)
        {
            OnTestTapped?.Invoke(sender, e);
        }
    }
}
