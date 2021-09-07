using DataAccess.Models;
using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface IPlayerCardService : IService<PlayerCard>
    {
        Task<PlayerCard> AddPlayerCard(int playerID, int cardID);
        Task<List<GetCardDTO>> GetPlayerCards(int playerID);
        Task<int> UseCards(int card1ID, int cards2ID, int card3ID);
    }
}
