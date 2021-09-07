using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface ITerritoryService : IService<Territory>
    {
        Task<List<Territory>> GetContinentTerritories(int continentID);
    }
}
