using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using DTOs;

namespace BussinesLogic.Services
{
    public class MapService : IMapService
    {
        private readonly IUnitOfWork unit;

        public MapService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<List<MapDTO>> GetAll()
        {
            using (unit)
            {
                List<Map> maps = await unit.Maps.GetAll();

                return MapDTO.FromEntityList(maps);

            }
        }
        public async Task<MapDTO> Get(int id)
        {
            using (unit)
            {
                Map map = await unit.Maps.Get(id);

                if (map == null) return null;

                return new MapDTO(map);
            }
        }

        public MapDTO Put(MapDTO entity)
        {
            using (unit)
            {
                Map map = unit.Maps.Update(MapDTO.FromDTO(entity));

                unit.Complete();

                return new MapDTO(map);

            }
        }

        public async Task<MapDTO> Post(MapDTO entity)
        {
            using (unit)
            {
                Map map = await unit.Maps.Add(MapDTO.FromDTO(entity));

                unit.Complete();

                return new MapDTO(map);
   
            }
        }

        public MapDTO Delete(int id)
        {
            using (unit)
            {
                Map map = unit.Maps.Delete(id);

                unit.Complete();

                return new MapDTO(map);
 
            }
        }

      

    }
}
