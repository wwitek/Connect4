using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Domain.Interfaces;

namespace Connect4.Domain.AI
{
    public class BasicBoardEvaluation : IBoardEvaluation
    {
        public int Evaluate(IBoard board, int myId, int opponentId)
        {
            List<IField[]> my4s = board.PossibleFours
                .Where(fours => (fours.FirstOrDefault(field => field.PlayerId == myId) != null &&
                                 fours.FirstOrDefault(field => field.PlayerId == opponentId) == null)).ToList();

            List<IField[]> opponent4s = board.PossibleFours
                .Where(fours => (fours.FirstOrDefault(field => field.PlayerId == opponentId) != null &&
                                 fours.FirstOrDefault(field => field.PlayerId == myId) == null)).ToList();

            int my1s = my4s.Count(fours => fours.Count(f => f.PlayerId == myId) == 1);
            int my2s = my4s.Count(fours => fours.Count(f => f.PlayerId == myId) == 2);
            int my3s = my4s.Count(fours => fours.Count(f => f.PlayerId == myId) == 3);
            int myWinning = my4s.Count(fours => fours.Count(f => f.PlayerId == myId) == 4);

            int opponent1s = opponent4s.Count(fours => fours.Count(f => f.PlayerId == opponentId) == 1);
            int opponent2s = opponent4s.Count(fours => fours.Count(f => f.PlayerId == opponentId) == 2);
            int opponent3s = opponent4s.Count(fours => fours.Count(f => f.PlayerId == opponentId) == 3);
            int opponentWinning = opponent4s.Count(fours => fours.Count(f => f.PlayerId == opponentId) == 4);

            int myScore = my1s + my2s * 6 + my3s * 106 + myWinning * 10000;
            int opponentScore = opponent1s * 2 + opponent2s * 8 + opponent3s * 208 + opponentWinning * 15000;
            return myScore - opponentScore;
        }
    }
}