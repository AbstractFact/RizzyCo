using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RizzyCoBE.Models;

namespace RizzyCoBE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private RizzyCoContext rizzyCoContext;

        // Dependency Injection DB Context-a
        public UserController(RizzyCoContext context)
        {
            rizzyCoContext = context;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<JsonResult> GetUsers()
        {
            var user = await rizzyCoContext.Users
                        .Include(u => u.Games)
                        .ToListAsync();

            return new JsonResult(user);
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task AddUser([FromBody] User user)
        {
            rizzyCoContext.Users.Add(user);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP DELETE
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        // id se prosleđuje preko URL-a i upisuje u parametar
        public async Task DeleteUser(int id)
        {
            var user = await rizzyCoContext.Users.FindAsync(id);
            rizzyCoContext.Users.Remove(user);
            await rizzyCoContext.SaveChangesAsync();
        }

        // HTTP PUT
        [HttpPut]
        [Route("ChangeUser/{id}/{newUsername}")]
        public async Task ChangeUser(int id, string newUsername)
        {
            var user = await rizzyCoContext.Users.FindAsync(id);
            user.Username = newUsername;
            rizzyCoContext.Users.Update(user);
            await rizzyCoContext.SaveChangesAsync();
        }
    }
}