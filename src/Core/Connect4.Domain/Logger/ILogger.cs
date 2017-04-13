using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Logger
{
    public interface ILogger
    {
        void Log(LogEntry entry);
    }
}
