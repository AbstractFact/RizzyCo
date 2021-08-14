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
        private readonly PlayerService playerService;
        public PlayerTerritoryController(PlayerTerritoryService service, PlayerService playerSevice, IHubContext<MessageHub> hubContext) : base(service)
        {
            hub = new HubService(hubContext);
            this.playerService = playerSevice;
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
            AddArmieDTO result = await service.AddArmie(gameID, playerID, territoryID);
            if (result != null)
            {
                await hub.NotifyOnGameChanges(gameID, "PlayerAddArmie", result);
                return Ok();
            }
                
            return NotFound();
        }

        [HttpPost("AddReinforcement")]
        public async Task<ActionResult> AddReinforcement(AddReinforcementDTO dto)
        {
            AddArmieDTO result = await service.AddReinforcement(dto);
            if (result != null)
            {
                await hub.NotifyOnGameChanges(dto.GameID, "PlayerAddReinforcement", result);
                return Ok();
            }

            return NotFound();
        }
    }
}
