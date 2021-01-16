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
    public class NeighbourRepository : Repository<Neighbour, RizzyCoContext>, INeighbourRepository
    {
        public NeighbourRepository(RizzyCoContext context) : base(context)
        {

        }
        
        public async Task<List<Neighbour>> GetTerritoryNeighbours(Territory terr)
        {
            return await context.Set<Neighbour>().Include(p => p.Src).Include(p => p.Dst).Where(p => p.Src == terr).ToListAsync();
        }
    }
}
