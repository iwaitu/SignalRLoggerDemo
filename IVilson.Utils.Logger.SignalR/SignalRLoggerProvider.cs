using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace IVilson.Utils.Logger.SignalR
{
    public class SignalRLoggerProvider : ILoggerProvider
    {
        private readonly SignalRLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, SignalRLogger> _loggers = new ConcurrentDictionary<string, SignalRLogger>();

        public SignalRLoggerProvider(SignalRLoggerConfiguration config)
        {
            _config = config;
        }
            

        public ILogger CreateLogger(string categoryName)
            => this._loggers.GetOrAdd(categoryName, name => new SignalRLogger(name, this._config));

        public void Dispose()
            => this._loggers.Clear();
    }
}
