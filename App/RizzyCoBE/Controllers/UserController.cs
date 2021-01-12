using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BussinesLogic.Services;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MyMDBController<User, UserService>
    {
        public UserController(UserService service) : base(service)
        {
           
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            List<User> result = await service.GetAllUsers();
            if (result != null)
                return Ok(result);

            return NotFound();
        }


        // POST: api/[controller]
        [HttpPost("CreateGame/{userId}/{numPlayers}")]
        public async Task<ActionResult> CreateGame(int userId, int numPlayers)
        {
            User result = await this.service.CreateGame(userId, numPlayers);

            if (result != null)
                return Ok();

            return BadRequest("Bad request!");
        }
    }
}
