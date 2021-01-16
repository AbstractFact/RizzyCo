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
    public class MapRepository : Repository<Map, RizzyCoContext>, IMapRepository
    {
        public MapRepository(RizzyCoContext context) : base(context)
        {

        }

        public async Task<List<Map>> GetAllMaps()
        {
            return await context.Set<Map>().Include(p => p.Continents).ToListAsync();
        }
    }
}
