using Connect4.Domain.Entities;
using Connect4.Domain.Entities.Players;
using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Connect4.Domain.AI;

namespace Connect4.Diagnostics
{
    public class Diagnostics
    {
        public List<long> IterativeDeepeningSearchTest(int[,] ids, int count)
        {
            IField[,] fields = new IField[ids.GetLength(0), ids.GetLength(1)];
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    fields[i, j] = new Field(i, j);
                    fields[i, j].PlayerId = ids[i, j];
                }
            }
            IBoard board = new Board(fields);

            IBoardEvaluation eval = new BasicBoardEvaluation();
            AlphaBeta alphaBetaSearch = new AlphaBeta(eval);
            IterativeDeepeningSearch interDeepeningSearch = new IterativeDeepeningSearch(alphaBetaSearch);

            List<long> totalTime = new List<long>();
            for (int i = 0; i < count; i++)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Reset();

                stopwatch.Start();
                int column = interDeepeningSearch.Search(board, 2, 1);
                stopwatch.Stop();
                totalTime.Add(stopwatch.ElapsedMilliseconds);

                //Console.WriteLine($"Moved to column { column } in time={ stopwatch.ElapsedMilliseconds }ml");
            }
            return totalTime;
        }

        public List<long> AlphaBetaTest(int[,] ids, int count)
        {
            IField[,] fields = new IField[ids.GetLength(0), ids.GetLength(1)];
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    fields[i, j] = new Field(i, j);
                    fields[i, j].PlayerId = ids[i, j];
                }
            }
            IBoard board = new Board(fields);

            IBoardEvaluation eval = new BasicBoardEvaluation();
            AlphaBeta alphaBetaSearch = new AlphaBeta(eval);
            List<int> order = new List<int> { 3, 2, 4, 1, 5, 0, 6 };

            List<long> totalTime = new List<long>();
            for (int i = 0; i < count; i++)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Reset();

                stopwatch.Start();
                int column = alphaBetaSearch.GenerateMove(board, 8, 2, 1, ref order).Item1;
                stopwatch.Stop();
                totalTime.Add(stopwatch.ElapsedMilliseconds);

                //Console.WriteLine($"Moved to column { column } in time={ stopwatch.ElapsedMilliseconds }ml");
            }
            return totalTime;
        }
    }
}
