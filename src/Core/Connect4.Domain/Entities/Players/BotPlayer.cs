using Connect4.Domain.AI;
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
        private IterativeDeepeningSearch InterDeepeningSearch { get; }

        public BotPlayer(int id, IterativeDeepeningSearch interDeepeningSearch)
        {
            Id = id;
            AllowUserInteraction = false;
            InterDeepeningSearch = interDeepeningSearch;
        }

        public bool InjectMove(int column)
        {
            throw new NotImplementedException();
        }

        public IMove WaitForMove(IBoard board)
        {
            //int[] order = { 3, 2, 4, 1, 5, 0, 6 };
            //int column = InterDeepeningSearch.AlphaBeta.GenerateMove(board, 6, 2, 1, ref order).Item1;
            int column = InterDeepeningSearch.Search(board, 2, 1);

            int row = board.GetLowestEmptyRow(column);
            board.InsertChip(row, column, Id);

            bool isWinner = board.IsChipConnected(row, column);
            bool isDraw = !isWinner && board.IsBoardFull();
            IMove move = new Move(row, column, Id, isWinner, isDraw);
            return move;
        }
    }
}