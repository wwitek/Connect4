using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Entities
{
    public class Game : IGame
    {
        private IBoard Board { get; }
        private List<IPlayer> Players { get; }

        public Game(IBoard board, List<IPlayer> players)
        {
            Board = board;
            Players = players;
        }
    }
}
