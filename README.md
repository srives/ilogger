The idea of CallStackLogger was to get a stack trace on where log messages came from. I didn't solve that completely, but
I wanted to keep this code around because it is usefuel.

In program.cs, add this

                .ConfigureLogging((hostingContext, logger) =>
                {
                    logger.ClearProviders();
                    logger.AddCallStackLogger();
                    // logger.AddApplicationInsightsLogger(hostingContext.Configuration);
                });


In appsettings.json, in Logging, add your Logger information

  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Information", // Debug
      "System": "Information",
      "Microsoft": "Information",
      "Microsoft.AspNetCore.SignalR": "Warning"
    },    
    "CallStack": {
      "LogLevel": {
        "Default": "Error", // Debug
        "System": "Error",
        "Microsoft": "Error",
        "Microsoft.AspNetCore.SignalR": "Error",
        "Gtpx.Cloud.Api.Web.Controllers": "Trace"
      }
    }
  },


Notice, you can just use the regular logger, and just set the LogLevel for the API controllers to Trace, and everything else to Error

