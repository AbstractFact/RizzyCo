using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.ServiceInterfaces
{
    public interface IGameService : IService<Game>
    {
        Task<List<Game>> GetAllGames();
    }
}
