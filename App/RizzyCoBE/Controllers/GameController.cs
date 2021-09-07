using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using DataAccess.Models;
using BussinesLogic.Services;
using DTOs;
using BussinesLogic.Messaging;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : MyMDBController<Game, GameService>
    {
        private HubService hub;
        public GameController(GameService service, IHubContext<MessageHub> hubContext) : base(service)
        {
            hub = new HubService(hubContext);
        }

        [HttpGet("GetGameTerritories/{gameID}")]
        public async Task<ActionResult<List<PlayerTerritoryDTO>>> GetGameTerritories(int gameID)
        {
            var result = await service.GetGameTerritories(gameID);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPut("NextStage/{gameID}/{playerID}/{mapID}")]
        public async Task<ActionResult> NextStage(int gameID, int playerID, int mapID)
        {
            var bonus = await service.NextStage(gameID, playerID, mapID);
            if (bonus > -1)
            {
                await hub.NotifyOnGameChanges(gameID, "ReceiveFirstStageDone", bonus);
                return Ok();
            }
                
            return NotFound();
        }

        [HttpGet("EndTurn/{gameID}/{mapID}")]
        public async Task<ActionResult<NextPlayerDTO>> EndTurn(int gameID, int mapID)
        {
            NextPlayerDTO result = await service.EndTurn(gameID, mapID);
            if (result != null)
            {
                await hub.NotifyOnGameChanges(gameID, "NextPlayerTurn", result);
                return Ok(result);
            }

            return NotFound();
        }
    }
}
