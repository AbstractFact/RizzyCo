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
    public class PlayerCardRepository : Repository<PlayerCard, RizzyCoContext>, IPlayerCardRepository
    {
        public PlayerCardRepository(RizzyCoContext context) : base(context)
        {

        }

        public async Task<List<PlayerCard>> GetPlayerCards(int playerID)
        {
            return await context.Set<PlayerCard>().Where(t => t.Player.ID == playerID).Include(t=>t.Card).ToListAsync();
        }
    }
}
