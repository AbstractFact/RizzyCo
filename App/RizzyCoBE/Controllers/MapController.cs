using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RizzyCoBE.Models;

namespace RizzyCoBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MapController : ControllerBase
    {
        private RizzyCoContext rizzyCoContext;

        // Dependency Injection DB Context-a
        public MapController(RizzyCoContext context)
        {
            rizzyCoContext = context;
        }

        [HttpGet]
        [Route("GetMaps")]
        public async Task<JsonResult> GetMaps()
        {
            var map = await rizzyCoContext.Maps
                        .Include(m => m.Territories)
                        .Include(m => m.Cards)
                        .Include(m => m.Missions)
                        .ToListAsync();

            return new JsonResult(map);
        }

        [HttpPost]
        [Route("AddMap")]
        public async Task AddMap([FromBody] Map map)
        {
            rizzyCoContext.Maps.Add(map);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP DELETE
        [HttpDelete]
        [Route("DeleteMap/{id}")]
        // id se prosleđuje preko URL-a i upisuje u parametar
        public async Task DeleteMap(int id)
        {
            var map = await rizzyCoContext.Maps.FindAsync(id);
            rizzyCoContext.Maps.Remove(map);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP PUT
        [HttpPut]
        [Route("ChangeMap/{id}/{newDescription}")]
        public async Task ChangeMap(int id, string newName)
        {
            var map = await rizzyCoContext.Maps.FindAsync(id);
            map.Name = newName;
            rizzyCoContext.Maps.Update(map);
            await rizzyCoContext.SaveChangesAsync();
        }
    }
}