using DataAccess.Models;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface ICardService : IService<Card>
    {
        Task<Card> AddCard(Card entity, int mapID, int territoryID);
        Task<Card> AddJokerCard(Card entity, int mapID);
    }
}
