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
        private AlphaBeta AlphaBeta { get; }

        public IterativeDeepeningSearch(AlphaBeta alphaBeta)
        {
            AlphaBeta = alphaBeta;
        }

        public int Search(IBoard board, int myId, int opponentId)
        {
            int[] order = {3, 2, 4, 1, 5, 0, 6};
            int depth = 1;
            var result = AlphaBeta.GenerateMove(board, depth, myId, opponentId, ref order);
            while (true)
            {
                if (Math.Abs(result.Item2) > 5000) return result.Item1;
                result = AlphaBeta.GenerateMove(board, ++depth, myId, opponentId, ref order);
                if (depth == 6) break;
            }
            Debug.WriteLine("All in all hits: {0}" + Environment.NewLine, AlphaBeta.allInAll);
            return result.Item1;
        }
    }
}