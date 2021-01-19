using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface IPlayerCardService : IService<PlayerCard>
    {
        Task<PlayerCard> AddPlayerCard(int playerID, int cardID);
        Task<List<PlayerCard>> GetPlayerCards(int playerID);
    }
}
