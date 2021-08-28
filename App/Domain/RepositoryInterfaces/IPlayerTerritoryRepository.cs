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
        Task<PlayerTerritory> GetTargetTerritory(int playerID, int terrID, int gameID);
        Task<PlayerTerritory> AddReinforcement(int playerID, int territoryID, int numArmies);
        Task<PlayerTerritory> GetPlayer(int terrID, int gameID);
        Task<int> GetPlayerTerritoriesByColor(string playerColor, int gameID);
    }
}
