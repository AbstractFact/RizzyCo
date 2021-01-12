using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> GetAllSync();
        Task<List<User>> GetAllUsers();
    }
}
