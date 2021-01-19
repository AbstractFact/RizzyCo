using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface IPlayerTerritoryService : IService<PlayerTerritory>
    {
        Task<List<PlayerTerritory>> GetPlayerTerritories(int playerID);
    }
}
