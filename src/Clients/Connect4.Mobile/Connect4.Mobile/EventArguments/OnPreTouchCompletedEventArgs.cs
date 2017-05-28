using Connect4.Mobile.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile.EventArguments
{
    public class OnPreTouchCompletedEventArgs : EventArgs
    {
        public PlayerColor Player;
        public int Column;
    }
}
