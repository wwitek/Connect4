using Connect4.Domain.Enums;
using Connect4.Domain.EventArguments;
using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (Board.IsColumnFull(column)) return false;
            if (!CurrentPlayer.AllowUserInteraction) return false;
            if (State == GameState.Finished || State == GameState.Aborted) return false;
            return true;
        }

        public bool TryMove(int column)
        {
            if (!IsMoveValid(column)) return false;
            IPlayer player = Players.FirstOrDefault(p => p.Id == CurrentPlayer.Id);
            if (player != null)
            {
                player.InjectMove(column);
            }
            return true;
        }
    }
}