using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Diagnostics;
using Connect4.Server.EventArguments;

namespace Connect4.Server
{
    [HubName("GameHub")]
    public class GameHub : Hub
    {
        private readonly GameServer GameServer;

        public GameHub(GameServer gameServer)
        {
            Debug.WriteLine($"[GameHub] Created");
            GameServer = gameServer;
        }

        public void GameRequest()
        {
            if (!GameServer.IsInWaitingList(Context.ConnectionId))
            {
                Debug.WriteLine($"[GameHub] GameRequest (Context.ConnectionId={ Context.ConnectionId })");
                GameServer.GameStarted += OnGameStarted;
                GameServer.AddToWaitingList(Context.ConnectionId);

                Clients.Caller.onRequestResponse(Context.ConnectionId);
            }
        }

        public void CancelRequest()
        {
            Debug.WriteLine($"[GameHub] CancelRequest (Context.ConnectionId={ Context.ConnectionId })");
            GameServer.GameStarted -= OnGameStarted;
            GameServer.RemoveFromWaitingList(Context.ConnectionId);
        }

        public void Move(int column)
        {
            Debug.WriteLine($"[GameHub] Move (Context.ConnectionId={ Context.ConnectionId })");
            long gameId = GameServer.GetGameId(Context.ConnectionId);
            Clients.OthersInGroup(gameId.ToString()).onMoved(column);
        }

        private void OnGameStarted(object sender, GameStartedArgs e)
        {
            if (e.Player1Id == Context.ConnectionId || e.Player2Id == Context.ConnectionId)
            {
                GameServer.GameStarted -= OnGameStarted;
                Debug.WriteLine($"[GameHub] GameStarted. P1={e.Player1Id}; P2={e.Player2Id} (Context.ConnectionId={ Context.ConnectionId })");
                Groups.Add(Context.ConnectionId, e.GameId.ToString());
            }
        }
    }
}