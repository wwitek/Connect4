using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Exceptions
{
    public class GameException : Exception
    {
        internal GameException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
