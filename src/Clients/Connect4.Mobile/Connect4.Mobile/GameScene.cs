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
        private float _ballRadius;
        private CCLayer _layer;

        public GameScene(CCGameView gameView) : base(gameView)
        {
            _boardCoordinates = new CCPoint[7, 6];
            _viewWidth = gameView.DesignResolution.Width;
            _viewHeight = gameView.DesignResolution.Height;
            _circleGap = (float)Math.Ceiling(_viewWidth / 7 * 0.1);
            _circleSize = (float)Math.Floor((_viewWidth - (_circleGap * 10)) / 7);
            if (((_circleSize % 2) == 0 && (_viewWidth % 2) != 0) || ((_circleSize % 2) != 0 && (_viewWidth % 2) == 0)) _circleSize--;
            _edgeGap = (_viewWidth - (_circleGap * 10) - (_circleSize * 7)) / 2;
            _ballRadius = (_circleSize / 2) - 1;

            _layer = new CCLayerGradient(C4Colors.StartColor, C4Colors.EndColor, new CCPoint(0f, 1f));
            AddLayer(_layer);
            InitializeBoard(_layer);

            var ball = new CCDrawNode();
            ball.DrawSolidCircle(
                _boardCoordinates[1, 1],
                radius: _ballRadius,
                color: C4Colors.YellowColor);
            _layer.AddChild(ball);

            ball.DrawSolidCircle(
                _boardCoordinates[3, 2],
                radius: _ballRadius,
                color: C4Colors.RedColor);
            _layer.AddChild(ball);
        }

        public void InitializeBoard(CCLayer layer)
        {
            var node = new CCDrawNode();
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

                    node.DrawSolidCircle(
                        new CCPoint(x + _circleSize / 2, y + _circleSize / 2),
                        radius: _circleSize / 2,
                        color: C4Colors.CircleLighterColor);
                    layer.AddChild(node);

                    node.DrawEllipse(
                        rect: new CCRect(x + 1, y + 1, _circleSize - 2, _circleSize - 2),
                        lineWidth: 1,
                        color: C4Colors.CircleDarkerColor);
                    layer.AddChild(node);

                    node.DrawEllipse(
                        rect: new CCRect(x, y, _circleSize, _circleSize),
                        lineWidth: 1,
                        color: C4Colors.CircleBorderLight);
                    layer.AddChild(node);
                }
            }
        }
    }
}
