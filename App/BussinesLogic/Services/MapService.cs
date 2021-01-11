using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;

namespace BussinesLogic.Services
{
    public class MapService : IMapService
    {
        private readonly IUnitOfWork unit;

        public MapService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<List<Map>> GetAll()
        {
            using (unit)
            {
                Task<List<Map>> maps = unit.Maps.GetAll();

                return await maps;
            }
        }
        public async Task<Map> Get(int id)
        {
            using (unit)
            {
                Task<Map> map = unit.Maps.Get(id);

                if (map == null) return null;

                return await map;
            }
        }

        public Map Put(Map entity)
        {
            using (unit)
            {
                Map map = unit.Maps.Update(entity);

                unit.Complete();

                return map;
            }
        }

        public async Task<Map> Post(Map entity)
        {
            using (unit)
            {
                Task<Map> map = unit.Maps.Add(entity);

                unit.Complete();

                return await map;
            }
        }

        public Map Delete(int id)
        {
            using (unit)
            {
                Map map = unit.Maps.Delete(id);

                unit.Complete();

                return  map;
            }
        }
    }
}
