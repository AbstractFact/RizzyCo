using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RizzyCoBE.Models;

namespace RizzyCoBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MissionController : ControllerBase
    {
        private RizzyCoContext rizzyCoContext;

        // Dependency Injection DB Context-a
        public MissionController(RizzyCoContext context)
        {
            rizzyCoContext = context;
        }

        [HttpGet]
        [Route("GetMissions")]
        public async Task<JsonResult> GetMissions()
        {
            var mission = await rizzyCoContext.Missions
                        .ToListAsync();

            return new JsonResult(mission);
        }

        [HttpPost]
        [Route("AddMission")]
        public async Task AddMission([FromBody]Mission mission)
        {
            rizzyCoContext.Missions.Add(mission);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP DELETE
        [HttpDelete]
        [Route("DeleteMission/{id}")]
        // id se prosleđuje preko URL-a i upisuje u parametar
        public async Task DeleteMission(int id)
        {
            var mission = await rizzyCoContext.Missions.FindAsync(id);
            rizzyCoContext.Missions.Remove(mission);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP PUT
        [HttpPut]
        [Route("ChangeMission/{id}/{newDescription}")]
        public async Task ChangeMission(int id, string newDescription)
        {
            var mission = await rizzyCoContext.Missions.FindAsync(id);
            mission.Description = newDescription;
            rizzyCoContext.Missions.Update(mission);
            await rizzyCoContext.SaveChangesAsync();
        }
    }
}