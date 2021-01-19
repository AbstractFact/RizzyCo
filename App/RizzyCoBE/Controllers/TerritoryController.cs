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
    public class TerritoryController : MyMDBController<Territory, TerritoryService>
    {
        public TerritoryController(TerritoryService service) : base(service)
        {

        }

        // POST: api/[controller]
        [HttpPost("AddTerritory/{continentID}")]
        public async Task<ActionResult<Territory>> AddTerritory([FromBody] Territory entity, int continentID)
        {
            Territory result = await service.Post(entity, continentID);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }

        [HttpGet("GetContinentTerritories/{continentID}")]
        public async Task<ActionResult<IEnumerable<Territory>>> GetContinentTerritories(int continentID)
        {
            List<Territory> result = await service.GetContinentTerritories(continentID);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

     
    }
}
