using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> GetAllSync();
        Task<User> GetUserByUsername(string username);
    }
}
