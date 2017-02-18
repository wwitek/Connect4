using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Interfaces
{
    public interface IBoard
    {
        int Width { get; }
        int Height { get; }
    }
}