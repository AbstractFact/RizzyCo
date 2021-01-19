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
    }
}
