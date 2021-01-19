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
    public class PlayerCardController : MyMDBController<PlayerCard, PlayerCardService>
    {
        public PlayerCardController(PlayerCardService service) : base(service)
        {

        }

        [HttpPost("AddPlayerCard/{playerID}/{cardID}")]
        public async Task<ActionResult<PlayerCard>> AddPlayerCard(int playerID, int cardID)
        {
            PlayerCard result = await service.AddPlayerCard(playerID, cardID);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }
    }
}
