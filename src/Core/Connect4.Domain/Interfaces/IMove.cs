using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Interfaces
{
    public interface IMove
    {
        int Row { get; }
        int Column { get; }
        int PlayerId { get; }

        bool IsWinner { get; }
        bool IsDraw { get; }
    }
}
