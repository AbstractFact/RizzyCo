using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RizzyCoBE.Models;

namespace RizzyCoBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerColorController : ControllerBase
    {
        private RizzyCoContext rizzyCoContext;

        // Dependency Injection DB Context-a
        public PlayerColorController(RizzyCoContext context)
        {
            rizzyCoContext = context;
        }

        [HttpGet]
        [Route("GetPlayerColors")]
        public async Task<JsonResult> GetPlayerColors()
        {
            var mission = await rizzyCoContext.PlayerColors
                        .ToListAsync();

            return new JsonResult(mission);
        }

        [HttpPost]
        [Route("AddPlayerColor")]
        public async Task AddPlayerColor([FromBody]PlayerColor playerColor)
        {
            rizzyCoContext.PlayerColors.Add(playerColor);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP DELETE
        [HttpDelete]
        [Route("DeletePlayerColor/{id}")]
        // id se prosleđuje preko URL-a i upisuje u parametar
        public async Task DeletePlayerColor(int id)
        {
            var playerColor = await rizzyCoContext.PlayerColors.FindAsync(id);
            rizzyCoContext.PlayerColors.Remove(playerColor);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP PUT
        [HttpPut]
        [Route("ChangePlayerColor/{id}/{newColor}")]
        public async Task ChangeMission(int id, string newDescription)
        {
            var playerColor = await rizzyCoContext.PlayerColors.FindAsync(id);
            playerColor.Color = newColor;
            rizzyCoContext.PlayerColors.Update(playerColor);
            await rizzyCoContext.SaveChangesAsync();
        }
    }
}