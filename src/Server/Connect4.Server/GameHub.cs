using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Connect4.Server
{
    [HubName("gameHub")]
    public class GameHub : Hub
    {
        public void RegisterPlayer()
        {
            Clients.Caller.onRegisteredPlayer(2);
        }

        public void GameRequest(int playerId)
        {


        }

        public void CancelRequest(int playerId)
        {


        }

        public void Move(int playerId, int column)
        {
            Clients.All.onMoved(column);
        }
    }
}