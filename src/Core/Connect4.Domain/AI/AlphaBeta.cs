using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Domain.Entities;

namespace Connect4.Domain.AI
{
    public class AlphaBeta
    {
        private List<int> ColumnOrder { get; set; }
        private IBoardEvaluation Evaluation { get; }

        private IBoard Board { get; set; }
        private int BoardWidth { get; set; }
        private int MyId { get; set; }
        private int OpponentId { get; set; }
        private List<int> DefaultOrder { get; } = new List<int> { 3, 2, 4, 1, 5, 0, 6 };

        public AlphaBeta(IBoardEvaluation evaluation)
        {
            Evaluation = evaluation;
        }

        public Tuple<int, double> GenerateMove(IBoard board, int depth, int myId, int opponentId, ref List<int> columnOrder)
        {
            Board = board;
            BoardWidth = board.Width;
            MyId = myId;
            OpponentId = opponentId;
            ColumnOrder = columnOrder;

            List<Tuple<int, double>> moves = new List<Tuple<int, double>>();
            double alphabetaResult = -1000000;
            foreach (int columnIndex in ColumnOrder)
            {
                if (!Board.IsColumnFull(columnIndex))
                {
                    int rowIndex = Board.GetLowestEmptyRow(columnIndex);
                    Board.InsertChip(rowIndex, columnIndex, MyId);
                    alphabetaResult = Math.Max(alphabetaResult, AlphaBetaPruning(false, depth - 1, int.MinValue, int.MaxValue, rowIndex, columnIndex));
                    moves.Add(new Tuple<int, double>(columnIndex, alphabetaResult));
                    Board.RemoveChip(rowIndex, columnIndex);
                }
            }
            
            Tuple<int, double> bestMove = moves.OrderByDescending(m => m.Item2).FirstOrDefault();
            columnOrder = UpdateOrder(bestMove.Item1);
            return bestMove;
        }

        private List<int> UpdateOrder(int bestColumn)
        {
            List<int> newOrder = new List<int>() { bestColumn };
            newOrder.AddRange(DefaultOrder.Where(o => o != bestColumn));
            return newOrder;
        }

        private double AlphaBetaPruning(bool isMax, int depth, double alpha, double beta, int insertedRow, int insertedColumn)
        {
            if (depth == 0 || Board.IsChipConnected(insertedRow, insertedColumn))
            {
                return Evaluation.Evaluate(Board, MyId, OpponentId); ;
            }

            if (isMax)
            {
                double alphabetaResult = -1000000;
                foreach (int columnIndex in ColumnOrder)
                {
                    if (!Board.IsColumnFull(columnIndex))
                    {
                        int rowIndex = Board.GetLowestEmptyRow(columnIndex);

                        Board.InsertChip(rowIndex, columnIndex, MyId);
                        alphabetaResult = Math.Max(alphabetaResult, AlphaBetaPruning(false, depth - 1, alpha, beta, rowIndex, columnIndex));
                        Board.RemoveChip(rowIndex, columnIndex);

                        if (beta <= alphabetaResult) break;
                        alpha = Math.Max(alpha, alphabetaResult);
                    }
                }
                return alphabetaResult;
            }
            else
            {
                double alphabetaResult = 1000000;
                foreach (int columnIndex in ColumnOrder)
                {
                    if (!Board.IsColumnFull(columnIndex))
                    {
                        int rowIndex = Board.GetLowestEmptyRow(columnIndex);

                        Board.InsertChip(rowIndex, columnIndex, OpponentId);
                        alphabetaResult = Math.Min(alphabetaResult, AlphaBetaPruning(true, depth - 1, alpha, beta, rowIndex, columnIndex));
                        Board.RemoveChip(rowIndex, columnIndex);

                        if (alphabetaResult <= alpha) break;
                        beta = Math.Min(beta, alphabetaResult);
                    }
                }
                return alphabetaResult;
            }
        }
    }
}