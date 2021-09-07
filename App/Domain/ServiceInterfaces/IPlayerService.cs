using DataAccess.Models;
using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.ServiceInterfaces
{
    public interface IPlayerService : IService<Player>
    {
        Task<Player> GetPlayer(int id);
        Task<List<Player>> GetPlayers(int id);
        Task<PlayerInfoDTO> GetPlayerInfo(int gameID, int userID);
        Task<List<GameInfoDTO>> GetUserGames(int userID);
        Task<bool> FullWaitingLobby(List<string> playersJoined, int gameID);
    }
}
