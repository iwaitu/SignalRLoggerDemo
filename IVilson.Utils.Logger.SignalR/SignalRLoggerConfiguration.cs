using Microsoft.Extensions.Logging;

namespace IVilson.Utils.Logger.SignalR
{
    public class SignalRLoggerConfiguration
    {
        
        public IServiceProvider ServiceProvider { get; set; }
        public string GroupName { get; set; } = "LogMonitor";
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        public int EventId { get; set; } = 0;
    }
}
