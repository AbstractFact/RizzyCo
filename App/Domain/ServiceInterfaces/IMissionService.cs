using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface IMissionService : IService<Mission>
    {
        Task<Mission> Post(Mission entity, int mapID);

        Task<List<Mission>> GetMapMissions(int mapID);
    }
}
