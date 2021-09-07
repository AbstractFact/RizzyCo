using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinesLogic.Services
{
    public class TerritoryService : ITerritoryService
    {
        private readonly IUnitOfWork unit;

        public TerritoryService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<List<Territory>> GetAll()
        {
            using (unit)
            {
                Task<List<Territory>> territories = unit.Territories.GetAll();

                return await territories;
            }
        }
        public async Task<Territory> Get(int id)
        {
            using (unit)
            {
                Task<Territory> territory = unit.Territories.Get(id);

                if (territory == null) return null;

                return await territory;
            }
        }

        public Territory Put(Territory entity)
        {
            using (unit)
            {
                Territory territory = unit.Territories.Update(entity);

                unit.Complete();

                return territory;
            }
        }

        public async Task<Territory> Post(Territory entity)
        {
            using (unit)
            {
                Task<Territory> territory = unit.Territories.Add(entity);

                unit.Complete();

                return await territory;
            }
        }

        public Territory Delete(int id)
        {
            using (unit)
            {
                Territory territory = unit.Territories.Delete(id);

                unit.Complete();

                return territory;
            }
        }

        public async Task<Territory> Post(Territory entity, int continentID)
        {
            using (unit)
            {
                Continent continent = await unit.Continents.Get(continentID);
                entity.Continent = continent;
                Task<Territory> territory = unit.Territories.Add(entity);

                unit.Complete();

                return await territory;
            }
        }

        public async Task<List<Territory>> GetContinentTerritories(int continentID)
        {
            using (unit)
            {
                Task<List<Territory>> territories = unit.Territories.GetContinentTerritories(continentID);

                return await territories;
            }
        }
    }
}
