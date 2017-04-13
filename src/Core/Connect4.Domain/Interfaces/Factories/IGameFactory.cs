﻿using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Interfaces.Factories
{
    public interface IGameFactory
    {
        IGame Create(IList<IPlayer> players);
    }
}
