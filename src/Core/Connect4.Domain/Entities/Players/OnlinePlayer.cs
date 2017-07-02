using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Entities.Players
{
    public class OnlinePlayer : IPlayer
    {
        public int Id { get; }
        public bool AllowUserInteraction { get; }
        private IProxy Proxy { get; }

        public OnlinePlayer(int id, IProxy proxy)
        {
            Id = id;
            Proxy = proxy;
            AllowUserInteraction = false;
        }

        public bool InjectMove(int column)
        {
            // Method used only for a local-type player
            throw new NotImplementedException();
        }

        public IMove WaitForMove(IBoard board)
        {
            return new Move(0, 0, Id);
        }
    }
}
