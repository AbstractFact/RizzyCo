using DataAccess.Models;
using DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface IPlayerTerritoryService : IService<PlayerTerritory>
    {
        Task<List<PlayerTerritoryDTO>> GetPlayerTerritories(int playerID);
        Task<PlayerTerritory> AddArmie(int gameID, int playerID, int territoryID);
    }
}
