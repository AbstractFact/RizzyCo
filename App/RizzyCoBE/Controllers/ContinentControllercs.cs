using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("AddContinent/{mapID}")]
        public async Task<ActionResult<Continent>> AddContinent([FromBody] Continent entity, int mapID)
        {
            Continent result = await service.Post(entity, mapID);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }

        [HttpGet("GetMapContinents/{mapID}")]
        public async Task<ActionResult<IEnumerable<Continent>>> GetMapContinents(int mapID)
        {
            List<Continent> result = await service.GetMapContinents(mapID);
            if (result != null)
                return Ok(result);

            return NotFound();
        }


    }
}
