using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.EventArguments
{
    public class OnlineGameStartedArgs : EventArgs
    {
        public bool GoesFirst { get; }

        public OnlineGameStartedArgs(bool goesFirst)
        {
            GoesFirst = goesFirst;
        }
    }
}