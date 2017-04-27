using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.AI
{
    public class AlphaBeta
    {
        private const int MaxDepth = 6;
        private const int WinValue = 1;
        private const int LoseValue = -1;
        private const int DrawValue = 0;

        private readonly int[] ColumnOrder = new int[7] { 3, 2, 4, 1, 5, 0, 6 };

        private IBoard Board;
        private int BoardWidth;
        private int MyId;
        private int OpponentId;

        private int columnCounter = 0;
        private int allCounter = 0;

        public int GenerateMove(IBoard board, int myId, int opponentId)
        {
            Board = board;
            BoardWidth = board.Width;
            MyId = myId;
            OpponentId = opponentId;

            List<int> moves = new List<int>();
            double max = -1;
            for (int columnIndex = 0; columnIndex < BoardWidth; columnIndex++)
            {
                if (!Board.IsColumnFull(columnIndex))
                {
                    double value = GetMoveValue(columnIndex);
                    Debug.WriteLine("Move {0}: {1} ({2})", columnIndex, value, columnCounter);

                    allCounter += columnCounter;
                    columnCounter = 0;
                    if (value > max)
                    {
                        max = value;
                        moves.Clear();
                        moves.Add(columnIndex);
                    }
                    else if (value == max)
                    {
                        moves.Add(columnIndex);
                    }
                }
            }

            Debug.WriteLine("All hits: {0}", allCounter);

            Random random = new Random();
            int move = moves[random.Next(0, moves.Count)];
            return move;
        }

        private double GetMoveValue(int column)
        {
            int row = Board.GetLowestEmptyRow(column);
            Board.InsertChip(row, column, MyId);

            double value = AlphaBetaPruning(false, MaxDepth, int.MinValue, int.MaxValue, row, column);

            Board.RemoveChip(row, column);
            return value;
        }

        private double AlphaBetaPruning(bool isMax, int depth, double alpha, double beta, int insertedRow, int insertedColumn)
        {
            columnCounter++;

            bool isConnected = Board.IsChipConnected(insertedRow, insertedColumn);
            if (isConnected || depth == 0)
            {
                double score = 0;
                if (isConnected)
                {
                    score = (!isMax) ? WinValue : LoseValue;
                }
                else if (depth == 0)
                {
                    score = DrawValue;
                }

                return score / (MaxDepth - depth + 1);
            }

            if (isMax)
            {
                foreach (int columnIndex in ColumnOrder)
                {
                    if (!Board.IsColumnFull(columnIndex))
                    {
                        int rowIndex = Board.GetLowestEmptyRow(columnIndex);
                        Board.InsertChip(rowIndex, columnIndex, MyId);

                        double alphabetaResult = AlphaBetaPruning(false, depth - 1, alpha, beta, rowIndex, columnIndex);
                        alpha = Math.Max(alpha, alphabetaResult);

                        Board.RemoveChip(rowIndex, columnIndex);
                        if (beta <= alpha) break;
                    }
                }
                return alpha;
            }
            else
            {
                foreach (int columnIndex in ColumnOrder)
                {
                    if (!Board.IsColumnFull(columnIndex))
                    {
                        int rowIndex = Board.GetLowestEmptyRow(columnIndex);
                        Board.InsertChip(rowIndex, columnIndex, OpponentId);

                        double alphabetaResult = AlphaBetaPruning(true, depth - 1, alpha, beta, rowIndex, columnIndex);
                        beta = Math.Min(beta, alphabetaResult);

                        Board.RemoveChip(rowIndex, columnIndex);
                        if (beta <= alpha) break;
                    }
                }
                return beta;
            }
        }
    }
}