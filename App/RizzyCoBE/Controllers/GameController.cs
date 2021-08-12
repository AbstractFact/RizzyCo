using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BussinesLogic.Services;
using DTOs;
using BussinesLogic.Messaging;
using Microsoft.AspNetCore.SignalR;

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

        [HttpPut("NextStage/{gameID}")]
        public async Task<ActionResult> NextStage(int gameID)
        {
            var result = await service.NextStage(gameID);
            if (result != null)
            {
                await hub.NotifyOnGameChanges(gameID, "ReceiveFirstStageDone", "First phase done");
                return Ok();
            }
                

            return NotFound();
        }
    }
}
