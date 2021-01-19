using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;

namespace BussinesLogic.Services
{
    public class PlayerTerritoryService : IPlayerTerritoryService
    {
        private readonly IUnitOfWork unit;

        public PlayerTerritoryService(IUnitOfWork unit)
        {
            this.unit = unit;
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

        public async Task<List<PlayerTerritory>> GetPlayerTerritories(int playerID)
        {
            using (unit)
            {
                Task<List<PlayerTerritory>> territories = unit.PlayerTerritories.GetPlayerTerritories(playerID);

                return await territories;
            }
        }
    }
}
