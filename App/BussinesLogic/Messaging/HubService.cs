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

        public async Task<int> GameStartedAsync(CreateGameMsgDTO msg)
        {
            await _hub.Clients.Group("Game" + msg.GameID).SendAsync("JoinGameGroup", msg);
            return msg.GameID;
        }

    }
}