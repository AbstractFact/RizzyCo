using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<List<Card>> GetMapCards(int mapID);
    }
}
