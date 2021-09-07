using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface IContinentService : IService<Continent>
    {
        Task<Continent> Post(Continent entity, int mapID);
        Task<List<Continent>> GetMapContinents(int mapID);
    }
}
