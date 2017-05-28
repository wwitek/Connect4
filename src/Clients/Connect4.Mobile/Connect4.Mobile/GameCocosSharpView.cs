using CocosSharp;
using Connect4.Mobile.Enums;
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

        public event EventHandler OnCreated;
        public event EventHandler OnPreTouched;
        public event EventHandler OnTouched;
        public event EventHandler OnReset;
        public event EventHandler OnQuit;

        private double ScreenWidth { get; }
        private double ScreenHeight { get; }
        private CCGameView GameView { get; set; }
        private GameScene GameScene { get; set; }

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
                    GameScene.OnPreTouched += (s, e) => OnPreTouched?.Invoke(s, e);
                    GameScene.OnTouched += (s, e) => OnTouched?.Invoke(s, e);
                    GameScene.OnReset += (s, e) => OnReset?.Invoke(s, e);
                    GameScene.OnQuit += (s, e) => OnQuit?.Invoke(s, e);
                    GameView.RunWithScene(GameScene);
                }
            }
            OnCreated?.Invoke(sender, ea);
        }

        public void PreTouch(PlayerColor player, int column)
        {
            GameScene.PreTouch(player, column);
        }

        public void MoveBall(PlayerColor player, int x, int y)
        {
            GameScene.MoveBall(player, x, y);
        }

        public void SetScore(PlayerColor player, int score)
        {
            GameScene.SetScore(player, score);
        }
    }
}