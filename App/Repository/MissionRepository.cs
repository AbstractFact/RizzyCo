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
    public class MissionRepository : Repository<Mission, RizzyCoContext>, IMissionRepository
    {
        public MissionRepository(RizzyCoContext context) : base(context)
        {

        }

        public async Task<List<Mission>> GetMapMissions(int mapID)
        {
            return await context.Set<Mission>().Where(t => t.Map.ID == mapID).ToListAsync();
        }

    }
}
