using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class TerritoryRepository : Repository<Territory, RizzyCoContext>, ITerritoryRepository
    {
        public TerritoryRepository(RizzyCoContext context) : base(context)
        {

        }

        public async Task<List<Territory>> GetContinentTerritories(int continentID)
        {
            return await context.Set<Territory>().Where(t=>t.Continent.ID==continentID).ToListAsync();
        }

        public async Task<List<Territory>> GetContinentTerritoriesByName(string continent)
        {
            return await context.Set<Territory>().Where(t => t.Continent.Name == continent).ToListAsync();
        }

    }
}
