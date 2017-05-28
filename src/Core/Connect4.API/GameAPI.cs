using Connect4.Domain.Enums;
using Connect4.Domain.EventArguments;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.API
{
    public class GameAPI : IGameAPI
    {
        private IGameFactory GameFactory { get; }
        private IPlayerFactory PlayerFactory { get; }
        private IGame CurrentGame { get; set; }

        public event EventHandler<MoveEventArgs> OnMoveMade;

        public GameAPI(IGameFactory gameFactory, IPlayerFactory playerFactory)
        {
            GameFactory = gameFactory;
            PlayerFactory = playerFactory;
        }

        public void Start(GameType gameType)
        {
            List<IPlayer> players = new List<IPlayer>();
            players.Add(PlayerFactory.Create(PlayerType.Local, 1));

            switch (gameType)
            {
                case GameType.SinglePlayer:
                    players.Add(PlayerFactory.Create(PlayerType.Bot, 2));
                    break;
                case GameType.TwoPlayers:
                    players.Add(PlayerFactory.Create(PlayerType.Local, 2));
                    break;
                case GameType.Online:
                    players.Add(PlayerFactory.Create(PlayerType.Online, 2));
                    break;
            }

            CurrentGame = GameFactory.Create(players);
            CurrentGame.OnMoveMade += (s, e) => OnMoveMade?.Invoke(s, e);
        }

        public bool TryMove(int column)
        {
            return CurrentGame.TryMove(column);
        }

        public IPlayer GetCurrentPlayer()
        {
            return CurrentGame?.CurrentPlayer;
        }

        public GameState GetGameState()
        {
            return (CurrentGame != null) ? CurrentGame.State : GameState.New;
        }
    }
}
