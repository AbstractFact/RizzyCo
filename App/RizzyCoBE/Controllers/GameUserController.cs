using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BussinesLogic.Services;
using Domain.ServiceInterfaces;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameUserController : ControllerBase
    {
        private GameUserService service;

        public GameUserController(GameUserService service)
        {
            this.service = service;
        }

        [HttpGet("{userID}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetAllUserGames(int userID)
        {
            List<GameUser> result = await service.GetAllUserGames(userID);
            if (result != null)
                return Ok(result);

            return NotFound();
        }
        [HttpDelete("{id}")]
        public ActionResult<GameUser> Delete(int id)
        {
            var entity = service.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
