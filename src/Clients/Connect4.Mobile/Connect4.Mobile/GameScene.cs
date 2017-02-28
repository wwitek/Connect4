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

            CCColor4B circleDarkerBorder = new CCColor4B(2, 24, 50);
            CCColor4B circleLighterBorder = new CCColor4B(1, 36, 76);

            CCColor4B circleDarkerColor = new CCColor4B(104, 172, 250);
            CCColor4B circleLighterColor = new CCColor4B(178, 216, 255);


            CCColor4B yellowColor = new CCColor4B(255, 211, 0);
            CCColor4B royalYellowColor = new CCColor4B(250, 218, 94);
            CCColor4B goldYellowColor = new CCColor4B(255, 215, 0);

            CCColor4B redColor = new CCColor4B(196, 2, 51);
            CCColor4B scarletRedColor = new CCColor4B(255, 36, 0);
            CCColor4B fireEngineRed = new CCColor4B(206, 32, 41);


            _boardCoordinates = new CCPoint[7, 6];
            _viewWidth = gameView.DesignResolution.Width;
            _viewHeight = gameView.DesignResolution.Height;
            _circleGap = (float)Math.Ceiling(_viewWidth / 7 * 0.1);
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
                        color: circleLighterColor);
                    layer.AddChild(circle);

                    circle.DrawEllipse(
                        rect: new CCRect(x + 1, y + 1, _circleSize - 2, _circleSize - 2),
                        lineWidth: 1,
                        color: circleDarkerColor);
                    layer.AddChild(circle);

                    circle.DrawEllipse(
                        rect: new CCRect(x, y, _circleSize, _circleSize),
                        lineWidth: 1,
                        color: circleLighterBorder);
                    layer.AddChild(circle);
                }
            }

            //var ball = new CCSprite("redball");
            //ball.ContentSize = new CCSize(_circleSize, _circleSize);
            //ball.Position = _boardCoordinates[2, 3];
            //layer.AddChild(ball);
            
            //var ball2 = new CCSprite("yellowball");
            //ball2.ContentSize = new CCSize(_circleSize, _circleSize);
            //ball2.Position = _boardCoordinates[6, 1];
            //layer.AddChild(ball2);

            float ballRadius = (_circleSize / 2) - 1;

            circle.DrawSolidCircle(
                _boardCoordinates[1, 1],
                radius: ballRadius,
                color: yellowColor);
            layer.AddChild(circle);

            circle.DrawSolidCircle(
                _boardCoordinates[1, 2],
                radius: ballRadius,
                color: royalYellowColor);
            layer.AddChild(circle);

            circle.DrawSolidCircle(
                _boardCoordinates[1, 4],
                radius: ballRadius,
                color: goldYellowColor);
            layer.AddChild(circle);

            circle.DrawSolidCircle(
                _boardCoordinates[3, 2],
                radius: ballRadius,
                color: redColor);
            layer.AddChild(circle);

            circle.DrawSolidCircle(
                _boardCoordinates[3, 3],
                radius: ballRadius,
                color: scarletRedColor);
            layer.AddChild(circle);

            circle.DrawSolidCircle(
                _boardCoordinates[3, 5],
                radius: ballRadius,
                color: fireEngineRed);
            layer.AddChild(circle);
        }
    }
}
