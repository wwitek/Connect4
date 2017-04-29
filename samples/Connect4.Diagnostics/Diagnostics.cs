using Connect4.Domain.Entities;
using Connect4.Domain.Entities.Players;
using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connect4.Diagnostics
{
    public class Diagnostics
    {
        private static readonly object testLocker = new object();

        public IGame CreateGame(int[,] ids)
        {
            IField[,] fields = new IField[ids.GetLength(0), ids.GetLength(1)];
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    fields[i, j] = new Field(i, j);
                    fields[i, j].PlayerId = ids[i, j];
                }
            }
            IBoard board = new Board(fields);
            List<IPlayer> players = new List<IPlayer>();
            players.Add(new LocalPlayer(1));
            players.Add(new BotPlayer(2));
            IGame game = new Game(board, players);
            return game;
        }

        public long Test(int[,] fields, int nextPlayerMove)
        {
            lock (testLocker)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Reset();

                AutoResetEvent MoveMadeSignal = new AutoResetEvent(false);

                IGame game = CreateGame(fields);
                game.OnMoveMade += (s, e) =>
                {
                    string time = "";
                    if (e.Move.PlayerId == 1) stopwatch.Start();
                    if (e.Move.PlayerId == 2)
                    {
                        stopwatch.Stop();
                        time = $"(time: { stopwatch.ElapsedMilliseconds }ms)";
                        Console.WriteLine($"Moved: { e.Move.Column } { time }");
                        MoveMadeSignal.Set();
                    }
                };
                game.TryMove(nextPlayerMove);
                MoveMadeSignal.WaitOne();
                return stopwatch.ElapsedMilliseconds;
            }
        }
    }
}
