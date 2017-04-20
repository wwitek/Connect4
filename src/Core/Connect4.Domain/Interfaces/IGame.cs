using Connect4.Domain.EventArguments;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Connect4.Domain.Interfaces
{
    public interface IGame
    {
        event EventHandler<MoveEventArgs> OnMoveMade;
        bool TryMove(int column);
    }
}