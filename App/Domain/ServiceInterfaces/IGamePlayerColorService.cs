using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface IGamePlayerColorService : IService<GamePlayerColor>
    {
        Task<GamePlayerColor> AddGamePlayerColor(int gameID, int playerColorID);
    }
}
