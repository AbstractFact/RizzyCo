using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using DataAccess.Models;
using BussinesLogic.Services;
using BussinesLogic.Authentication;
using DTOs;

namespace RizzyCoBE.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MyMDBController<User, UserService>
    {
        private IUserAuthService _userService;
        public UserController(UserService service, IUserAuthService userService) : base(service)
        {
            _userService = userService;
        }

        [HttpPost("CreateGame")]
        public async Task<ActionResult<int>> CreateGame([FromBody] CreateGameDTO entity)
        {
            int result =  await this.service.CreateGame(entity.Users, entity.MapID);

            if (result != -1)
                return Ok(result);

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
    }
}
