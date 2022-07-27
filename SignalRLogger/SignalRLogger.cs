﻿using Microsoft.AspNetCore.SignalR;

namespace SignalRLoggerDemo
{
    public class SignalRLogger : ILogger
    {
        private readonly string _name;
        private readonly SignalRLoggerConfiguration _config;
        public SignalRLogger(string name, SignalRLoggerConfiguration config)
        {
            this._name = name;
            this._config = config;
            
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel == this._config.LogLevel;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
                        Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (this._config.EventId == 0 || this._config.EventId == eventId.Id)
            {
                try
                {
                    var hub = _config.ServiceProvider.GetRequiredService<IHubContext<LoggerHub>>();
                    //this._config.HubContext?.Clients.Group(_config.GroupName).SendAsync("Broadcast",  $"{DateTimeOffset.UtcNow:T}-UTC : {formatter(state, exception)}");
                    hub?.Clients.Group(_config.GroupName).SendAsync("Broadcast", $"{DateTimeOffset.UtcNow:T}-UTC : {formatter(state, exception)}");
                }
                catch (Exception e){
                    // todo
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
