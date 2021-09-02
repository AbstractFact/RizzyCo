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
            PlayerTerritory entity = (await context.Set<PlayerTerritory>().Include(p => p.Territory).Include(p => p.Player).Include(p=>p.Player.User).Include(p=>p.Territory).Where(t => t.Player.ID == playerID && t.Territory.ID == territoryID).ToListAsync()).FirstOrDefault();
            entity.Armies++;

            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<PlayerTerritory> AddReinforcement(int playerID, int territoryID, int numArmies)
        {
            PlayerTerritory entity = (await context.Set<PlayerTerritory>().Include(p => p.Territory).Include(p => p.Player).Include(p => p.Player.User).Include(p => p.Territory).Where(t => t.Player.ID == playerID && t.Territory.ID == territoryID).ToListAsync()).FirstOrDefault();
            entity.Armies+=numArmies;

            context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<List<PlayerTerritory>> GetPlayerTerritories(int playerID)
        {
            return await context.Set<PlayerTerritory>().Where(t => t.Player.ID == playerID).Include(p => p.Territory).Include(p => p.Player.PlayerColor).ToListAsync(); 
        }

        public async Task<PlayerTerritory> GetTargetTerritory(int playerID, int terrID, int gameID)
        {
            return (await context.Set<PlayerTerritory>().Include(p => p.Territory).Include(p => p.Player.PlayerColor).Where(t => t.Territory.ID == terrID && t.Player.ID!=playerID && t.Player.Game.ID == gameID).ToListAsync()).FirstOrDefault();
        }

        public async Task<PlayerTerritory> GetPlayer(int terrID, int gameID)
        {
            PlayerTerritory p = (await context.Set<PlayerTerritory>().Include(p => p.Player).Include(p => p.Player.PlayerColor).Include(p => p.Territory).Include(p => p.Player.User).Where(t => t.Territory.ID == terrID && t.Player.Game.ID == gameID).ToListAsync()).FirstOrDefault();

            return p;
        }

        public async Task<int> GetPlayerTerritoriesByColor(string playerColor, int gameID)
        {
            return (await context.Set<PlayerTerritory>().Where(t => t.Player.PlayerColor.Value == playerColor && t.Player.Game.ID==gameID).Include(p => p.Territory).Include(p => p.Player).ToListAsync()).Count;
        }
    }
}
