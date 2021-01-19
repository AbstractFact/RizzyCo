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
    public class CardController : MyMDBController<Card, CardService>
    {
        public CardController(CardService service) : base(service)
        {

        }

        [HttpPost("AddCard/{mapID}/{territoryID}")]
        public async Task<ActionResult<Card>> AddCard([FromBody] Card entity, int mapID, int territoryID)
        {
            Card result = await service.AddCard(entity, mapID, territoryID);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }

        [HttpPost("AddJokerCard/{mapID}")]
        public async Task<ActionResult<Card>> AddJokerCard([FromBody] Card entity, int mapID)
        {
            Card result = await service.AddJokerCard(entity, mapID);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }
    }
}
