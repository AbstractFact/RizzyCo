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
    public class ContinentController : MyMDBController<Continent, ContinentService>
    {
        public ContinentController(ContinentService service) : base(service)
        {
           

        }

        // POST: api/[controller]
        [HttpPost("AddContinent/{mapID}")]
        public async Task<ActionResult<Continent>> AddContinent([FromBody] Continent entity, int mapID)
        {
            Continent result = await service.Post(entity, mapID);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }

        [HttpGet("GetAllContinents")]
        public async Task<ActionResult<IEnumerable<Continent>>> GetAllContinents()
        {
            List<Continent> result = await service.GetAllContinents();
            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }
}
