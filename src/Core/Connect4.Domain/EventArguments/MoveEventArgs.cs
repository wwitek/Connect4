using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.EventArguments
{
    public class MoveEventArgs : EventArgs
    {
        public IMove Move { get; }

        public MoveEventArgs(IMove move)
        {
            Move = move;
        }
    }
}
