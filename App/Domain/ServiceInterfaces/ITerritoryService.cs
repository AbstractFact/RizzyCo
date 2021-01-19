using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.ServiceInterfaces
{
    public interface ITerritoryService : IService<Territory>
    {
        Task<List<Territory>> GetContinentTerritories(int continentID);
    }
}
