using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;

namespace Domain.ServiceInterfaces
{
    public interface IGameUserService
    {
        public Task<List<GameUser>> GetAllUserGames(int userID);
    }
}
