using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile
{
    public class GameScene : CCScene
    {
        private float _viewWidth;
        private float _viewHeight;
        private CCPoint[,] _boardCoordinates;
        private float _circleSize;
        private float _circleGap;
        private float _edgeGap;

        public GameScene(CCGameView gameView) : base(gameView)
        {
            CCColor4B startColor = new CCColor4B(1, 36, 76);
            CCColor4B endColor = new CCColor4B(4, 79, 162);
            CCColor4B circleBorder = new CCColor4B(2, 23, 50);
            CCColor4B circleColor = new CCColor4B(178, 216, 255);

            _boardCoordinates = new CCPoint[7, 6];
            _viewWidth = gameView.DesignResolution.Width;
            _viewHeight = gameView.DesignResolution.Height;
            _circleGap = (float)Math.Ceiling(_viewWidth / 7 * 0.05);
            _circleSize = (float)Math.Floor((_viewWidth - (_circleGap * 10)) / 7);
            if (((_circleSize % 2) == 0 && (_viewWidth % 2) != 0) || ((_circleSize % 2) != 0 && (_viewWidth % 2) == 0))
            {
                _circleSize--;
            }
            _edgeGap = (_viewWidth - (_circleGap * 10) - (_circleSize * 7)) / 2;

            var layer = new CCLayerGradient(startColor, endColor, new CCPoint(0f, 1f));
            AddLayer(layer);

            var circle = new CCDrawNode();
            float y = (_viewHeight / 2) - (4 * _circleGap) - (4 * _circleSize);
            for (int j = 0; j < 6; j++)
            {
                y += (_circleSize + _circleGap);
                for (int i = 0; i < 7; i++)
                {
                    float x = _edgeGap + (2 * _circleGap);
                    if (i > 0)
                    {
                        x += i * (_circleSize + _circleGap);
                    }
                    var coordinatesX = x + _circleSize / 2;
                    var coordinatesY = y + _circleSize / 2;
                    _boardCoordinates[i, j] = new CCPoint(coordinatesX, coordinatesY);

                    circle.DrawSolidCircle(
                        new CCPoint(x + _circleSize / 2, y + _circleSize / 2),
                        radius: _circleSize / 2,
                        color: circleColor);
                    layer.AddChild(circle);

                    circle.DrawEllipse(
                        rect: new CCRect(x, y, _circleSize, _circleSize),
                        lineWidth: 1,
                        color: circleBorder);
                    layer.AddChild(circle);
                }
            }

            var ball = new CCSprite("redball");
            ball.ContentSize = new CCSize(_circleSize, _circleSize);
            ball.Position = _boardCoordinates[2, 3];
            layer.AddChild(ball);

            var ball2 = new CCSprite("yellowball");
            ball2.ContentSize = new CCSize(_circleSize, _circleSize);
            ball2.Position = _boardCoordinates[6, 1];
            layer.AddChild(ball2);
        }
    }
}
