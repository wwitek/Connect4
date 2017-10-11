using Connect4.Domain.Enums;
using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Interfaces.Factories
{
    public interface IPlayerFactory
    {
        IPlayer Create(PlayerType type, int id, IProxy proxy = null);
    }
}
