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
    public class PlayerTerritoryController : MyMDBController<PlayerTerritory, PlayerTerritoryService>
    {
        private HubService hub;
        public PlayerTerritoryController(PlayerTerritoryService service, IHubContext<MessageHub> hubContext) : base(service)
        {
            hub = new HubService(hubContext);
        }

        [HttpGet("GetPlayerTerritories/{playerID}")]
        public async Task<ActionResult<IEnumerable<PlayerTerritoryDTO>>> GetPlayerTerritories(int playerID)
        {
            List<PlayerTerritoryDTO> result = await service.GetPlayerTerritories(playerID);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost("AddArmie/{gameID}/{playerID}/{territoryID}")]
        public async Task<ActionResult> AddArmie(int gameID, int playerID, int territoryID)
        {
            PlayerTerritory result = await service.AddArmie(gameID, playerID, territoryID);
            if (result != null)
            {
                await hub.NotifyOnGameChanges(gameID, "PlayerAddArmie", "Armie added");
                return Ok();
            }
                
            return NotFound();
        }
    }
}
