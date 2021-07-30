using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DTOs;

namespace Domain.ServiceInterfaces
{
    public interface IPlayerService : IService<Player>
    {
        Task<Player> GetPlayer(int id);
        Task<List<Player>> GetPlayers(int id);
        Task<PlayerInfoDTO> GetPlayerInfo(int gameID, int userID);
        Task<List<GameInfoDTO>> GetUserGames(int userID);
    }
}
