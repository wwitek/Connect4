using Connect4.Domain.Enums;
using Connect4.Domain.EventArguments;
using Connect4.Domain.Exceptions;
using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Connect4.Domain.Entities
{
    public class Game : IGame
    {
        private IBoard Board { get; }
        private List<IPlayer> Players { get; }
        private GameState State { get; set; }
        private IPlayer CurrentPlayer { get; set; }

        public event EventHandler<MoveEventArgs> OnMoveMade;

        public Game(IBoard board, List<IPlayer> players)
        {
            State = GameState.New;
            Board = board;
            Players = players;

            CurrentPlayer = players[0];
            StartGame();
        }

        private async void StartGame()
        {
            await Task.Factory.StartNew(() =>
            {
                while (State != GameState.Finished && State != GameState.Aborted)
                {
                    foreach (IPlayer player in Players)
                    {
                        CurrentPlayer = player;
                        IMove move = player.WaitForMove(Board);
                        OnMoveMade?.Invoke(this, new MoveEventArgs(move));

                        State = GameState.Running;
                        if (move.IsWinner || move.IsDraw)
                        {
                            State = GameState.Finished;
                            break;
                        }
                    }
                }
            });
        }

        private bool IsMoveValid(int column)
        {
            if (Board == null) throw new GameException("Board cannot be null. Cannot performe IsMoveValid method");
            if (CurrentPlayer == null) throw new GameException("CurrentPlayer cannot be null. Cannot performe IsMoveValid method");
            if (CurrentPlayer.Id == 0) throw new GameException("CurrentPlayer.Id cannot be 0. Cannot performe IsMoveValid method");

            if (Board == null || Board.IsColumnFull(column)) return false;
            if (CurrentPlayer == null || CurrentPlayer.Id == 0 || !CurrentPlayer.AllowUserInteraction) return false;
            if (State == GameState.Finished || State == GameState.Aborted) return false;
            return true;
        }

        public bool TryMove(int column)
        {
            bool result = false;
            if (IsMoveValid(column))
            {
                IPlayer player = Players.FirstOrDefault(p => p.Id == CurrentPlayer.Id);
                if (player != null)
                {
                    result = player.InjectMove(column);
                }
            }
            return result;
        }
    }
}