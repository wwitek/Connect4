using Connect4.Server.EventArguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect4.Server.Interfaces
{
    public interface IDataProvider
    {
        bool IsInWaitingList(string playerConnectionId);
        void AddToWaitingList(string playerConnectionId);
        void RemoveFromWaitingList(string playerConnectionId);
        string GetWaitingPlayer();

        long AddGame(string player1ConnectionId, string player2ConnectionId);
        void RemoveGame(long gameId);
        long GetGameId(string playerConnectionId);
        string GetFirstPlayer(long gameId);
    }
}