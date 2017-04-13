using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Interfaces.Factories
{
    public interface IBoardFactory
    {
        IBoard Create(int width, int height);
    }
}
