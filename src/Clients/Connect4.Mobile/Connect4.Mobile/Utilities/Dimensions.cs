using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile.Utilities
{
    public class Dimensions
    {
        public double MinButtonHeight { get; }
        public double MinLabelHeight { get; }

        public double Margin { get; }
        public double BoardHeight { get; }
        public double BoardWidth { get; }
        public double ButtonHeight { get; }

        public double CircleSize { get; }
        public double CircleGap { get; }
        public double BoardPadding { get; }

        public Dimensions(float viewWidth, float viewHeight, int columns, int rows)
        {
            int gapsCount = columns + 3;

            MinButtonHeight = 40;
            MinLabelHeight = 40;
            ButtonHeight = MinButtonHeight;

            BoardWidth = Math.Round(viewWidth * 0.8, 0);
            if (BoardWidth % 2 != 0) BoardWidth -= 1;
            Margin = (viewWidth - BoardWidth) / 2;

            CircleGap = (float)Math.Ceiling(BoardWidth / columns * 0.1);
            CircleSize = (float)Math.Floor((BoardWidth - (CircleGap * gapsCount)) / columns);

            if (CircleGap * gapsCount + CircleSize * columns != BoardWidth)
            {
                if (columns % 2 != 0)
                {
                    if (((CircleSize % 2) == 0 && (BoardWidth % 2) != 0) || ((CircleSize % 2) != 0 && (BoardWidth % 2) == 0)) CircleSize--;
                }
                else if (gapsCount % 2 != 0)
                {
                    if (((CircleGap % 2) == 0 && (BoardWidth % 2) != 0) || ((CircleGap % 2) != 0 && (BoardWidth % 2) == 0)) CircleGap--;
                }
                else
                {
                    throw new Exception("Dimensions exceptions");
                }

                BoardPadding = (BoardWidth - (CircleGap * gapsCount) - (CircleSize * columns)) / 2;

                if ((BoardPadding*2)%2 != 0)
                    throw new Exception("Dimensions exceptions");

                if (CircleGap * gapsCount + CircleSize * columns + 2 * BoardPadding != BoardWidth)
                    throw new Exception("Dimensions exceptions");
            }

            int rowGapsCount = rows + 3;
            BoardHeight = CircleGap * (rowGapsCount + 1) + CircleSize * (rows + 1);

            double topAndBottomSpaceHeight = (viewHeight - BoardHeight) / 2;
            if (MinButtonHeight * 2 > topAndBottomSpaceHeight)
            {
                // Board is too big, shrink the board
            }
            else
            {

            }

        }
    }
}
