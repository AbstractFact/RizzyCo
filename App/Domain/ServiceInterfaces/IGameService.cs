using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DTOs;

namespace Domain.ServiceInterfaces
{
    public interface IGameService : IService<Game>
    {
        Task<List<PlayerTerritoryDTO>> GetGameTerritories(int id);
        Task<Game> NextStage(int gameID);
    }
}
