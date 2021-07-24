using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DTOs;

namespace Domain.RepositoryInterfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<Player> GetPlayer(int id);
        Task<List<Player>> GetPlayers(int gameID);
        Task<Player> GetPlayerInfo(int gameID, int userID);
        Task<Player> UpdateAvailableArmies(int playerID);
    }
}
