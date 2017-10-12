using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.EventArguments
{
    public class OnlineMoveReceivedArgs : EventArgs
    {
        public int Column { get; }

        public OnlineMoveReceivedArgs(int column)
        {
            Column = column;
        }
    }
}