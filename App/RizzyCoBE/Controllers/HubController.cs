using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using RizzyCoBE.Messaging.Hubs;

namespace RizzyCoBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HubController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _hub;
        private readonly IMemoryCache _memoryCache;

        public HubController(IHubContext<MessageHub> hub, IMemoryCache memoryCache)
        {
            _hub = hub;
            _memoryCache = memoryCache;
        }

        //[HttpGet("GetLobbyPlayers")]
        //public List<string> GetLobbyPlayers(string lobbyID)
        //{
        //    //await _chatHub.Clients.All.SendAsync(message);
        //}
    }
}