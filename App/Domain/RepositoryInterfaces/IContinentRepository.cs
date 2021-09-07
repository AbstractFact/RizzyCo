using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IContinentRepository : IRepository<Continent>
    {
        Task<List<Continent>> GetMapContinents(int mapID);
    }
}
