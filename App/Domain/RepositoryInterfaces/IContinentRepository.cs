using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IContinentRepository : IRepository<Continent>
    {
        Task<List<Continent>> GetMapContinents(int mapID);
    }
}
