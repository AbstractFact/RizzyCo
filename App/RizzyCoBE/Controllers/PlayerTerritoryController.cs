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

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerTerritoryController : MyMDBController<PlayerTerritory, PlayerTerritoryService>
    {
        public PlayerTerritoryController(PlayerTerritoryService service) : base(service)
        {

        }

        [HttpGet("GetPlayerTerritories/{playerID}")]
        public async Task<ActionResult<IEnumerable<PlayerTerritoryDTO>>> GetPlayerTerritories(int playerID)
        {
            List<PlayerTerritoryDTO> result = await service.GetPlayerTerritories(playerID);
            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }
}
