using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD10k.Extensions
{
    public static class ILoggerExtensions
    {
        /// <summary>
        /// Encapsulates both the message and the stack trace
        /// of an exception in a logging message.
        /// </summary>
        public static void LogException<T>(this ILogger<T> logger, Exception e)
        {
            var exception = new StringBuilder()
                .Append(e.Message)
                .Append(e.StackTrace)
                .ToString();

            logger.LogError(exception);
        }
    }
}

