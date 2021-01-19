using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.RepositoryInterfaces
{
    public interface ITerritoryRepository : IRepository<Territory>
    {
        Task<List<Territory>> GetContinentTerritories(int continentID);
    }
}
