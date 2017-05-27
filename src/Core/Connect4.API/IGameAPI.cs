using Connect4.Domain.Enums;
using Connect4.Domain.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.API
{
    public interface IGameAPI
    {
        event EventHandler<MoveEventArgs> OnMoveMade;
        void Start(GameType gameType);
        bool TryMove(int column);
    }
}
