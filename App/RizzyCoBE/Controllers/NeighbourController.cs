﻿using System;
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
    public class NeighbourController : MyMDBController<Neighbour, NeighbourService>
    {
        public NeighbourController(NeighbourService service) : base(service)
        {

        }

        // POST: api/[controller]
        [HttpPost("AddNeighbour/{srcID}/{dstID}")]
        public async Task<ActionResult<Neighbour>> AddNeighbour(int srcID, int dstID)
        {
            Neighbour result = await service.Post(srcID, dstID);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }

        [HttpGet("GetTerritoryNeighbours/{terrID}")]
        public async Task<ActionResult<IEnumerable<Neighbour>>> GetTerritoryNeighbours(int terrID)
        {
            List<Neighbour> result = await service.GetTerritoryNeighbours(terrID);
            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }
}
