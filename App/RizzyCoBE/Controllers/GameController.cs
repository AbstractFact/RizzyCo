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
    public class GameController : MyMDBController<Game, GameService>
    {
        public GameController(GameService service) : base(service)
        {
            
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
            var result = await service.NextStage(gameID, playerID, mapID);
            if (result != null)
                return Ok();

            return NotFound();
        }
    }
}
