using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Connect4.Mobile
{
    public class GameCocosSharpView : CocosSharpView
    {
        private GameScene _gameScene;
        private double _screenWidth;
        private double _screenHeight;

        public event EventHandler OnCreated;
        public event EventHandler OnTouched;
        public CCGameView gameView;

        public GameCocosSharpView()
            : base()
        {
            _screenWidth = App.ContentWidth;
            _screenHeight = App.ContentHeight;
            Margin = 0;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            ViewCreated = OnViewCreated;
        }

        public GameCocosSharpView(double viewWidth, double viewHeight)
            : base()
        {
            _screenWidth = viewWidth;
            _screenHeight = viewHeight;
            Margin = 0;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            ViewCreated = OnViewCreated;
        }

        private void OnViewCreated(object sender, EventArgs ea)
        {
            if (gameView == null)
            {
                gameView = sender as CCGameView;
                if (gameView != null)
                {
                    gameView.DesignResolution = new CCSizeI((int)_screenWidth, (int)_screenHeight);

                    var contentSearchPaths = new List<string>() { "Fonts", "Sounds", "Images", "Animations" };
                    gameView.ContentManager.SearchPaths = contentSearchPaths;

                    _gameScene = new GameScene(gameView);
                    _gameScene.OnTouched += (s, e) =>
                    {
                        OnTouched?.Invoke(s, e);
                    };
                    gameView.RunWithScene(_gameScene);
                }
            }
            OnCreated?.Invoke(sender, ea);
        }

        public void MoveBall(int x, int y)
        {
            _gameScene.MoveBall(x, y);
        }
    }
}