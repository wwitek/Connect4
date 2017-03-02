using CocosSharp;
using System;

namespace Connect4.Mobile
{
    public class GameLayer : CCLayerGradient
    {
        private float _viewWidth;
        private float _viewHeight;
        private float _circleSize;
        private float _circleGap;
        private float _edgeGap;
        private float _ballRadius;
        private CCPoint[,] _boardCoordinates;

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

            InitializeBoard();

            DrawBall(1, 3);
            DrawBall(0, 0);

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = (touches, ccevent) =>
            {
                DrawBall(6, 5);
            };
            AddEventListener(touchListener, this);
        }

        public void InitializeBoard()
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
                    this.AddChild(node);

                    //node.DrawEllipse(
                    //    rect: new CCRect(x + 1, y + 1, _circleSize - 2, _circleSize - 2),
                    //    lineWidth: 1,
                    //    color: C4Colors.CircleDarkerColor);
                    //this.AddChild(node);

                    node.DrawEllipse(
                        rect: new CCRect(x, y, _circleSize, _circleSize),
                        lineWidth: 1,
                        color: C4Colors.CircleBorderLight);
                    this.AddChild(node);
                }
            }
        }

        public void DrawBall(int x, int y)
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
