using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.ServiceInterfaces
{
    public interface ICardService : IService<Card>
    {
        Task<Card> AddCard(Card entity, int mapID, int territoryID);
        Task<Card> AddJokerCard(Card entity, int mapID);
    }
}
