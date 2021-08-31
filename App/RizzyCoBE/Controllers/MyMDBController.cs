using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.ServiceInterfaces;

namespace RizzyCoBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class MyMDBController<TEntity, TService> : ControllerBase
        where TEntity : class
        where TService : class, IService<TEntity>
    {
        protected readonly TService service;

        public MyMDBController(TService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            List<TEntity> result = await service.GetAll();
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var result = await service.Get(id);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPut]
        public IActionResult Put(TEntity entity)
        {
            TEntity result =  service.Put(entity);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            TEntity result = await service.Post(entity);
            if (result != null)
                return Ok(result);

            return BadRequest("Bad request!");
        }

        [HttpDelete("{id}")]
        public ActionResult<TEntity> Delete(int id)
        {
            var entity = service.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
