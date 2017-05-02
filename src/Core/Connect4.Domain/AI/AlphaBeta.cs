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
        private int[] ColumnOrder { get; set; }
        private IBoardEvaluation Evaluation { get; }

        private IBoard Board { get; set; }
        private int BoardWidth { get; set; }
        private int MyId { get; set; }
        private int OpponentId { get; set; }

        private int columnHits = 0;
        private int evalHits = 0;

        public AlphaBeta(IBoardEvaluation evaluation)
        {
            Evaluation = evaluation;
        }

        public Tuple<int, double> GenerateMove(IBoard board, int depth, int myId, int opponentId, ref int[] columnOrder)
        {
            columnHits = 0;
            evalHits = 0;

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

            Debug.WriteLine("All hits: {0}", columnHits);
            Debug.WriteLine("All eval hits: {0}{1}", evalHits, Environment.NewLine);

            Tuple<int, double> bestMove = moves.OrderByDescending(m => m.Item2).FirstOrDefault();
            //columnOrder = UpdateOrder(bestMove, columnOrder);
            return bestMove;
        }

        private int[] UpdateOrder(Tuple<int, double> bestMove, int[] currentOrder)
        {
            while (currentOrder[0] != bestMove.Item1)
            {
                currentOrder = ShiftLeft(currentOrder);
            }
            return currentOrder;
        }

        public int[] ShiftLeft(int[] arr)
        {
            int[] demo = new int[arr.Length];
            for (int i = 0; i < arr.Length - 1; i++)
            {
                demo[i] = arr[i + 1];
            }
            demo[demo.Length - 1] = arr[0];
            return demo;
        }

        private double AlphaBetaPruning(bool isMax, int depth, double alpha, double beta, int insertedRow, int insertedColumn)
        {
            columnHits++;
            if (depth == 0 || Board.IsChipConnected(insertedRow, insertedColumn))
            {
                evalHits++;
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