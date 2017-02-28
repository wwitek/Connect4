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
        private GameScene _gameScene;
        private double _screenWidth;
        private double _screenHeight;

        public GamePage()
        {
            InitializeComponent();

            var grid = new Grid() { Padding = 0, Margin = 0 };
            grid.SizeChanged += OnGridSizeChanged;

            var gameView = new CocosSharpView()
            {
                Margin = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ViewCreated = OnViewCreated
            };

            grid.Children.Add(gameView);
            Content = grid;
        }

        private void OnGridSizeChanged(object sender, EventArgs e)
        {
            _screenWidth = ((Grid)sender).Width;
            _screenHeight = ((Grid)sender).Height;
        }

        private void OnViewCreated(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView != null)
            {
                gameView.DesignResolution = new CCSizeI((int)_screenWidth, (int)_screenHeight);

                var contentSearchPaths = new List<string>() { "Fonts", "Sounds", "Images", "Animations" };
                gameView.ContentManager.SearchPaths = contentSearchPaths;

                _gameScene = new GameScene(gameView);
                gameView.RunWithScene(_gameScene);
            }
        }
    }
}
