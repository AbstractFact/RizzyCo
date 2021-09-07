using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                List<Map> maps = await unit.Maps.GetAll();

                return maps;
            }
        }
        public async Task<Map> Get(int id)
        {
            using (unit)
            {
                Map map = await unit.Maps.Get(id);

                if (map == null) return null;

                return map;
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
                Map map = await unit.Maps.Add(entity);

                unit.Complete();

                return map;
            }
        }

        public Map Delete(int id)
        {
            using (unit)
            {
                Map map = unit.Maps.Delete(id);

                unit.Complete();

                return map;
            }
        }
    }
}
