using Connect4.Domain.Entities.Players;
using Connect4.Domain.Enums;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Domain.AI;

namespace Connect4.Domain.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private IterativeDeepeningSearch InterDeepeningSearch { get; }
        private IProxy Proxy { get; }

        public PlayerFactory(IterativeDeepeningSearch interDeepeningSearch, IProxy proxy = null)
        {
            InterDeepeningSearch = interDeepeningSearch;
            Proxy = proxy;
        }

        public IPlayer Create(PlayerType type, int id)
        {
            switch(type)
            {
                case PlayerType.Local:
                    return new LocalPlayer(id);
                case PlayerType.Bot:
                    return new BotPlayer(id, InterDeepeningSearch);
                case PlayerType.Online:
                    return new OnlinePlayer(id, Proxy);
            }
            return null;
        }
    }
}
