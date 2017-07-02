using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Connect4.Server
{
    public class GameServer
    {
        private static readonly object waitingListLocker = new object();

        public List<int> WaitingList { get; }
        private Dictionary<int, List<int>> CurrentGames { get; }

        public GameServer()
        {
            WaitingList = new List<int>();
            CurrentGames = new Dictionary<int, List<int>>();
        }

        public void AddToWaitingList(int player)
        {
            lock (waitingListLocker)
            {
                WaitingList.Add(player);
            }
        }
    }
}