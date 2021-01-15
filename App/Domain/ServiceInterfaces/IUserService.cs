using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.ServiceInterfaces
{
    public interface IUserService : IService<User>
    {
        Task<User> CreateGame(List<string> players, int userId, int numPlayers, int mapID);
    }
}
