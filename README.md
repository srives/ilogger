SSR May 12 2024
The idea of CallStackLogger was to get a stack trace on where log messages came from. I didn't solve that completely, but
I wanted to keep this code around because it is useful.

------------------------------------------------------------------
Step 1: Microsoft has defined ILoggingBuilder in the same
        namespace in two DLLs, and so we specify which to use:
------------------------------------------------------------------
Update .csproj file and add this:
		<PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="8.0.0.0">
			<Aliases>ILoggingBuilder</Aliases>
		</PackageReference>
	</ItemGroup>

------------------------------------------------------------------
Step 2: Copy files from this GitHub to a directory called 
        Logging
------------------------------------------------------------------

------------------------------------------------------------------
Step 3: Update program.cs to use the new logger
------------------------------------------------------------------
In program.cs, add this

using Gtpx.Cloud.Api.Web.Logger;

using Microsoft.Extensions.Logging;

                .ConfigureLogging((hostingContext, logger) =>
                {
                    logger.ClearProviders();
                    logger.AddCallStackLogger();
                    // logger.AddApplicationInsightsLogger(hostingContext.Configuration);
                });


------------------------------------------------------------------
Step 4: Update your appsettings.json
------------------------------------------------------------------
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


Notice, instead of using this repo and my code,
you can use the regular logger and just set the LogLevel for the API controllers to Trace, and everything else to Error

------------------------------------------------------------------
Step 5: Add logging functions (this is for me, general public
        won't be debugging what I have, but I wanted to
        keep track of my Start() and Stop() functions.
------------------------------------------------------------------


        Dictionary<string, Stopwatch> _time = new Dictionary<string, Stopwatch>();
        private void Stop(string uniqueThrottleKey, string why, bool logIfNoTimerFond)
        {
            if (_time.ContainsKey(uniqueThrottleKey))
            {
                _time[uniqueThrottleKey].Stop();
                Logger.LogTrace($"{uniqueThrottleKey} Stopped. Time Ran: {_time[uniqueThrottleKey].ElapsedMilliseconds}ms. {why}");
            }
            else if (!string.IsNullOrEmpty(why) && logIfNoTimerFond)
            {
                Logger.LogTrace($"{uniqueThrottleKey} Stopped. No Timer Found. {why}");
            }
        }

        private void Start(string uniqueThrottleKey)
        {
            Stop(uniqueThrottleKey, "Startting", false); // in case it alread started.
            _time[uniqueThrottleKey] = new Stopwatch();
            _time[uniqueThrottleKey].Start();
            Logger.LogTrace("Started Deashboard Report" + uniqueThrottleKey);
        }

            Start(uniqueThrottleKey);
            Stop(uniqueThrottleKey, "  THROTTLED", true);
            Stop(uniqueThrottleKey, " REPORT GENERATED ", true);
