using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile
{
    public class TouchEventArgs : EventArgs
    {
        public int Column { get; set; }

        public TouchEventArgs(int column)
        {
            Column = column;
        }
    }
}
