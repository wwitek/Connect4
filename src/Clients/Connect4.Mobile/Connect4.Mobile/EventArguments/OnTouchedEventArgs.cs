using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile.EventArguments
{
    public class OnTouchedEventArgs : EventArgs
    {
        public object Column { get; }

        public OnTouchedEventArgs(int column)
        {
            Column = column;
        }
    }
}
