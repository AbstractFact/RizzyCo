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
    public class PlayerRepository : Repository<Player, RizzyCoContext>, IPlayerRepository
    {
        public PlayerRepository(RizzyCoContext context) : base(context)
        {

        }
        public async Task<Player> GetPlayer(int id)
        {
            return (await context.Set<Player>().Where(p=>p.ID==id).Include(p=>p.User).Include(p => p.Game).Include(p => p.PlayerColor).Include(p => p.Mission).ToListAsync()).FirstOrDefault();
        }

        public async Task<Player> GetPlayerInfo(int gameID, int userID)
        {
            return (await context.Set<Player>().Where(p => p.Game.ID == gameID).Where(p => p.User.ID == userID).Include(p => p.PlayerColor).Include(p => p.Mission).ToListAsync()).FirstOrDefault();  
        }

        public async Task<List<Player>> GetPlayers(int gameID)
        {
            return await context.Set<Player>().Where(p => p.Game.ID == gameID).Include(p => p.User).Include(p => p.PlayerColor).Include(p => p.Mission).ToListAsync();
        }

        public async Task<List<Player>> GetUserPlayers(int userID)
        {
            return await context.Set<Player>().Where(p => p.User.ID == userID).Include(p => p.Game).OrderBy(p => p.Game.Finished).ThenByDescending(p => p.Game.CreationDate).ToListAsync();
        }

        public async Task<Player> UpdateAvailableArmies(int playerID)
        {
            Player tmp = await context.Players.FindAsync(playerID);
            tmp.AvailableArmies--;
            context.Update(tmp);
            return tmp;
        }

        public async Task<string> EndTurn(int gameID)
        {
           
            List<Player> players = await GetPlayers(gameID);
            string result = "";
            players.ForEach(el =>
            {
                el.OnTurn--;
                if (el.OnTurn < 0)
                {
                    el.OnTurn = players.Count - 1;
                    context.Update(el);
                }

                if (el.OnTurn == 0)
                    result = el.User.Username;
            });

            return result;
            
        }
    }
}
