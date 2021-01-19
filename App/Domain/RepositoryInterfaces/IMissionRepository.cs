using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IMissionRepository : IRepository<Mission>
    {
        Task<List<Mission>> GetMapMissions(int mapID);
    }
}
