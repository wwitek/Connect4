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

            var grid = new Grid();
            grid.Padding = 0;
            grid.Margin = 0;
            grid.SizeChanged += Grid_SizeChanged;
            
            var gameView = new CocosSharpView()
            {
                Margin = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
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
                var contentSearchPaths = new List<string>() { "Fonts", "Sounds", "Images", "Animations" };
                gameView.ContentManager.SearchPaths = contentSearchPaths;
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
            CCColor4B startColor = new CCColor4B(1, 36, 76);
            CCColor4B endColor = new CCColor4B(4, 79, 162);
            CCColor4B circleBorder = new CCColor4B(2, 23, 50);
            CCColor4B circleColor = new CCColor4B(178, 216, 255);

            var layer = new CCLayerGradient(startColor, endColor, new CCPoint(0f, 1f));
            AddLayer(layer);

            double viewWidth = gameView.DesignResolution.Width;
            double viewHeight = gameView.DesignResolution.Height;

            double gap = Math.Ceiling(viewWidth / 7 * 0.05);
            double circleSize = Math.Floor((viewWidth - (gap * 10)) / 7);
            if (((circleSize % 2) == 0 && (viewWidth % 2) != 0) || ((circleSize % 2) != 0 && (viewWidth % 2) == 0))
            {
                circleSize--;
            }
            double outerGap = (viewWidth - (gap * 10) - (circleSize * 7)) / 2;

            float yTestX = 300;
            float yTestY = 300;
            float rTestX = 300;
            float rTestY = 300;

            var circle = new CCDrawNode();
            double y = (viewHeight / 2) + (5 * gap) + (3 * circleSize);
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

                    if (i == 1 && j == 1)
                    {
                        yTestX = ((float)x + (float)circleSize / 2);
                        yTestY = ((float)y + (float)circleSize / 2);
                    }
                    if (i == 5 && j == 2)
                    {
                        rTestX = ((float)x + (float)circleSize / 2);
                        rTestY = ((float)y + (float)circleSize / 2);
                    }

                    circle.DrawSolidCircle(
                        new CCPoint((float)x + (float)circleSize / 2, (float)y + (float)circleSize / 2),
                        radius: (float)circleSize / 2,
                        color: circleColor);
                    layer.AddChild(circle);

                    circle.DrawEllipse(
                        rect: new CCRect((float)x, (float)y, (float)circleSize, (float)circleSize),
                        lineWidth: 1,
                        color: circleBorder);
                    layer.AddChild(circle);
                }
            }

            var ball = new CCSprite("redball");
            ball.ContentSize = new CCSize((float)circleSize, (float)circleSize);
            ball.Position = new CCPoint(rTestX, rTestY);
            layer.AddChild(ball);

            var ball2 = new CCSprite("yellowball");
            ball2.ContentSize = new CCSize((float)circleSize, (float)circleSize);
            ball2.Position = new CCPoint(yTestX, yTestY);
            layer.AddChild(ball2);
        }
    }
}
