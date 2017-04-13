using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Logger
{
    public static class LoggerExtensions
    {
        public static void Debug(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Debug, message));
        }

        public static void Debug(this ILogger logger, string message, params object[] args)
        {
            logger.Log(new LogEntry(LoggingEventType.Debug, string.Format(message, args)));
        }

        public static void Info(this ILogger logger, string message)
        {
            logger.Log(new LogEntry(LoggingEventType.Info, message));
        }

        public static void Info(this ILogger logger, string message, params object[] args)
        {
            logger.Log(new LogEntry(LoggingEventType.Info, string.Format(message, args)));
        }

        public static void Warning(this ILogger logger, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Warning, exception.Message, exception));
        }

        public static void Warning(this ILogger logger, string message, Exception exception = null)
        {
            logger.Log(new LogEntry(LoggingEventType.Warning, message, exception));
        }

        public static void Warning(this ILogger logger, string message, Exception exception = null, params object[] args)
        {
            logger.Log(new LogEntry(LoggingEventType.Warning, string.Format(message, args), exception));
        }

        public static void Error(this ILogger logger, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Error, exception.Message, exception));
        }

        public static void Error(this ILogger logger, string message, Exception exception = null)
        {
            logger.Log(new LogEntry(LoggingEventType.Error, message, exception));
        }

        public static void Fatal(this ILogger logger, Exception exception)
        {
            logger.Log(new LogEntry(LoggingEventType.Fatal, exception.Message, exception));
        }

        public static void Fatal(this ILogger logger, string message, Exception exception = null)
        {
            logger.Log(new LogEntry(LoggingEventType.Fatal, message, exception));
        }
    }
}
