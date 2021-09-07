using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IMissionRepository : IRepository<Mission>
    {
        Task<List<Mission>> GetMapMissions(int mapID);
    }
}
