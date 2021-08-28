﻿using System;
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
        Task<int> NextStage(int gameID, int playerID, int mapID);
        Task<int> CalculateBonusArmies(int playerID, int mapID);
        Task<NextPlayerDTO> EndTurn(int gameID, int mapID);
    }
}
