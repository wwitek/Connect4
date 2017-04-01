using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile.EventArguments
{
    public class OnMoveCompletedEventArgs : EventArgs
    {
        public int MoveId;
        public int Column;
        public int Row;
    }
}
