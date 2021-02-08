using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using BussinesLogic.Services;
using BussinesLogic.Authentication;
using Microsoft.AspNetCore.Authorization;
using DTOs;
using RizzyCoBE.MessagingService;

namespace RizzyCoBE.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MyMDBController<User, UserService>
    {
        private IUserAuthService _userService;
        private Sender _sender;
        public UserController(UserService service, IUserAuthService userService, Sender sender) : base(service)
        {
            _userService = userService;
            _sender = sender;
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

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("Signup")]
        public async Task<IActionResult> Signup(User entity)
        {

            User result = await this.service.Post(entity);
            if (result != null)
            {
                var user = _userService.Authenticate(result);
                return Ok(user);
            }
            return BadRequest("Bad request!");
        }

        // POST: api/[controller]
        [HttpPost("SendInvitation")]
        public ActionResult SendInvitation([FromBody] TestDTO msg)
        {
            _sender.PushMessageToQ(msg);
            return Ok();
        }
    }
}
