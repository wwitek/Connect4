using Connect4.Domain.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Interfaces
{
    public interface IProxy
    {
        void GameRequest();
        void CancelRequest();
        bool Move(int column);

        event EventHandler<OnlineMoveReceivedArgs> MoveReceived;
        event EventHandler<OnlineGameStartedArgs> GameStarted;
    }
}
