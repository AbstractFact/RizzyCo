using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.ServiceInterfaces
{
    public interface INeighbourService : IService<Neighbour>
    {
        Task<Neighbour> Post(int scrID, int dstID);
        Task<List<Neighbour>> GetTerritoryNeighbours(int terrID);
    }
}
