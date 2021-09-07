using Microsoft.AspNetCore.Mvc;

using DataAccess.Models;
using BussinesLogic.Services;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : MyMDBController<Map, MapService>
    {
        public MapController(MapService service) : base(service)
        {

        }   

    }
}
