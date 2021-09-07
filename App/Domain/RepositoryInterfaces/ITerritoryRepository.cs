using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface ITerritoryRepository : IRepository<Territory>
    {
        Task<List<Territory>> GetContinentTerritories(int continentID);
        Task<List<Territory>> GetContinentTerritoriesByName(string continent);
    }
}
