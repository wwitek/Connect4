using Connect4.Domain.Entities;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IBoard board;

        public GameFactory(IBoard board)
        {
            this.board = board;
        }

        public IGame Create(List<IPlayer> players)
        {

            return new Game(board, players);
        }
    }
}
