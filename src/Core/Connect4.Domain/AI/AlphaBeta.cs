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
        private int MaxDepth { get; set; }
        private int[] ColumnOrder { get; set; }
        private IBoardEvaluation Evaluation { get; }

        private IBoard Board { get; set; }
        private int BoardWidth { get; set; }
        private int MyId { get; set; }
        private int OpponentId { get; set; }

        private int columnHits = 0;
        private int evalHits = 0;
        private int allEvalHits = 0;
        private int allHits = 0;
        public int allInAll = 0;

        public AlphaBeta(IBoardEvaluation evaluation)
        {
            Evaluation = evaluation;
        }

        public Tuple<int, double> GenerateMove(IBoard board, int maxDepth, int myId, int opponentId, ref int[] columnOrder)
        {
            allEvalHits = 0;
            allHits = 0;

            Board = board;
            BoardWidth = board.Width;
            MyId = myId;
            OpponentId = opponentId;
            MaxDepth = maxDepth;
            ColumnOrder = columnOrder;

            List<Tuple<int, double>> moves = new List<Tuple<int, double>>();
            for (int columnIndex = 0; columnIndex < BoardWidth; columnIndex++)
            {
                if (!Board.IsColumnFull(columnIndex))
                {
                    double value = GetMoveValue(columnIndex);

                    Debug.WriteLine("Move {0}: {1} (hits={2},evalHits={3})", columnIndex, value, columnHits, evalHits);
                    allHits += columnHits;
                    allEvalHits += evalHits;
                    columnHits = 0;
                    evalHits = 0;
                    
                    moves.Add(new Tuple<int, double>(columnIndex, value));
                }
                else
                {
                    moves.Add(new Tuple<int, double>(columnIndex, -1000000));
                }
            }

            Debug.WriteLine("All hits: {0}", allHits);
            Debug.WriteLine("All eval hits: {0}", allEvalHits);
            allInAll += allHits;

            columnOrder = moves.OrderByDescending(m => m.Item2).Select(m => m.Item1).ToArray();
            return moves.OrderByDescending(m => m.Item2).FirstOrDefault();
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
            columnHits++;
            bool isConnected = Board.IsChipConnected(insertedRow, insertedColumn);
            if (isConnected || depth == 0)
            {
                evalHits++;
                int evalOld = Evaluation.Evaluate(Board, MyId, OpponentId);
                int evalNew = Evaluation.FastEvaluate(Board, MyId, OpponentId);
                if (evalOld != evalNew)
                {
                    
                }
                return evalOld;
            }

            if (isMax)
            {
                double alphabetaResult = -10000;
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
                double alphabetaResult = 10000;
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