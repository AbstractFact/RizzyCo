using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BussinesLogic.Services;
using Domain.ServiceInterfaces;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePlayerColorController : MyMDBController<GamePlayerColor, GamePlayerColorService>
    {
        public GamePlayerColorController(GamePlayerColorService service) : base(service)
        {

        }


        // POST: api/[controller]
        [HttpPost("AddGamePlayerColor/{gameID}/{playerColorID}")]
        public async Task<ActionResult<GamePlayerColor>> AddGamePlayerColor(int gameID, int playerColorID)
        {
            GamePlayerColor result = await service.AddGamePlayerColor(gameID, playerColorID);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }

    }
}
