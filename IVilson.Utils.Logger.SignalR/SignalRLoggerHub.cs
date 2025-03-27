using Microsoft.AspNetCore.SignalR;
using System;

namespace IVilson.Utils.Logger.SignalR
{
    public class SignalRLoggerHub : Hub
    {
        public const string HubUrl = "/loghub";
        private readonly SignalRLoggerConfiguration _config;
        public SignalRLoggerHub(SignalRLoggerConfiguration config) {
            _config = config;
        }
        public async Task Broadcast(string username, string message) => await Clients.All.SendAsync("Broadcast", username, message);
        public async Task JoinGroup(string groupName)
        {
            Console.WriteLine($"{Context.ConnectionId} join {groupName}");
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            Clients.Group(_config.GroupName).SendAsync("Broadcast", $"logger ready");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}
