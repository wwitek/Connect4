using CocosSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Connect4.Mobile.Pages
{
    public partial class GamePage : ContentPage
    {
        private Grid grid;
        private GameCocosSharpView gameView;

        public GamePage()
        {
            InitializeComponent();
            grid = new Grid() { Padding = 0, Margin = 0 };
            grid.SizeChanged += OnGridSizeChanged;
            Content = grid;
        }

        private void OnGridSizeChanged(object sender, EventArgs e)
        {
            double _screenWidth = ((Grid)sender).Width;
            double _screenHeight = ((Grid)sender).Height;

            if (gameView == null)
            {
                gameView = new GameCocosSharpView(_screenWidth, _screenHeight);
                grid.Children.Add(gameView);
            }
        }
    }
}
