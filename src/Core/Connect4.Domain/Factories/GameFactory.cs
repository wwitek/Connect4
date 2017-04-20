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
        private IBoardFactory BoardFactory { get; }

        public GameFactory(IBoardFactory boardFactory)
        {
            BoardFactory = boardFactory;
        }

        public IGame Create(List<IPlayer> players)
        {
            IBoard board = BoardFactory.Create(6, 7);
            return new Game(board, players);
        }
    }
}
