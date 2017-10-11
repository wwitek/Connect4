using Connect4.Domain.Interfaces;
using Connect4.Server.EventArguments;
using Connect4.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Connect4.Server
{
    public class GameServer
    {
        public IDataProvider Data { get; }
        public event EventHandler<GameStartedArgs> GameStarted;

        private readonly object waitingListLocker = new object();

        public GameServer(IDataProvider data)
        {
            Debug.WriteLine($"[GameServer] Created");
            Data = data;
        }

        public bool IsInWaitingList(string playerConnectionId)
        {
            return Data.IsInWaitingList(playerConnectionId);
        }

        public void AddToWaitingList(string playerConnectionId)
        {
            lock (waitingListLocker)
            {
                Debug.WriteLine($"[GameServer] AddToWaitingList (Context.ConnectionId={ playerConnectionId })");
                string waitingPlayerId = Data.GetWaitingPlayer();

                if (waitingPlayerId != null)
                {
                    Debug.WriteLine($"[GameServer] Opponent found! (Opponent={ waitingPlayerId })");
                    Data.RemoveFromWaitingList(waitingPlayerId);

                    Debug.WriteLine($"[GameServer] Create a game! (Context.ConnectionId={ playerConnectionId }, Opponent={ waitingPlayerId })");
                    long gameId = Data.AddGame(playerConnectionId, waitingPlayerId);
                    GameStarted.Invoke(this, new GameStartedArgs(gameId, playerConnectionId, waitingPlayerId));
                }
                else
                {
                    Debug.WriteLine($"[GameServer] Add to waiting list (Context.ConnectionId={ playerConnectionId })");
                    Data.AddToWaitingList(playerConnectionId);
                }
            }
        }

        public void RemoveFromWaitingList(string playerConnectionId)
        {
            Debug.WriteLine($"[GameServer] RemoveFromWaitingList (Context.ConnectionId={ playerConnectionId })");
            Data.RemoveFromWaitingList(playerConnectionId);
        }

        public void CreateGame(string player1ConnectionId, string player2ConnectionId)
        {
            Debug.WriteLine($"[GameServer] CreateGame (player1={ player1ConnectionId }, player2={ player2ConnectionId })");
            Data.AddGame(player1ConnectionId, player2ConnectionId);
        }

        public long GetGameId(string playerConnectionId)
        {
            return Data.GetGameId(playerConnectionId);
        }

        public void RemoveGame(int gameId)
        {
            Debug.WriteLine($"[GameServer] RemoveGame (gameId={ gameId })");
            Data.RemoveGame(gameId);
        }

        public string GetFirstPlayer(long gameId)
        {
            return Data.GetFirstPlayer(gameId);
        }
    }
}