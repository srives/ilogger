using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gtpx.Cloud.Api.Web.Logger
{
    public static class CallStackLoggerExtensions
    {        
        static public ILoggingBuilder AddCallStackLogger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, CallStackLoggerProvider>();
            return builder;
        }        
    }
}
