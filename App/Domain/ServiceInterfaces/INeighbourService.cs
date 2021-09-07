using DataAccess.Models;
using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface INeighbourService : IService<Neighbour>
    {
        Task<Neighbour> Post(int scrID, int dstID);
        Task<List<PlayerTerritoryDTO>> GetTargetTerritories(int playerID, int terrID, int gameID);
    }
}
