using Connect4.Server.EventArguments;
using Connect4.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Connect4.Server.Data
{
    public class DataProvider : IDataProvider
    {
        private long gameId = 0;
        private readonly object waitingListLocker = new object();
        private readonly object gameListLocker = new object();

        public List<string> WaitingList { get; }
        public Dictionary<long, List<string>> CurrentGames { get; }
        
        public DataProvider()
        {
            WaitingList = new List<string>();
            CurrentGames = new Dictionary<long, List<string>>();
        }

        public bool IsInWaitingList(string playerConnectionId)
        {
            lock (waitingListLocker)
            {
                return WaitingList.Contains(playerConnectionId);
            }
        }

        public void AddToWaitingList(string playerConnectionId)
        {
            lock (waitingListLocker)
            {
                if (!WaitingList.Contains(playerConnectionId))
                {
                    WaitingList.Add(playerConnectionId);
                }
            }
        }

        public void RemoveFromWaitingList(string playerConnectionId)
        {
            lock (waitingListLocker)
            {
                if (WaitingList.Contains(playerConnectionId))
                {
                    WaitingList.Remove(playerConnectionId);
                }
            }
        }

        public string GetWaitingPlayer()
        {
            lock (waitingListLocker)
            {
                return (WaitingList.Count >= 1) ? WaitingList[0] : null;
            }
        }

        public long AddGame(string player1ConnectionId, string player2ConnectionId)
        {
            Random random = new Random();
            double randomDouble = random.NextDouble();

            lock (gameListLocker)
            {
                List<string> playerList = (randomDouble > 0.5)
                    ? new List<string>() { player1ConnectionId, player2ConnectionId }
                    : new List<string>() { player2ConnectionId, player1ConnectionId };

                CurrentGames.Add(++gameId, playerList);
                return gameId;
            }
        }

        public void RemoveGame(long gameId)
        {
            lock (gameListLocker)
            {
                CurrentGames.Remove(gameId);
            }
        }

        public long GetGameId(string playerConnectionId)
        {
            lock (gameListLocker)
            {
                var gameId = CurrentGames.Where(l => l.Value.Contains(playerConnectionId)).FirstOrDefault().Key;
                return gameId;
            }
        }

        public string GetFirstPlayer(long gameId)
        {
            lock (gameListLocker)
            {
                return CurrentGames[gameId][0];
            }
        }
    }
}