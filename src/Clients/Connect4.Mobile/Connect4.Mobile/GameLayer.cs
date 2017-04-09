using CocosSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Connect4.Mobile.Utilities;
using Connect4.Mobile.EventArguments;
using Connect4.Mobile.Enums;

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

        private readonly CCNode _drawBallsRoot;
        private readonly CCNode _drawBoardRoot;
        private readonly CCNode _drawPreDropRoot;

        private float[] BoardHeightRange = new float[2];
        private float[] ResetHeightRange = new float[2];
        private float[] QuitHeightRange = new float[2];

        public event EventHandler OnTouched;
        public event EventHandler OnReset;
        public event EventHandler OnQuit;

        private CCLabel leftScoreLabel;
        private CCLabel rightScoreLabel;

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
            _drawBallsRoot = new CCNode();
            _drawBoardRoot = new CCNode();
            _drawPreDropRoot = new CCNode();
            AddChild(_drawBoardRoot);
            AddChild(_drawBallsRoot);
            AddChild(_drawPreDropRoot);

            InitializeBoardCoordinates();
            InitializeBoard();

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = (touches, ccevent) =>
            {
                if (touches.Count > 0)
                {
                    GamePagePart partClicked = GetGamePagePartClicked(touches[0]);
                    if (partClicked == GamePagePart.Board)
                    {
                        var targetColumn = GetColumnByTouch(touches[0]);
                        if (targetColumn >= 0)
                        {
                            OnTouchedEventArgs tea = new OnTouchedEventArgs(targetColumn);
                            OnTouched?.Invoke(this, tea);
                            _drawPreDropRoot.RemoveAllChildren();
                        }
                    }
                    else if (partClicked == GamePagePart.ResetButton)
                    {
                        Debug.WriteLine("Reset");
                        OnReset?.Invoke(this, EventArgs.Empty);
                        _drawBallsRoot.RemoveAllChildren(true);
                        leftScoreLabel.Text = 0.ToString();
                        rightScoreLabel.Text = 0.ToString();
                    }
                    else if (partClicked == GamePagePart.QuitButton)
                    {
                        Debug.WriteLine("Quit");
                        OnQuit?.Invoke(this, EventArgs.Empty);
                    }
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
                if (targetColumn >= 0)
                {
                    CCDrawNode ball = DrawBall(PlayerColor.Yellow, targetColumn);
                    _drawPreDropRoot.RemoveAllChildren();
                    _drawPreDropRoot.AddChild(ball);
                }
                else
                {
                    _drawPreDropRoot.RemoveAllChildren();
                }
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

            leftScoreLabel = new CCLabel("0", "ArialBlack", scoreSize, CCLabelFormat.SystemFont);
            leftScoreLabel.Color = new CCColor3B(C4Colors.YellowColor);
            leftScoreLabel.HorizontalAlignment = CCTextAlignment.Left;
            leftScoreLabel.AnchorPoint = CCPoint.AnchorUpperLeft;
            leftScoreLabel.Position = new CCPoint(leftXPosition, scoreYPosition);
            _drawBoardRoot.AddChild(leftScoreLabel);

            CCLabel player1Label = new CCLabel("PLAYER1", "ArialBlack", nameSize, CCLabelFormat.SystemFont);
            player1Label.Color = CCColor3B.Yellow;
            player1Label.HorizontalAlignment = CCTextAlignment.Left;
            player1Label.AnchorPoint = CCPoint.AnchorUpperLeft;
            player1Label.Position = new CCPoint(leftXPosition, nameYPosition);
            _drawBoardRoot.AddChild(player1Label);

            rightScoreLabel = new CCLabel("0", "ArialBlack", scoreSize, CCLabelFormat.SystemFont);
            rightScoreLabel.Color = new CCColor3B(C4Colors.RedColor);
            rightScoreLabel.HorizontalAlignment = CCTextAlignment.Right;
            rightScoreLabel.AnchorPoint = CCPoint.AnchorUpperRight;
            rightScoreLabel.Position = new CCPoint(rightXPosition, scoreYPosition);
            _drawBoardRoot.AddChild(rightScoreLabel);

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

            BoardHeightRange = new float[2] { bottomBarHeight, scoreYPosition - topBarHeight };
            ResetHeightRange = new float[2] { quitLabelYPosition, quitLabelYPosition + quitSize };
            QuitHeightRange = new float[2] { quitLabelYPosition - quitSize, quitLabelYPosition };
        }

        private GamePagePart GetGamePagePartClicked(CCTouch touch)
        {
            var clickedY = touch.Location.Y;

            if (clickedY > BoardHeightRange[0] && clickedY < BoardHeightRange[1])
            {
                return GamePagePart.Board;
            }
            else if (clickedY > ResetHeightRange[0] && clickedY < ResetHeightRange[1])
            {
                return GamePagePart.ResetButton;
            }
            else if (clickedY > QuitHeightRange[0] && clickedY < QuitHeightRange[1])
            {
                return GamePagePart.QuitButton;
            }

            return GamePagePart.Void;
        }

        private int GetColumnByTouch(CCTouch touch)
        {
            var targetColumn = -1;
            if (GetGamePagePartClicked(touch) == GamePagePart.Board)
            {
                var clickedX = touch.Location.X;
                targetColumn = _boardCoordinates.GetLength(0) - 1;
                for (int i = 0; i < _boardCoordinates.GetLength(0); i++)
                {
                    var x = _boardCoordinates[i, 0].X + (_circleSize / 2) + (_circleGap / 2);
                    if (clickedX < x)
                    {
                        targetColumn = i;
                        break;
                    }
                }
            }
            return targetColumn;
        }

        private CCDrawNode DrawBall(PlayerColor player, int x, int y = -1)
        {
            CCDrawNode ball = new CCDrawNode();
            CCPoint pos = (y >= 0) ? _boardCoordinates[x, y] 
                                   : new CCPoint(_boardCoordinates[x, 5].X, _boardCoordinates[x, 5].Y + _circleSize + _circleGap);

            switch (player)
            {
                case PlayerColor.Yellow:
                    ball.DrawSolidCircle(pos, radius: _ballRadius, color: C4Colors.YellowColor);
                    break;
                case PlayerColor.Red:
                    ball.DrawSolidCircle(pos, radius: _ballRadius, color: C4Colors.RedColor);
                    break;
            }
            return ball;
        }

        public void MoveBall(PlayerColor player, int x, int y)
        {
            CCNode drawBall = new CCNode();
            _drawBallsRoot.AddChild(drawBall);

            CCDrawNode ball = DrawBall(player, x);
            drawBall.AddChild(ball);

            float timePerRow = 0.15f;
            float timeToTake = (_boardCoordinates.GetLength(1) - y) * timePerRow;

            var boardHeight = (_boardCoordinates.GetLength(1) * (_circleSize + _circleGap)) * -1;
            boardHeight += (y * (_circleSize + _circleGap));

            CCFiniteTimeAction coreAction = new CCMoveTo(timeToTake, new CCPoint(0, boardHeight));
            CCAction easing = new CCEaseBounceInOut(coreAction);
            drawBall.AddAction(coreAction);
        }

        public void SetScore(PlayerColor player, int score)
        {
            switch(player)
            {
                case PlayerColor.Yellow:
                    leftScoreLabel.Text = score.ToString();
                    break;
                case PlayerColor.Red:
                    rightScoreLabel.Text = score.ToString();
                    break;
            }
        }
    }
}