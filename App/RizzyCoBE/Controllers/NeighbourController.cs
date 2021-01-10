using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.EFCore;
using BussinesLogic.Services;


namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeighbourController : MyMDBController<Neighbour, NeighbourService>
    {
        public NeighbourController(NeighbourService service) : base(service)
        {

        }
    }
}
