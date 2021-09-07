using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IPlayerCardRepository : IRepository<PlayerCard>
    {
        Task<List<PlayerCard>> GetPlayerCards(int playerID);
        Task<List<PlayerCard>> GetAvailableCards(int gameID);
        Task<PlayerCard> GetCard(int playerCardID);
    }
}
