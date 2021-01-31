using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.ServiceInterfaces
{
    public interface IPlayerService : IService<Player>
    {
        Task<Player> GetPlayer(int id);
        Task<List<Player>> GetPlayers(int id);


    }
}
