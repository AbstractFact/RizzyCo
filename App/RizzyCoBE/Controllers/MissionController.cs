using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DataAccess.Models;
using BussinesLogic.Services;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : MyMDBController<Mission, MissionService>
    {
        public MissionController(MissionService service) : base(service)
        {

        }

        [HttpPost("AddMission/{mapID}")]
        public async Task<ActionResult<Mission>> AddMission([FromBody] Mission entity, int mapID)
        {
            Mission result = await service.Post(entity, mapID);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }
    }
}
