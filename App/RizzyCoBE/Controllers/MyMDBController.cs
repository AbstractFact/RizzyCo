using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using BussinesLogic.Services;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class MyMDBController<TEntity, TService> : ControllerBase
        where TEntity : class, IEntity
        where TService : class, ICardService
    {
        private readonly TService service;

        public MyMDBController(TService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IEntity>>> Get()
        {
            return await service.GetAllCards();
        }

    //    // GET: api/[controller]
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<TEntity>>> Get()
    //    {
    //        return await repository.GetAll();
    //    }

    //    // GET: api/[controller]/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<TEntity>> Get(int id)
    //    {
    //        var movie = await repository.Get(id);
    //        if (movie == null)
    //        {
    //            return NotFound();
    //        }
    //        return movie;
    //    }

    //    // PUT: api/[controller]/5
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> Put(int id, TEntity movie)
    //    {
    //        if (id != movie.ID)
    //        {
    //            return BadRequest();
    //        }
    //        await repository.Update(movie);
    //        return NoContent();
    //    }

    //    // POST: api/[controller]
    //    [HttpPost]
    //    public async Task<ActionResult<TEntity>> Post(TEntity movie)
    //    {
    //        await repository.Add(movie);
    //        return CreatedAtAction("Get", new { id = movie.ID }, movie);
    //    }

    //    // DELETE: api/[controller]/5
    //    [HttpDelete("{id}")]
    //    public async Task<ActionResult<TEntity>> Delete(int id)
    //    {
    //        var movie = await repository.Delete(id);
    //        if (movie == null)
    //        {
    //            return NotFound();
    //        }
    //        return movie;
    //    }

    }
}
