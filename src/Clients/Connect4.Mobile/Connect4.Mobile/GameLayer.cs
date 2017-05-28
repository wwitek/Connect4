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

        private float[] BoardHeightRange { get; set; } = new float[2];
        private float[] ResetHeightRange { get; set; } = new float[2];
        private float[] QuitHeightRange { get; set; } = new float[2];

        private CCLabel LeftScoreLabel { get; set; }
        private CCLabel RightScoreLabel { get; set; }

        public event EventHandler OnPreTouched;
        public event EventHandler OnTouched;
        public event EventHandler OnReset;
        public event EventHandler OnQuit;

        public GameLayer(float viewWidth, float viewHeight)
            : base(C4Colors.StartColor, C4Colors.EndColor, new CCPoint(0f, 1f))
        {
            BoardCoordinates = new CCPoint[7, 6];
            ViewWidth = viewWidth;
            ViewHeight = viewHeight;
            CircleGap = (float)Math.Ceiling(ViewWidth / 7 * 0.1);
            CircleSize = (float)Math.Floor((ViewWidth - (CircleGap * 10)) / 7);
            if (((CircleSize % 2) == 0 && (ViewWidth % 2) != 0) || ((CircleSize % 2) != 0 && (ViewWidth % 2) == 0)) CircleSize--;
            EdgeGap = (ViewWidth - (CircleGap * 10) - (CircleSize * 7)) / 2;
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
                    GamePagePart partClicked = GetGamePagePartClicked(touches[0]);
                    if (partClicked == GamePagePart.Board)
                    {
                        var targetColumn = GetColumnByTouch(touches[0]);
                        if (targetColumn >= 0)
                        {
                            OnTouchedEventArgs tea = new OnTouchedEventArgs(targetColumn);
                            OnTouched?.Invoke(this, tea);
                            DrawPreDropRoot.RemoveAllChildren();
                        }
                    }
                    else if (partClicked == GamePagePart.ResetButton)
                    {
                        Debug.WriteLine("Reset");
                        OnReset?.Invoke(this, EventArgs.Empty);
                        DrawBallsRoot.RemoveAllChildren(true);
                        LeftScoreLabel.Text = 0.ToString();
                        RightScoreLabel.Text = 0.ToString();
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
                    OnTouchedEventArgs tea = new OnTouchedEventArgs(targetColumn);
                    OnPreTouched?.Invoke(this, tea);
                }
            }
        }

        private void InitializeBoardCoordinates()
        {
            float y = (ViewHeight / 2) - (4 * CircleGap) - (4 * CircleSize);
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
                        color: C4Colors.CircleLighterColor);
                    DrawBoardRoot.AddChild(circle);

                    CCDrawNode ellipse = new CCDrawNode();
                    ellipse.DrawEllipse(
                        rect: new CCRect(x - (CircleSize / 2), y - (CircleSize / 2), CircleSize, CircleSize),
                        lineWidth: 1,
                        color: C4Colors.CircleBorderLight);
                    DrawBoardRoot.AddChild(ellipse);
                }
            }

            float scoreYPosition = ViewHeight - 10;
            float topBarHeight = scoreYPosition - (BoardCoordinates[0, 5].Y + (float)(CircleSize * 1.5) + CircleGap) - 10;
            float nameSize = 16;
            float scoreSize = topBarHeight - nameSize;
            float nameYPosition = scoreYPosition - scoreSize;
            float leftXPosition = BoardCoordinates[0, 0].X - (BoardCoordinates[0, 0].X / 2) - EdgeGap;
            float rightXPosition = BoardCoordinates[6, 0].X + (CircleSize / 2);

            LeftScoreLabel = new CCLabel("0", "ArialBlack", scoreSize, CCLabelFormat.SystemFont);
            LeftScoreLabel.Color = new CCColor3B(C4Colors.YellowColor);
            LeftScoreLabel.HorizontalAlignment = CCTextAlignment.Left;
            LeftScoreLabel.AnchorPoint = CCPoint.AnchorUpperLeft;
            LeftScoreLabel.Position = new CCPoint(leftXPosition, scoreYPosition);
            DrawBoardRoot.AddChild(LeftScoreLabel);

            CCLabel player1Label = new CCLabel("PLAYER1", "ArialBlack", nameSize, CCLabelFormat.SystemFont);
            player1Label.Color = CCColor3B.Yellow;
            player1Label.HorizontalAlignment = CCTextAlignment.Left;
            player1Label.AnchorPoint = CCPoint.AnchorUpperLeft;
            player1Label.Position = new CCPoint(leftXPosition, nameYPosition);
            DrawBoardRoot.AddChild(player1Label);

            RightScoreLabel = new CCLabel("0", "ArialBlack", scoreSize, CCLabelFormat.SystemFont);
            RightScoreLabel.Color = new CCColor3B(C4Colors.RedColor);
            RightScoreLabel.HorizontalAlignment = CCTextAlignment.Right;
            RightScoreLabel.AnchorPoint = CCPoint.AnchorUpperRight;
            RightScoreLabel.Position = new CCPoint(rightXPosition, scoreYPosition);
            DrawBoardRoot.AddChild(RightScoreLabel);

            CCLabel player2Label = new CCLabel("PLAYER2", "ArialBlack", nameSize, CCLabelFormat.SystemFont);
            player2Label.Color = CCColor3B.Red;
            player2Label.HorizontalAlignment = CCTextAlignment.Right;
            player2Label.AnchorPoint = CCPoint.AnchorUpperRight;
            player2Label.Position = new CCPoint(rightXPosition, nameYPosition);
            DrawBoardRoot.AddChild(player2Label);

            float bottomBarHeight = BoardCoordinates[0, 0].Y - (float)(CircleSize * 0.5);
            float quitLabelYPosition = bottomBarHeight / 2;
            float quitLabelXPosition = ViewWidth / 2;
            float quitSize = bottomBarHeight / 6;

            CCLabel resetLabel = new CCLabel("RESET", "ArialBlack", quitSize, CCLabelFormat.SystemFont);
            resetLabel.Position = new CCPoint(quitLabelXPosition, quitLabelYPosition);
            resetLabel.AnchorPoint = CCPoint.AnchorMiddleBottom;
            DrawBoardRoot.AddChild(resetLabel);

            CCLabel quitLabel = new CCLabel("QUIT", "ArialBlack", quitSize, CCLabelFormat.SystemFont);
            quitLabel.Position = new CCPoint(quitLabelXPosition, quitLabelYPosition);
            quitLabel.AnchorPoint = CCPoint.AnchorMiddleTop;
            DrawBoardRoot.AddChild(quitLabel);

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
                    ball.DrawSolidCircle(pos, radius: BallRadius, color: C4Colors.YellowColor);
                    break;
                case PlayerColor.Red:
                    ball.DrawSolidCircle(pos, radius: BallRadius, color: C4Colors.RedColor);
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

        public void SetScore(PlayerColor player, int score)
        {
            switch(player)
            {
                case PlayerColor.Yellow:
                    LeftScoreLabel.Text = score.ToString();
                    break;
                case PlayerColor.Red:
                    RightScoreLabel.Text = score.ToString();
                    break;
            }
        }
    }
}