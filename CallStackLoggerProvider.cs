using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Gtpx.Cloud.Api.Web.Logger
{

    [ProviderAlias("CallStack")]
    public class CallStackLoggerProvider : ILoggerProvider
    {
        
        public CallStackLoggerProvider()
        {
 
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CallStackLogger(this);
        }

        public void Dispose()
        {
            
        }
    }
}
