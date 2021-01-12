using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BussinesLogic.Services;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : MyMDBController<Game, GameService>
    {
        public GameController(GameService service) : base(service)
        {

        }

        [HttpGet("GetAllGames")]
        public async Task<ActionResult<IEnumerable<Game>>> GetAllUsers()
        {
            List<Game> result = await service.GetAllGames();
            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }
}
