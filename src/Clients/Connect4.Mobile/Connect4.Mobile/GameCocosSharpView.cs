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
        private double ScreenWidth { get; }
        private double ScreenHeight { get; }
        private CCGameView GameView { get; set; }
        private GameScene GameScene { get; set; }

        public event EventHandler OnCreated;
        public event EventHandler OnTouched;
        public event EventHandler OnReset;
        public event EventHandler OnQuit;

        public GameCocosSharpView()
            : base()
        {
            ScreenWidth = App.ContentWidth;
            ScreenHeight = App.ContentHeight;
            Margin = 0;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            ViewCreated = OnViewCreated;
        }

        // Obsolete constructor
        // Screen size is taken from App.ContentHeight and App.ContentWidth
        //public GameCocosSharpView(double viewWidth, double viewHeight)
        //    : base()
        //{
        //    _screenWidth = viewWidth;
        //    _screenHeight = viewHeight;
        //    Margin = 0;
        //    HorizontalOptions = LayoutOptions.FillAndExpand;
        //    VerticalOptions = LayoutOptions.FillAndExpand;
        //    ViewCreated = OnViewCreated;
        //}

        private void OnViewCreated(object sender, EventArgs ea)
        {
            if (GameView == null)
            {
                GameView = sender as CCGameView;
                if (GameView != null)
                {
                    GameView.DesignResolution = new CCSizeI((int)ScreenWidth, (int)ScreenHeight);

                    var contentSearchPaths = new List<string>() { "Fonts", "Sounds", "Images", "Animations" };
                    GameView.ContentManager.SearchPaths = contentSearchPaths;
                    GameScene = new GameScene(GameView);
                    GameScene.OnTouched += (s, e) => OnTouched?.Invoke(s, e);
                    GameScene.OnReset += (s, e) => OnReset?.Invoke(s, e);
                    GameScene.OnQuit += (s, e) => OnQuit?.Invoke(s, e);
                    GameView.RunWithScene(GameScene);
                }
            }
            OnCreated?.Invoke(sender, ea);
        }

        public void MoveBall(int x, int y)
        {
            GameScene.MoveBall(x, y);
        }
    }
}