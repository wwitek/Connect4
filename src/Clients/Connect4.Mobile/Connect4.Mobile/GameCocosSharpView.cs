using CocosSharp;
using Connect4.Mobile.Enums;
using Connect4.Mobile.Utilities;
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
            Margin = 0;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            VerticalOptions = LayoutOptions.FillAndExpand;
            ViewCreated = OnViewCreated;
        }

        public event EventHandler Created;
        public event EventHandler PreTouched;
        public event EventHandler Touched;

        private CCGameView GameView { get; set; }
        private GameScene GameScene { get; set; }

        public Dimensions Dimensions
        {
            get
            {
                return (Dimensions)GetValue(DimensionsProperty);
            }
            set
            {
                try
                {
                    SetValue(DimensionsProperty, value);
                }
                catch (ArgumentException ex)
                {
                    // We need to do something here to let the user know
                    // the value passed in failed databinding validation
                    SetValue(DimensionsProperty, null);
                }
            }
        }
        public static readonly BindableProperty DimensionsProperty =
           BindableProperty.Create(nameof(Dimensions), typeof(Dimensions), typeof(GameCocosSharpView), null);

        public Colors Colors
        {
            get
            {
                return (Colors)GetValue(ColorsProperty);
            }
            set
            {
                try
                {
                    SetValue(ColorsProperty, value);
                }
                catch (ArgumentException ex)
                {
                    // We need to do something here to let the user know
                    // the value passed in failed databinding validation
                    SetValue(ColorsProperty, null);
                }
            }
        }
        public static readonly BindableProperty ColorsProperty =
           BindableProperty.Create(nameof(Colors), typeof(Colors), typeof(GameCocosSharpView), null);

        private void OnViewCreated(object sender, EventArgs ea)
        {
            if (GameView == null)
            {
                GameView = sender as CCGameView;
                if (GameView != null)
                {
                    double screenWidth = Dimensions.BoardWidth;
                    double screenHeight = Dimensions.BoardHeight;
                    GameView.DesignResolution = new CCSizeI((int)screenWidth, (int)screenHeight);

                    var contentSearchPaths = new List<string>() { "Fonts", "Sounds", "Images", "Animations" };
                    GameView.ContentManager.SearchPaths = contentSearchPaths;
                    GameScene = new GameScene(GameView, Dimensions, Colors);
                    GameScene.PreTouched += (s, e) => PreTouched?.Invoke(s, e);
                    GameScene.Touched += (s, e) => Touched?.Invoke(s, e);
                    GameView.RunWithScene(GameScene);
                }
            }
            Created?.Invoke(sender, ea);
        }

        public void PreTouch(PlayerColor player, int column)
        {
            GameScene.PreTouch(player, column);
        }

        public void MoveBall(PlayerColor player, int x, int y)
        {
            GameScene.MoveBall(player, x, y);
        }

        public void Restart()
        {
            GameScene.Restart();
        }
    }
}