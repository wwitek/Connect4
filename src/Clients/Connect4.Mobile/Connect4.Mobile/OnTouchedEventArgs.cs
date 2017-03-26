using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile
{
    public class OnTouchedEventArgs : EventArgs
    {
        public int Column { get; set; }

        public OnTouchedEventArgs(int column)
        {
            Column = column;
        }
    }
}
