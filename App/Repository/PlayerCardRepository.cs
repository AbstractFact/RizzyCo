using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain.RepositoryInterfaces;

namespace Repository
{
    public class PlayerCardRepository : Repository<PlayerCard, RizzyCoContext>, IPlayerCardRepository
    {
        public PlayerCardRepository(RizzyCoContext context) : base(context)
        {

        }

        public async Task<List<PlayerCard>> GetPlayerCards(int playerID)
        {
            return await context.Set<PlayerCard>().Where(t => t.Player.ID == playerID).Include(t=>t.Card).Include(t => t.Card.Territory).Include(t => t.Player).ToListAsync();
        }

        public async Task<List<PlayerCard>> GetAvailableCards(int gameID)
        {
            return await context.Set<PlayerCard>().Where(t => t.Player == null && t.GameID==gameID).Include(t => t.Card).Include(t => t.Card.Territory).ToListAsync();
        }

        public async Task<PlayerCard> GetCard(int playerCardID)
        {
            return (await context.Set<PlayerCard>().Where(t => t.ID == playerCardID).Include(t => t.Card).Include(t => t.Card.Territory).Include(t => t.Player).ToListAsync()).FirstOrDefault();
        }

    }
}
