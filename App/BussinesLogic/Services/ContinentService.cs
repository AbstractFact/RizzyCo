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
    public class ContinentService : IContinentService
    {
        private readonly IUnitOfWork unit;

        public ContinentService(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public Continent Delete(int id)
        {
            using (unit)
            {
                Continent continent = unit.Continents.Delete(id);

                unit.Complete();

                return continent;
            }
        }

        public async Task<Continent> Get(int id)
        {
            using (unit)
            {
                Task<Continent> continent = unit.Continents.Get(id);

                if (continent == null) return null;

                return await continent;
            }
        }

        public async Task<List<Continent>> GetAll()
        {
            using (unit)
            {
                Task<List<Continent>> continents = unit.Continents.GetAll();

                return await continents;
            }
        }

        public async Task<Continent> Post(Continent entity)
        {
            using (unit)
            {
                Task<Continent> continent = unit.Continents.Add(entity);

                unit.Complete();

                return await continent;
            }
        }

        public async Task<Continent> Post(Continent entity, int mapID)
        {
            using (unit)
            {
                Map map = await unit.Maps.Get(mapID);
                entity.Map = map;
                Task<Continent> continent = unit.Continents.Add(entity);

                unit.Complete();

                return await continent;
            }
        }
        public Continent Put(Continent entity)
        {
            using (unit)
            {
                Continent continent = unit.Continents.Update(entity);

                unit.Complete();

                return continent;
            }
        }

        public async Task<List<Continent>> GetMapContinents(int mapID)
        {
            using (unit)
            {
                Task<List<Continent>> territories = unit.Continents.GetMapContinents(mapID);

                return await territories;
            }
        }
    }
}