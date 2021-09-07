using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface INeighbourRepository : IRepository<Neighbour>
    {
        Task<List<Neighbour>> GetTerritoryNeighbours(Territory terr);
    }
}
