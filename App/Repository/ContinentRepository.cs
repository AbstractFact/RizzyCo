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
    public class ContinentRepository : Repository<Continent, RizzyCoContext>, IContinentRepository
    {
        public ContinentRepository(RizzyCoContext context) : base(context)
        {

        }
        public async Task<List<Continent>> GetMapContinents(int mapID)
        {
            return await context.Set<Continent>().Where(t => t.Map.ID == mapID).ToListAsync();
        }
    }
}
