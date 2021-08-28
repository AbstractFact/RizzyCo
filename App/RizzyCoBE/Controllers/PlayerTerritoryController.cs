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

        [HttpPost("Attack")]
        public async Task<ActionResult> Attack(AttackDTO dto)
        {
            AttackInfoDTO result = await service.Attack(dto);
            if (result != null)
            {
                await hub.NotifyOnGameChanges(dto.GameID, "PlayerAttacked", result);
                return Ok();
            }

            return NotFound();
        }

        [HttpPost("ThrowDice")]
        public async Task<ActionResult> ThrowDice(ThrowDiceDTO dto)
        {
            ThrowDiceNotificationDTO result = await service.ThrowDice(dto);
            if (result!=null)
            {
                await hub.NotifyOnGameChanges(dto.GameID, "PlayerDefended", result);
                return Ok();
            }

            return NotFound();
        }

        [HttpPost("Transfer")]
        public async Task<ActionResult> Transfer(TransferArmiesDTO dto)
        {
            TransferArmiesDTO result = await service.Transfer(dto);
            if (result != null)
            {
                await hub.NotifyOnGameChanges(dto.GameID, "PlayerTransferedArmies", result);
                return Ok();
            }

            return NotFound();
        }
    }
}
