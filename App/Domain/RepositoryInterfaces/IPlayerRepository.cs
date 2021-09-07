using DataAccess.Models;
using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.RepositoryInterfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<Player> GetPlayer(int id);
        Task<List<Player>> GetPlayers(int gameID);
        Task<Player> GetPlayerInfo(int gameID, int userID);
        Task<Player> UpdateAvailableArmies(int playerID);
        Task<Player> UpdateAvailableReinforcements(int playerID, int numArmies);
        Task<List<Player>> GetUserPlayers(int userID);
        Task<NextPlayerDTO> EndTurn(int gameID);
        Task<Player> WonCard(int playerID, bool won);
    }
}
