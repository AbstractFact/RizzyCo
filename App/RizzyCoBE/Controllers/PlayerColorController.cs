using Microsoft.AspNetCore.Mvc;

using DataAccess.Models;
using BussinesLogic.Services;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerColorController : MyMDBController<PlayerColor, PlayerColorService>
    {
        public PlayerColorController(PlayerColorService service) : base(service)
        {

        }
    }
}
