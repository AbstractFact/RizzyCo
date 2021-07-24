using DataAccess;
using DataAccess.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PlayerTerritoryRepository : Repository<PlayerTerritory, RizzyCoContext>, IPlayerTerritoryRepository
    {
        public PlayerTerritoryRepository(RizzyCoContext context) : base(context)
        {

        }

        public async Task<PlayerTerritory> AddArmie(int playerID, int territoryID)
        {
            PlayerTerritory entity = (await context.Set<PlayerTerritory>().Where(t => t.Player.ID == playerID && t.Territory.ID == territoryID).ToListAsync()).FirstOrDefault();
            entity.Armies++;

            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<List<PlayerTerritory>> GetPlayerTerritories(int playerID)
        {
            
            return await context.Set<PlayerTerritory>().Where(t => t.Player.ID == playerID).Include(p => p.Territory).ToListAsync();
            
        }
    }
}
