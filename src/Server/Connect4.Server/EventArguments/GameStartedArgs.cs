using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Connect4.Server.EventArguments
{
    public class GameStartedArgs : EventArgs
    {
        public long GameId { get; }
        public string Player1Id { get; }
        public string Player2Id { get; }

        public GameStartedArgs(long gameId, string player1Id, string player2Id)
        {
            GameId = gameId;
            Player1Id = player1Id;
            Player2Id = player2Id;
        }
    }
}