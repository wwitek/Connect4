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
            int opponent1s = my4s.Count(fours => fours.Count(f => f.PlayerId == opponentId) == 1);
            int opponent2s = my4s.Count(fours => fours.Count(f => f.PlayerId == opponentId) == 2);
            int opponent3s = my4s.Count(fours => fours.Count(f => f.PlayerId == opponentId) == 3);

            int myScore = my1s + (int)Math.Pow(my2s, 2) + (int)Math.Pow(my3s, 2);
            int opponentScore = opponent1s + (int)Math.Pow(opponent2s, 2) + (int)Math.Pow(opponent3s, 2);

            return myScore - opponentScore;
        }
    }
}