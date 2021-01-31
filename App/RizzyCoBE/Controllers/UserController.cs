using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BussinesLogic.Services;
using DTOs;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MyMDBController<UserDTO, UserService>
    {
        public UserController(UserService service) : base(service)
        {
           
        }

        // POST: api/[controller]
        [HttpPost("CreateGame/{creatorID}/{mapID}")]
        public async Task<ActionResult> CreateGame([FromBody] List<string> users, int creatorID, int mapID)
        {
            User result =  await this.service.CreateGame(users, creatorID, mapID);

            if (result != null)
                return Ok();

            return BadRequest("Bad request!");
        }
    }
}
