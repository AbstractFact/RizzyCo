using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface IUserService : IService<User>
    {
        Task<int> CreateGame(List<string> users, int mapID);
    }
}
