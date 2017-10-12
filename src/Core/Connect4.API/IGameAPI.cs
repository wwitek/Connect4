using Connect4.Domain.Enums;
using Connect4.Domain.EventArguments;
using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.API
{
    public interface IGameAPI
    {
        event EventHandler<MoveEventArgs> MoveMade;
        void Start(GameType gameType, IProxy proxy = null, bool goesFirst = false);
        bool TryMove(int column);
        IPlayer GetCurrentPlayer();
        GameState GetGameState();
    }
}
