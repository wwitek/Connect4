using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;

namespace Connect4.Server
{
    [HubName("GameHub")]
    public class GameHub : Hub
    {
        private static GameServer GameServer;

        public GameHub()
        {
            Debug.WriteLine("GameHub created!");
        }

        public void GameRequest(int playerId)
        {
            Debug.WriteLine($"GameRequest (Context.ConnectionId={ Context.ConnectionId })");

            Clients.Caller.onRequestResponse(playerId);
        }

        public void CancelRequest(int playerId)
        {


        }

        public void Move(int playerId, int column)
        {
            Debug.WriteLine($"Move in column={ column } (Context.ConnectionId={ Context.ConnectionId })");

            if (GameServer == null) GameServer = new GameServer();
            GameServer.AddToWaitingList(playerId);
            Debug.WriteLine($"Move. Players in line { GameServer.WaitingList.Count }!");

            Clients.All.onMoved(column);
        }
    }
}