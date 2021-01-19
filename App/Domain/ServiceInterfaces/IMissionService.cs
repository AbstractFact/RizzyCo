using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.ServiceInterfaces
{
    public interface IMissionService : IService<Mission>
    {
        Task<Mission> Post(Mission entity, int mapID);

        Task<List<Mission>> GetMapMissions(int mapID);
    }
}
