using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using Microsoft.AspNetCore.SignalR;

namespace BussinesLogic.Messaging
{
    public class HubService 
    {
        private readonly IHubContext<MessageHub> _hub;

        public HubService(IHubContext<MessageHub> hub)
        {
            _hub = hub;
        }

        public async Task<string> NotifyOnGameChanges(int gameID, string method, Object object_to_send)
        {
            await _hub.Clients.Group("Game" + gameID).SendAsync(method, object_to_send);
            return "Game changed";
        }

    }
}