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
    public class PlayerController : MyMDBController<Player, PlayerService>
    {
        public PlayerController(PlayerService service) : base(service)
        {

        }

        [HttpGet("GetPlayer/{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var result = await service.GetPlayer(id);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("GetPlayers/{gameID}")]
        public async Task<ActionResult<List<Player>>> GetPlayers(int gameID)
        {
            var result = await service.GetPlayers(gameID);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("GetPlayerInfo/{gameID}/{userID}")]
        public async Task<ActionResult<PlayerInfoDTO>> GetPlayerInfo(int gameID, int userID)
        {
            var result = await service.GetPlayerInfo(gameID, userID);
            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }
}
