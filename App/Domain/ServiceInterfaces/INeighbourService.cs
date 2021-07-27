using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DTOs;

namespace Domain.ServiceInterfaces
{
    public interface INeighbourService : IService<Neighbour>
    {
        Task<Neighbour> Post(int scrID, int dstID);
        Task<List<PlayerTerritoryDTO>> GetTargetTerritories(int playerID, int terrID, int gameID);
    }
}
