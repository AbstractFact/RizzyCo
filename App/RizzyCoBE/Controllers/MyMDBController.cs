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
        where TService : class, IService<TEntity>
    {
        private readonly TService service;

        public MyMDBController(TService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IEntity>>> Get()
        {
            return await service.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var entity = await service.Get(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity entity)
        {
            if (id != entity.ID)
            {
                return BadRequest();
            }
            await service.Put(entity);

            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            await service.Post(entity);
            return CreatedAtAction("Get", new { id = entity.ID }, entity);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var entity = await service.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }
    }
}
