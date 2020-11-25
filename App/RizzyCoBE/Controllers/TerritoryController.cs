using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RizzyCoBE.Models;

namespace RizzyCoBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TerritoryController : ControllerBase
    {
        private RizzyCoContext rizzyCoContext;

        // Dependency Injection DB Context-a
        public TerritoryController(RizzyCoContext context)
        {
            rizzyCoContext = context;
        }

        [HttpGet]
        [Route("GetTerritories")]
        public async Task<JsonResult> GetTerritories()
        {
            var territory = await rizzyCoContext.Territories
                        .Include(p => p.Neighbours)
                        .ToListAsync();

            return new JsonResult(territory);
        }

        [HttpPost]
        [Route("AddTerritory")]
        public async Task AddMission([FromBody]Territory territory)
        {
            rizzyCoContext.Territories.Add(territory);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP DELETE
        [HttpDelete]
        [Route("DeleteTerritory/{id}")]
        // id se prosleđuje preko URL-a i upisuje u parametar
        public async Task DeleteTerritory(int id)
        {
            var territory = await rizzyCoContext.Territories.FindAsync(id);
            rizzyCoContext.Territories.Remove(territory);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP PUT
        [HttpPut]
        [Route("ChangeTerritory/{id}/{newName}")]
        public async Task ChangeTerritory(int id, string newName)
        {
            var territory = await rizzyCoContext.Territories.FindAsync(id);
            territory.Name = newName;
            rizzyCoContext.Territories.Update(territory);
            await rizzyCoContext.SaveChangesAsync();
        }
    }
}