using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BussinesLogic.Messaging;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using DTOs;
using Microsoft.AspNetCore.SignalR;

namespace BussinesLogic.Services
{
    public class PlayerTerritoryService : IPlayerTerritoryService
    {
        private readonly IUnitOfWork unit;
        private HubService hubService;

        public PlayerTerritoryService(IUnitOfWork unit, IHubContext<MessageHub> hubContext)
        {
            this.unit = unit;
            hubService = new HubService(hubContext);
        }

        public PlayerTerritory Delete(int id)
        {
            using (unit)
            {
                PlayerTerritory playerTerritory = unit.PlayerTerritories.Delete(id);

                unit.Complete();

                return playerTerritory;
            }
        }

        public async Task<PlayerTerritory> Get(int id)
        {
            using (unit)
            {
                Task<PlayerTerritory> playerTerritory = unit.PlayerTerritories.Get(id);

                if (playerTerritory == null) return null;

                return await playerTerritory;
            }
        }

        public async Task<List<PlayerTerritory>> GetAll()
        {
            using (unit)
            {
                Task<List<PlayerTerritory>> playerTerritories = unit.PlayerTerritories.GetAll();

                return await playerTerritories;
            }
        }

        public async Task<PlayerTerritory> Post(PlayerTerritory entity)
        {
            using (unit)
            {
                Task<PlayerTerritory> playerTerritory = unit.PlayerTerritories.Add(entity);

                unit.Complete();

                return await playerTerritory;
            }
        }

        public PlayerTerritory Put(PlayerTerritory entity)
        {
            using (unit)
            {
                PlayerTerritory playerTerritory = unit.PlayerTerritories.Update(entity);

                unit.Complete();

                return playerTerritory;
            }
        }

        public async Task<List<PlayerTerritoryDTO>> GetPlayerTerritories(int playerID)
        {
            using (unit)
            {
                List<PlayerTerritory> territories = await unit.PlayerTerritories.GetPlayerTerritories(playerID);
                List<PlayerTerritoryDTO> territoriesDTO = new List<PlayerTerritoryDTO>();

                territories.ForEach(element =>
                {
                    territoriesDTO.Add(new PlayerTerritoryDTO(element));

                });

                return territoriesDTO;
            }
        }

        public async Task<PlayerTerritory> AddArmie(int gameID, int playerID, int territoryID)
        {
            using (unit)
            {
                PlayerTerritory playerTerritory = await unit.PlayerTerritories.AddArmie(playerID, territoryID);
                await unit.Players.UpdateAvailableArmies(playerID);
                unit.Complete();

                return playerTerritory;
            }
        }
    }
}
