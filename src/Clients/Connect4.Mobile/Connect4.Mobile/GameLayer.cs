using CocosSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Connect4.Mobile.Utilities;
using Connect4.Mobile.EventArguments;

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
        private readonly CCNode _drawBoardRoot;
        private readonly CCNode _drawPreDropRoot;

        public event EventHandler OnTouched;

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
            _drawBoardRoot = new CCNode();
            _drawPreDropRoot = new CCNode();
            AddChild(_drawBoardRoot);
            AddChild(_drawPreDropRoot);

            InitializeBoardCoordinates();
            InitializeBoard();

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = (touches, ccevent) =>
            {
                if (touches.Count > 0)
                {
                    var targetColumn = GetColumnByTouch(touches[0]);
                    OnTouchedEventArgs tea = new OnTouchedEventArgs(targetColumn);
                    OnTouched?.Invoke(this, tea);

                    _drawPreDropRoot.RemoveAllChildren();
                }
            };
            AddEventListener(touchListener, this);
            
            touchListener.OnTouchesBegan = OnPreTouch;
            touchListener.OnTouchesMoved = OnPreTouch;
        }

        private void OnPreTouch(List<CCTouch> touches, CCEvent ccevent)
        {
            if (touches.Count > 0)
            {
                var targetColumn = GetColumnByTouch(touches[0]);
                CCDrawNode ball = DrawBall(targetColumn);

                _drawPreDropRoot.RemoveAllChildren();
                _drawPreDropRoot.AddChild(ball);
            }
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

        private void InitializeBoard()
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
                    _drawBoardRoot.AddChild(circle);

                    CCDrawNode ellipse = new CCDrawNode();
                    ellipse.DrawEllipse(
                        rect: new CCRect(x - (_circleSize / 2), y - (_circleSize / 2), _circleSize, _circleSize),
                        lineWidth: 1,
                        color: C4Colors.CircleBorderLight);
                    _drawBoardRoot.AddChild(ellipse);
                }
            }

            float scoreYPosition = _viewHeight - 10;
            float topBarHeight = scoreYPosition - (_boardCoordinates[0, 5].Y + (float)(_circleSize * 1.5) + _circleGap) - 10;
            float nameSize = 16;
            float scoreSize = topBarHeight - nameSize;
            float nameYPosition = scoreYPosition - scoreSize;
            float leftXPosition = _boardCoordinates[0, 0].X - (_boardCoordinates[0, 0].X / 2) - _edgeGap;
            float rightXPosition = _boardCoordinates[6, 0].X + (_circleSize / 2);

            CCLabel score1Label = new CCLabel("0", "ArialBlack", scoreSize, CCLabelFormat.SystemFont);
            score1Label.Color = new CCColor3B(C4Colors.YellowColor);
            score1Label.HorizontalAlignment = CCTextAlignment.Left;
            score1Label.AnchorPoint = CCPoint.AnchorUpperLeft;
            score1Label.Position = new CCPoint(leftXPosition, scoreYPosition);
            _drawBoardRoot.AddChild(score1Label);

            CCLabel player1Label = new CCLabel("PLAYER1", "ArialBlack", nameSize, CCLabelFormat.SystemFont);
            player1Label.Color = CCColor3B.Yellow;
            player1Label.HorizontalAlignment = CCTextAlignment.Left;
            player1Label.AnchorPoint = CCPoint.AnchorUpperLeft;
            player1Label.Position = new CCPoint(leftXPosition, nameYPosition);
            _drawBoardRoot.AddChild(player1Label);

            CCLabel score2Label = new CCLabel("0", "ArialBlack", scoreSize, CCLabelFormat.SystemFont);
            score2Label.Color = new CCColor3B(C4Colors.RedColor);
            score2Label.HorizontalAlignment = CCTextAlignment.Right;
            score2Label.AnchorPoint = CCPoint.AnchorUpperRight;
            score2Label.Position = new CCPoint(rightXPosition, scoreYPosition);
            _drawBoardRoot.AddChild(score2Label);

            CCLabel player2Label = new CCLabel("PLAYER2", "ArialBlack", nameSize, CCLabelFormat.SystemFont);
            player2Label.Color = CCColor3B.Red;
            player2Label.HorizontalAlignment = CCTextAlignment.Right;
            player2Label.AnchorPoint = CCPoint.AnchorUpperRight;
            player2Label.Position = new CCPoint(rightXPosition, nameYPosition);
            _drawBoardRoot.AddChild(player2Label);

            float bottomBarHeight = _boardCoordinates[0, 0].Y - (float)(_circleSize * 0.5);
            float quitLabelYPosition = bottomBarHeight / 2;
            float quitLabelXPosition = _viewWidth / 2;
            float quitSize = bottomBarHeight / 6;

            CCLabel resetLabel = new CCLabel("RESET", "ArialBlack", quitSize, CCLabelFormat.SystemFont);
            resetLabel.Position = new CCPoint(quitLabelXPosition, quitLabelYPosition);
            resetLabel.AnchorPoint = CCPoint.AnchorMiddleBottom;
            _drawBoardRoot.AddChild(resetLabel);

            CCLabel quitLabel = new CCLabel("QUIT", "ArialBlack", quitSize, CCLabelFormat.SystemFont);
            quitLabel.Position = new CCPoint(quitLabelXPosition, quitLabelYPosition);
            quitLabel.AnchorPoint = CCPoint.AnchorMiddleTop;
            _drawBoardRoot.AddChild(quitLabel);
        }

        private int GetColumnByTouch(CCTouch touch)
        {
            var clickedX = touch.Location.X;
            var targetColumn = _boardCoordinates.GetLength(0) - 1;
            for (int i = 0; i < _boardCoordinates.GetLength(0); i++)
            {
                var x = _boardCoordinates[i, 0].X + (_circleSize / 2) + (_circleGap / 2);
                if (clickedX < x)
                {
                    targetColumn = i;
                    break;
                }
            }
            return targetColumn;
        }

        private CCDrawNode DrawBall(int x, int y = -1)
        {
            CCDrawNode ball = new CCDrawNode();
            CCPoint pos = (y >= 0) ? _boardCoordinates[x, y] 
                                   : new CCPoint(_boardCoordinates[x, 5].X, _boardCoordinates[x, 5].Y + _circleSize + _circleGap);
            ball.DrawSolidCircle(
                pos,
                radius: _ballRadius,
                color: C4Colors.YellowColor);
            return ball;
        }

        public void MoveBall(int x, int y)
        {
            CCNode drawBall = new CCNode();
            AddChild(drawBall);

            CCDrawNode ball = DrawBall(x);
            drawBall.AddChild(ball);

            float timePerRow = 0.15f;
            float timeToTake = (_boardCoordinates.GetLength(1) - y) * timePerRow;

            var boardHeight = (_boardCoordinates.GetLength(1) * (_circleSize + _circleGap)) * -1;
            boardHeight += (y * (_circleSize + _circleGap));

            CCFiniteTimeAction coreAction = new CCMoveTo(timeToTake, new CCPoint(0, boardHeight));
            CCAction easing = new CCEaseBounceInOut(coreAction);
            drawBall.AddAction(coreAction);
        }
    }
}