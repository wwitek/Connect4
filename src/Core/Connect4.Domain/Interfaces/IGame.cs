using Connect4.Domain.Enums;
using Connect4.Domain.EventArguments;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Connect4.Domain.Interfaces
{
    public interface IGame
    {
        GameState State { get; }
        IPlayer CurrentPlayer { get; }
        event EventHandler<MoveEventArgs> MoveMade;
        bool TryMove(int column);
    }
}