using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Domain.Interfaces;

namespace Connect4.Domain.AI
{
    public class IterativeDeepeningSearch
    {
        public AlphaBeta AlphaBeta { get; }

        public IterativeDeepeningSearch(AlphaBeta alphaBeta)
        {
            AlphaBeta = alphaBeta;
        }

        public int Search(IBoard board, int myId, int opponentId)
        {
            Stopwatch stopwatch = new Stopwatch();

            int depth = 1;
            List<int> order = new List<int> { 3, 2, 4, 1, 5, 0, 6 };

            var result = AlphaBeta.GenerateMove(board, depth, myId, opponentId, ref order);
            while (true)
            {
                if (Math.Abs(result.Item2) > 5000) return result.Item1;
                stopwatch.Reset();
                stopwatch.Start();
                result = AlphaBeta.GenerateMove(board, ++depth, myId, opponentId, ref order);
                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds > 500 || depth == 8) break;
            }
            return result.Item1;
        }
    }
}