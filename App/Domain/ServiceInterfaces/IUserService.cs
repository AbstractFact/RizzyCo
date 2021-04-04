using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.ServiceInterfaces
{
    public interface IUserService : IService<User>
    {
        Task<int> CreateGame(List<string> users, int mapID);
    }
}
