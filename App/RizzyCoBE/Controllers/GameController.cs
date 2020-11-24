using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RizzyCoBE.Models;

namespace RizzyCoBE.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class GameController : ControllerBase
        {
            private RizzyCoContext rizzyCoContext;

            // Dependency Injection DB Context-a
            public GameController(RizzyCoContext context)
            {
                rizzyCoContext = context;
            }

            [HttpGet]
            [Route("GetGames")]
            public async Task<JsonResult> GetGames()
            {
                var game = await rizzyCoContext.Games
                            .Include(p => p.Players)
                            .Include(p => p.Map)
                            .Include(p => p.PlayerColors)
                            .ToListAsync();

                return new JsonResult(game);
            }

            [HttpPost]
            [Route("AddGame")]
            public async Task AddGame([FromBody]Game game)
            {
                rizzyCoContext.Games.Add(game);
                await rizzyCoContext.SaveChangesAsync();
            }

            // HTTP DELETE
            [HttpDelete]
            [Route("DeleteGame/{id}")]
            // id se prosleđuje preko URL-a i upisuje u parametar
            public async Task DeleteGame(int id)
            {
                var game = await rizzyCoContext.Games.FindAsync(id);
                rizzyCoContext.Games.Remove(game);
                await rizzyCoContext.SaveChangesAsync();
            }

            // HTTP PUT
            [HttpPut]
            [Route("ChangeGame/{id}/{finished}")]
            public async Task ChangeGame(int id, bool finished)
            {
                var game = await rizzyCoContext.Games.FindAsync(id);
                game.Finished = finished;
                rizzyCoContext.Games.Update(game);
                await rizzyCoContext.SaveChangesAsync();
            }
        }
    
}
