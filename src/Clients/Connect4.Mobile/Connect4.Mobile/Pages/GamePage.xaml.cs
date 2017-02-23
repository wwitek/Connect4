using CocosSharp;
using System;
using System.Collections.Generic;
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

            var grid = new Grid();
            grid.Padding = 0;
            grid.Margin = 0;
            grid.SizeChanged += Grid_SizeChanged;
            
            var gameView = new CocosSharpView()
            {
                Margin = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromRgb(245, 245, 245),
                ViewCreated = HandleViewCreated
            };

            grid.Children.Add(gameView);
            Content = grid;
        }

        private void Grid_SizeChanged(object sender, EventArgs e)
        {
            _screenWidth = ((Grid)sender).Width;
            _screenHeight = ((Grid)sender).Height;
        }

        private void HandleViewCreated(object sender, EventArgs e)
        {
            var gameView = sender as CCGameView;
            if (gameView != null)
            {
                gameView.DesignResolution = new CCSizeI((int)_screenWidth, (int)_screenHeight);

                _gameScene = new GameScene(gameView);
                gameView.RunWithScene(_gameScene);
            }
        }
    }

    public class GameScene : CCScene
    {
        public GameScene(CCGameView gameView) : base(gameView)
        {
            var layer = new CCLayer();
            AddLayer(layer);

            double viewWidth = gameView.DesignResolution.Width;
            double viewHeight = gameView.DesignResolution.Height;

            var shape = new CCRect(0, 0, (int)viewWidth, (int)viewHeight);
            CCDrawNode background = new CCDrawNode();
            background.DrawRect(shape, fillColor: CCColor4B.LightGray);
            layer.AddChild(background);

            double gap = Math.Ceiling(viewWidth / 7 * 0.05);
            double circleSize = Math.Floor((viewWidth - (gap * 10)) / 7);
            if (((circleSize % 2) == 0 && (viewWidth % 2) != 0) || ((circleSize % 2) != 0 && (viewWidth % 2) == 0))
            {
                circleSize--;
            }
            double outerGap = (viewWidth - (gap * 10) - (circleSize * 7)) / 2;
            
            var circle = new CCDrawNode();
            double y = (viewHeight / 2) + (7 * gap) + (4 * circleSize);
            for (int j = 0; j < 6; j++)
            {
                y -= (circleSize + (2 * gap));
                for (int i = 0; i < 7; i++)
                {
                    double x = outerGap + (2 * gap);
                    if (i > 0)
                    {
                        x += i * (circleSize + gap);
                    }
                    circle.DrawSolidCircle(
                        new CCPoint((float)x + (float)circleSize / 2, (float)y + (float)circleSize / 2),
                        radius: (float)circleSize / 2,
                        color: CCColor4B.White);
                    layer.AddChild(circle);

                    circle.DrawEllipse(
                        rect: new CCRect((float)x, (float)y, (float)circleSize, (float)circleSize),
                        lineWidth: 1,
                        color: CCColor4B.Gray);
                    layer.AddChild(circle);
                }
            }
        }
    }
}
