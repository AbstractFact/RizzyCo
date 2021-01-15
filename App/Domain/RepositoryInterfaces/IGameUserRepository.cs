using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IGameUserRepository : IRepository<GameUser>
    {
        Task<List<GameUser>> GetAllUserGames(User user);
        Task<GameUser> AddGameUser(Game game, User user);
    }
}
