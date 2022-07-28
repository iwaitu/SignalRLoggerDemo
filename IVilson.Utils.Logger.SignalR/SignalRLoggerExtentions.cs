using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace IVilson.Utils.Logger.SignalR
{
    public static class SignalRLoggerExtentions
    {
        public static IServiceProvider AddSignalRLogger(
        this IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddProvider(new SignalRLoggerProvider(
                    new SignalRLoggerConfiguration
                    {
                        ServiceProvider = serviceProvider,
                        LogLevel = LogLevel.Information,
                        GroupName = "LogMonitor"
                    }));
            return serviceProvider;
        }

        public static IServiceProvider AddSignalRLogger(
        this IServiceProvider serviceProvider, Action<SignalRLoggerConfiguration> configure)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var options = new SignalRLoggerConfiguration();
            configure(options);
            loggerFactory.AddProvider(new SignalRLoggerProvider(options));
            return serviceProvider;
        }
    }

    
}
