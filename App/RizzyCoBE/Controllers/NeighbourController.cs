﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DataAccess.Models;
using BussinesLogic.Services;
using DTOs;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeighbourController : MyMDBController<Neighbour, NeighbourService>
    {
        public NeighbourController(NeighbourService service) : base(service)
        {

        }

        [HttpPost("AddNeighbour/{srcID}/{dstID}")]
        public async Task<ActionResult<Neighbour>> AddNeighbour(int srcID, int dstID)
        {
            Neighbour result = await service.Post(srcID, dstID);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }

        [HttpGet("GetTargetTerritories/{playerID}/{terrID}/{gameID}")]
        public async Task<ActionResult<IEnumerable<PlayerTerritoryDTO>>> GetTargetTerritories(int playerID, int terrID, int gameID)
        {
            List<PlayerTerritoryDTO> result = await service.GetTargetTerritories(playerID, terrID, gameID);
            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }
}
