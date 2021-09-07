using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DataAccess.Models;
using BussinesLogic.Services;
using DTOs;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerCardController : MyMDBController<PlayerCard, PlayerCardService>
    {
        public PlayerCardController(PlayerCardService service) : base(service)
        {

        }

        [HttpPost("AddPlayerCard/{playerID}/{cardID}")]
        public async Task<ActionResult<PlayerCard>> AddPlayerCard(int playerID, int cardID)
        {
            PlayerCard result = await service.AddPlayerCard(playerID, cardID);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }

        [HttpGet("GetPlayerCards/{playerID}")]
        public async Task<ActionResult<List<GetCardDTO>>> GetPlayerCards(int playerID)
        {
            List<GetCardDTO> result = await service.GetPlayerCards(playerID);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }

        [HttpPost("UseCards/{card1ID}/{card2ID}/{card3ID}")]
        public async Task<ActionResult<int>> UseCards(int card1ID, int card2ID, int card3ID)
        {
            int bonus = await service.UseCards(card1ID, card2ID, card3ID);
            if (bonus != -1)
                return Ok(bonus);

            return BadRequest("Bad request!");
        }
    }
}
