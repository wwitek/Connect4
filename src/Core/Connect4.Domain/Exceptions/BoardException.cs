using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Exceptions
{
    public class BoardException : Exception
    {
        internal BoardException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}
