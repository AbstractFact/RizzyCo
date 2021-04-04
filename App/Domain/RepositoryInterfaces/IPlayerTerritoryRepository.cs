using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IPlayerTerritoryRepository : IRepository<PlayerTerritory>
    {
        Task<List<PlayerTerritory>> GetPlayerTerritories(int playerID);
        Task<PlayerTerritory> AddArmie(int playerID, int territoryID);
    }
}
