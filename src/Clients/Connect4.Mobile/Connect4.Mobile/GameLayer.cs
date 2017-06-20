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
        private float ViewWidth { get; }
        private float ViewHeight { get; }
        private float CircleSize { get; }
        private float CircleGap { get; }
        private float EdgeGap { get; }
        private float BallRadius { get; }
        private CCPoint[,] BoardCoordinates { get; }

        private CCNode DrawBallsRoot { get; }
        private CCNode DrawBoardRoot { get; }
        private CCNode DrawPreDropRoot { get; }
        private Dimensions Dimensions { get; }
        private Colors Colors { get; }

        public event EventHandler OnPreTouched;
        public event EventHandler OnTouched;
        public event EventHandler OnReset;

        public GameLayer(float viewWidth, float viewHeight, Dimensions dimensions, Colors colors)
            : base(colors.BoardBackgroundLight, colors.BoardBackgroundDark, new CCPoint(0f, 1f))
        {
            BoardCoordinates = new CCPoint[7, 6];
            Dimensions = dimensions;
            Colors = colors;

            ViewWidth = viewWidth;
            ViewHeight = viewHeight;
            CircleGap = (float)dimensions.CircleGap;
            CircleSize = (float)dimensions.CircleSize;
            EdgeGap = (float)dimensions.BoardPadding;

            BallRadius = (CircleSize / 2) - 1;
            DrawBallsRoot = new CCNode();
            DrawBoardRoot = new CCNode();
            DrawPreDropRoot = new CCNode();
            AddChild(DrawBoardRoot);
            AddChild(DrawBallsRoot);
            AddChild(DrawPreDropRoot);

            InitializeBoardCoordinates();
            InitializeBoard();

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesEnded = (touches, ccevent) =>
            {
                if (touches.Count > 0)
                {
                    var targetColumn = GetColumnByTouch(touches[0]);
                    if (targetColumn >= 0)
                    {
                        OnTouchedEventArgs tea = new OnTouchedEventArgs(targetColumn);
                        OnTouched?.Invoke(this, tea);
                        DrawPreDropRoot.RemoveAllChildren();
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
                    OnTouchedEventArgs tea = new OnTouchedEventArgs(targetColumn);
                    OnPreTouched?.Invoke(this, tea);
                }
            }
        }

        private void InitializeBoardCoordinates()
        {
            // Starting point, when the board is placed in the middle of the screen. 
            // float y = (ViewHeight / 2) - (4 * CircleGap) - (4 * CircleSize);

            float y = -(CircleSize + CircleGap) + (CircleGap * 2) + EdgeGap;
            for (int j = 0; j < 6; j++)
            {
                y += (CircleSize + CircleGap);
                for (int i = 0; i < 7; i++)
                {
                    float x = EdgeGap + (2 * CircleGap);
                    if (i > 0)
                    {
                        x += i * (CircleSize + CircleGap);
                    }
                    var coordinatesX = x + CircleSize / 2;
                    var coordinatesY = y + CircleSize / 2;
                    BoardCoordinates[i, j] = new CCPoint(coordinatesX, coordinatesY);
                }
            }
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < BoardCoordinates.GetLength(0); i++)
            {
                for (int j = 0; j < BoardCoordinates.GetLength(1); j++)
                {
                    float x = BoardCoordinates[i, j].X;
                    float y = BoardCoordinates[i, j].Y;

                    CCDrawNode circle = new CCDrawNode();
                    circle.DrawSolidCircle(
                        new CCPoint(x, y),
                        radius: CircleSize / 2,
                        color: Colors.CircleBackground);
                    DrawBoardRoot.AddChild(circle);

                    CCDrawNode ellipse = new CCDrawNode();
                    ellipse.DrawEllipse(
                        rect: new CCRect(x - (CircleSize / 2), y - (CircleSize / 2), CircleSize, CircleSize),
                        lineWidth: 1,
                        color: Colors.CircleBorder);
                    DrawBoardRoot.AddChild(ellipse);
                }
            }
        }

        private int GetColumnByTouch(CCTouch touch)
        {
            var targetColumn = -1;
            var clickedX = touch.Location.X;
            targetColumn = BoardCoordinates.GetLength(0) - 1;
            for (int i = 0; i < BoardCoordinates.GetLength(0); i++)
            {
                var x = BoardCoordinates[i, 0].X + (CircleSize / 2) + (CircleGap / 2);
                if (clickedX < x)
                {
                    targetColumn = i;
                    break;
                }
            }
            return targetColumn;
        }

        private CCDrawNode DrawBall(PlayerColor player, int x, int y = -1)
        {
            CCDrawNode ball = new CCDrawNode();
            CCPoint pos = (y >= 0) ? BoardCoordinates[x, y] 
                                   : new CCPoint(BoardCoordinates[x, 5].X, BoardCoordinates[x, 5].Y + CircleSize + CircleGap);

            switch (player)
            {
                case PlayerColor.Yellow:
                    ball.DrawSolidCircle(pos, radius: BallRadius, color: Colors.YellowColor);
                    break;
                case PlayerColor.Red:
                    ball.DrawSolidCircle(pos, radius: BallRadius, color: Colors.RedColor);
                    break;
            }
            return ball;
        }

        public void PreTouch(PlayerColor player, int column)
        {
            if (column >= 0)
            {
                CCDrawNode ball = DrawBall(player, column);
                DrawPreDropRoot.RemoveAllChildren();
                DrawPreDropRoot.AddChild(ball);
            }
            else
            {
                DrawPreDropRoot.RemoveAllChildren();
            }
        }

        public void MoveBall(PlayerColor player, int x, int y)
        {
            CCNode drawBall = new CCNode();
            DrawBallsRoot.AddChild(drawBall);

            CCDrawNode ball = DrawBall(player, x);
            drawBall.AddChild(ball);

            float timePerRow = 0.15f;
            float timeToMove = (y + 1) * timePerRow;
            var destinationYPosition = -1 * ((y + 1) * (CircleSize + CircleGap));

            CCFiniteTimeAction coreAction = new CCMoveTo(timeToMove, new CCPoint(0, destinationYPosition));
            CCAction easing = new CCEaseBounceInOut(coreAction);
            drawBall.AddAction(coreAction);
        }
    }
}