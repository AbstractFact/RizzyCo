using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;
using DataAccess.EFCore;

namespace BussinesLogic.Services
{
    public class MapService : IMapService
    {
        private readonly RizzyCoContext context;

        public MapService(RizzyCoContext context)
        {
            this.context = context;
        }
        public async Task<List<Map>> GetAll()
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<List<Map>> maps = unit.Maps.GetAll();

                return await maps;
            }
        }
        public async Task<Map> Get(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Map> map = unit.Maps.Get(id);

                if (map == null) return null;

                return await map;
            }
        }

        public async Task<Map> Put(Map entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Map> map = unit.Maps.Update(entity);

                unit.Complete();

                return await map;
            }
        }

        public async Task<Map> Post(Map entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Map> map = unit.Maps.Add(entity);

                unit.Complete();

                return await map;
            }
        }

        public async Task<Map> Delete(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Map> map = unit.Maps.Delete(id);

                unit.Complete();

                return await map;
            }
        }
    }
}
