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
        public int Row { get; set; }
        public int Column { get; set; }
        public int PlayerId { get; set; }
        public bool IsWinner { get; set; }
        public bool IsDraw { get; set; }

        public Move(int row, int column, int playerId)
        {
            Row = row;
            Column = column;
            PlayerId = playerId;
        }
    }
}
