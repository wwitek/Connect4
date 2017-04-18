using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Interfaces
{
    public interface IPlayer
    {
        int Id { get; }
        bool AllowUserInteraction { get; }

        bool InjectMove(int column);
        IMove WaitForMove(IBoard board);
    }
}
