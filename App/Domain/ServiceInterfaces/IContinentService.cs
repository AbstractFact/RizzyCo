using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.ServiceInterfaces
{
    public interface IContinentService : IService<Continent>
    {
        Task<Continent> Post(Continent entity, int mapID);
        Task<List<Continent>> GetMapContinents(int mapID);
    }
}
