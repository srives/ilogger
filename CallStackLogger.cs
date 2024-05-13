using Microsoft.Extensions.Logging;
using System;

namespace Gtpx.Cloud.Api.Web.Logger
{
    public class CallStackLogger : ILogger
    {
        private readonly CallStackLoggerProvider _providerWithOptions;

        public CallStackLogger(CallStackLoggerProvider logger) 
        {
            _providerWithOptions = logger; 
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
           return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var stackTrace = new System.Diagnostics.StackTrace(1); // skip one frame as this is the Log function frame
            var name = stackTrace.GetFrame(0).GetMethod().Name;
            var message = formatter(state, exception);
            // Console.WriteLine(state.GetType().FullName);  // Keeps showing Microsoft.Extensions.Logging.FormattedLogValues, I thought it would be the type used in ILogger<Type>.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{DateTime.Now} ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{message}");
        }
    }
}
