﻿using Connect4.Domain.Entities.Players;
using Connect4.Domain.Enums;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        public IPlayer Create(PlayerType type, int id)
        {


            return new LocalPlayer(id);
        }
    }
}
