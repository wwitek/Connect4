using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Enums
{
    public enum GameState
    {
        New,
        Ready,
        Running,
        Finished,
        Aborted
    }
}
