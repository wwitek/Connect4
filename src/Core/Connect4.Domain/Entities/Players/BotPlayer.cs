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

        public BotPlayer(int id)
        {
            Id = id;
            AllowUserInteraction = false;
        }

        public bool InjectMove(int column)
        {
            throw new NotImplementedException();
        }

        public IMove WaitForMove(IBoard board)
        {
            IBoardEvaluation eval = new BasicBoardEvaluation();
            AlphaBeta alphaBeta = new AlphaBeta(eval);
            int column = alphaBeta.GenerateMove(board, 2, 1);

            int row = board.GetLowestEmptyRow(column);
            board.InsertChip(row, column, Id);

            bool isWinner = board.IsChipConnected(row, column);
            bool isDraw = !isWinner && board.IsBoardFull();
            IMove move = new Move(row, column, Id, isWinner, isDraw);
            return move;
        }
    }
}
