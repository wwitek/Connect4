using CocosSharp;
using System;

namespace Connect4.Mobile
{
    public class GameLayer : CCLayerGradient
    {
        private readonly float _viewWidth;
        private readonly float _viewHeight;
        private readonly float _circleSize;
        private readonly float _circleGap;
        private readonly float _edgeGap;
        private readonly float _ballRadius;
        private readonly CCPoint[,] _boardCoordinates;
        private readonly CCNode _drawNodeRoot;

        public GameLayer(float viewWidth, float viewHeight)
            : base(C4Colors.StartColor, C4Colors.EndColor, new CCPoint(0f, 1f))
        {
            _boardCoordinates = new CCPoint[7, 6];
            _viewWidth = viewWidth;
            _viewHeight = viewHeight;
            _circleGap = (float)Math.Ceiling(_viewWidth / 7 * 0.1);
            _circleSize = (float)Math.Floor((_viewWidth - (_circleGap * 10)) / 7);
            if (((_circleSize % 2) == 0 && (_viewWidth % 2) != 0) || ((_circleSize % 2) != 0 && (_viewWidth % 2) == 0)) _circleSize--;
            _edgeGap = (_viewWidth - (_circleGap * 10) - (_circleSize * 7)) / 2;
            _ballRadius = (_circleSize / 2) - 1;
            _drawNodeRoot = new CCNode();
            AddChild(_drawNodeRoot);

            InitializeBoardCoordinates();
            InitializeBoard(_drawNodeRoot);

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = (touches, ccevent) =>
            {
                if (touches.Count > 0)
                {
                    var touch = touches[0];


                    DrawBall(6, 5);
                }
                
            };
            AddEventListener(touchListener, this);
        }

        private void InitializeBoardCoordinates()
        {
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
                }
            }
        }

        private void InitializeBoard(CCNode drawNodeRoot)
        {
            for (int i = 0; i < _boardCoordinates.GetLength(0); i++)
            {
                for (int j = 0; j < _boardCoordinates.GetLength(1); j++)
                {
                    float x = _boardCoordinates[i, j].X;
                    float y = _boardCoordinates[i, j].Y;

                    CCDrawNode circle = new CCDrawNode();
                    circle.DrawSolidCircle(
                        new CCPoint(x, y),
                        radius: _circleSize / 2,
                        color: C4Colors.CircleLighterColor);
                    drawNodeRoot.AddChild(circle);

                    CCDrawNode ellipse = new CCDrawNode();
                    ellipse.DrawEllipse(
                        rect: new CCRect(x - (_circleSize / 2), y - (_circleSize / 2), _circleSize, _circleSize),
                        lineWidth: 1,
                        color: C4Colors.CircleBorderLight);
                    drawNodeRoot.AddChild(ellipse);
                }
            }
        }

        private void DrawBall(int x, int y)
        {
            var ball = new CCDrawNode();
            ball.DrawSolidCircle(
                _boardCoordinates[x, y],
                radius: _ballRadius,
                color: C4Colors.YellowColor);
            AddChild(ball);
        }

    }
}
