using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Entities.Players
{
    public class BotPlayer : IPlayer
    {
        public int Id { get; }
        public bool AllowUserInteraction { get; }

        public BotPlayer(int id)
        {
            Id = id;
            AllowUserInteraction = false;
        }

        public bool InjectMove(int column)
        {
            return true;
        }

        public IMove WaitForMove(IBoard board)
        {
            int column = 0;
            while (true)
            {
                Random r = new Random();
                column = r.Next(0, 6);
                if (!board.IsColumnFull(column)) break;
            }

            int row = board.GetLowestEmptyRow(column);
            board.InsertChip(row, column, Id);

            IMove move = new Move(row, column, Id);
            move.IsWinner = board.IsChipConnected(row, column);
            move.IsDraw = !move.IsWinner && board.IsBoardFull();
            return move;
        }
    }
}
