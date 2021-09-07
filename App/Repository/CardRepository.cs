using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CardRepository : Repository<Card, RizzyCoContext>, ICardRepository
    {
        public CardRepository(RizzyCoContext context) : base(context)
        {

        }
        public async Task<List<Card>> GetMapCards(int mapID)
        {
            return await context.Set<Card>().Where(c => c.Map.ID == mapID).Include(c => c.Map).ToListAsync();
        }
    }
}
