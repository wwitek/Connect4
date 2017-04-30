using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.AI
{
    public interface IBoardEvaluation
    {
        int Evaluate(IBoard board, int myId, int opponentId);
        int FastEvaluate(IBoard board, int myId, int opponentId);
    }
}
