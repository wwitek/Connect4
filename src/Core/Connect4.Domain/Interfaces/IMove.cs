using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Interfaces
{
    public interface IMove
    {
        int Row { get; set; }
        int Column { get; set; }
        int PlayerId { get; set; }
    }
}
