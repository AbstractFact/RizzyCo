using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RizzyCoBE.Models;

namespace RizzyCoBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private RizzyCoContext rizzyCoContext;

        // Dependency Injection DB Context-a
        public PlayerController(RizzyCoContext context)
        {
            rizzyCoContext = context;
        }

        [HttpGet]
        [Route("GetPlayers")]
        public async Task<JsonResult> GetPlayers()
        {
            var player = await rizzyCoContext.Players
                        .Include(p => p.User)
                        .Include(p => p.PlayerColor)
                        .Include(p => p.Game)
                        .ToListAsync();

            return new JsonResult(player);
        }

        [HttpPost]
        [Route("AddPlayer")]
        public async Task AddPlayer([FromBody]Player player)
        {
            rizzyCoContext.Players.Add(player);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP DELETE
        [HttpDelete]
        [Route("DeletePlayer/{id}")]
        // id se prosleđuje preko URL-a i upisuje u parametar
        public async Task DeletePlayer(int id)
        {
            var player = await rizzyCoContext.Players.FindAsync(id);
            rizzyCoContext.Players.Remove(player);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP PUT
        [HttpPut]
        [Route("ChangePlayer/{id}/{finished}")]
        public async Task ChangePlayer(int id)
        {
            var player = await rizzyCoContext.Players.FindAsync(id);
            //player.Finished = finished;
            rizzyCoContext.Players.Update(player);
            await rizzyCoContext.SaveChangesAsync();
        }
    }

}
