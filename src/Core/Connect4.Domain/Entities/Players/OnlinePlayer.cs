using Connect4.Domain.EventArguments;
using Connect4.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Entities.Players
{
    public class OnlinePlayer : IPlayer
    {
        private BlockingCollection<int> InjectedMoves { get; }

        public int Id { get; }
        public bool AllowUserInteraction { get; }
        private IProxy Proxy { get; }

        public OnlinePlayer(int id, IProxy proxy)
        {
            Id = id;
            Proxy = proxy;
            AllowUserInteraction = false;

            InjectedMoves = new BlockingCollection<int>(1);
            Proxy.MoveReceived += Proxy_MoveReceived;
        }

        private void Proxy_MoveReceived(object sender, OnlineMoveReceivedArgs e)
        {
            int receivedColumn = e.Column;
            InjectMove(receivedColumn);
        }

        public bool InjectMove(int column)
        {
            return InjectedMoves.TryAdd(column);
        }

        public IMove WaitForMove(IBoard board)
        {
            int injectedColumn = InjectedMoves.Take();
            int row = board.GetLowestEmptyRow(injectedColumn);
            board.InsertChip(row, injectedColumn, Id);

            bool isWinner = board.IsChipConnected(row, injectedColumn);
            bool isDraw = !isWinner && board.IsBoardFull();
            IMove move = new Move(row, injectedColumn, Id, isWinner, isDraw);
            return move;
        }
    }
}
