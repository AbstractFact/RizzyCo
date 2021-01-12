using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.ServiceInterfaces
{
    public interface IUserService : IService<User>
    {
        Task<User> CreateGame(int userId, int numPlayers);
        Task<List<User>> GetAllUsers();
    }
}
