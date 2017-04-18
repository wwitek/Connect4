﻿using Connect4.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Entities.Players
{
    public class LocalPlayer : IPlayer
    {
        private BlockingCollection<int> InjectedMoves { get; }

        public int Id { get; }
        public bool AllowUserInteraction { get; }

        public LocalPlayer(int id)
        {
            Id = id;
            AllowUserInteraction = true;
            InjectedMoves = new BlockingCollection<int>(1);
        }

        public bool InjectMove(int column)
        {
            return InjectedMoves.TryAdd(column);
        }

        public IMove WaitForMove(IBoard board)
        {
            int injectedColumn = InjectedMoves.Take();

            int row = board.GetLowestEmptyRow(injectedColumn);
            board.InsertChip(row, injectedColumn, Id);

            return new Move(row, injectedColumn, Id);
        }
    }
}