using Connect4.Domain.Exceptions;
using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Entities
{
    public class Move : IMove
    {
        public int Row { get; }
        public int Column { get; }
        public int PlayerId { get; }
        public bool IsWinner { get; }
        public bool IsDraw { get; }

        public Move(int row, int column, int playerId, bool isWinner = false, bool isDraw = false)
        {
            if (isWinner && isDraw) throw new MoveException("Move cannot be winning and draw at the same time");

            Row = row;
            Column = column;
            PlayerId = playerId;
            IsWinner = isWinner;
            IsDraw = isDraw;
        }
    }
}
