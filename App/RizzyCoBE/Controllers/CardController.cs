using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RizzyCoBE.Models;

namespace RizzyCoBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private RizzyCoContext rizzyCoContext;

        // Dependency Injection DB Context-a
        public CardController(RizzyCoContext context)
        {
            rizzyCoContext = context;
        }

        [HttpGet]
        [Route("GetCards")]
        public async Task<JsonResult> GetCards()
        {
            var card = await rizzyCoContext.Cards
                        .Include(c => c.Territory)
                        .ToListAsync();

            return new JsonResult(card);
        }

        [HttpPost]
        [Route("AddCard")]
        public async Task AddCard([FromBody] Card card)
        {
            rizzyCoContext.Cards.Add(card);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP DELETE
        [HttpDelete]
        [Route("DeleteCard/{id}")]
        // id se prosleđuje preko URL-a i upisuje u parametar
        public async Task DeleteCard(int id)
        {
            var card = await rizzyCoContext.Cards.FindAsync(id);
            rizzyCoContext.Cards.Remove(card);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP PUT
        [HttpPut]
        [Route("ChangeCard/{id}/{newDescription}")]
        public async Task ChangeCard(int id, string newPicture)
        {
            var card = await rizzyCoContext.Cards.FindAsync(id);
            card.Picture = newPicture;
            rizzyCoContext.Cards.Update(card);
            await rizzyCoContext.SaveChangesAsync();
        }
    }
}