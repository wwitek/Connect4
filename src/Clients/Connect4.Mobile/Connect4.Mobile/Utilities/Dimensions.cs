using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile.Utilities
{
    public class Dimensions
    {
        public double Margin { get; }
        public double BoardHeight { get; }
        public double BoardWidth { get; }
        public double BoardPadding { get; }

        public double BoardBorder { get; }
        public double CircleSize { get; }
        public double CircleGap { get; }

        public double ButtonHeight { get; }
        public double ButtonWidth { get; }
        public double ButtonFontSize { get; }
        public double TitleHeight { get; }
        public double TitleFontSize { get; }

        public Dimensions(float viewWidth, float viewHeight, int columns, int rows, double pixelDensity = 1)
        {
            // Consts
            if (pixelDensity == 0) pixelDensity = 1;
            BoardBorder = 2;

            ButtonFontSize = 18;
            TitleFontSize = 45;
            ButtonHeight = 40 * pixelDensity;
            TitleHeight = TitleFontSize * pixelDensity;
            // -----------------------------------

            BoardWidth = Math.Round(viewWidth * 0.8, 0);
            if (BoardWidth % 2 != 0) BoardWidth -= 1;
            Margin = (viewWidth - BoardWidth) / 2;

            int gapsCount = columns + 3;
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
            BoardHeight = CircleGap * (rowGapsCount + 1) + CircleSize * (rows + 1) + BoardPadding;
            double topAndBottomSpaceHeight = (viewHeight - BoardHeight) / 2;

            ButtonWidth = BoardWidth * 0.8;
        }
    }
}
