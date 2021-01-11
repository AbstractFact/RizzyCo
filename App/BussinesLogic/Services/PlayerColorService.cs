using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;

namespace BussinesLogic.Services
{
    public class PlayerColorService : IPlayerColorService
    {
        private readonly IUnitOfWork unit;

        public PlayerColorService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<List<PlayerColor>> GetAll()
        {
            using (unit)
            {
                Task<List<PlayerColor>> playerColors = unit.PlayerColors.GetAll();

                return await playerColors;
            }
        }
        public async Task<PlayerColor> Get(int id)
        {
            using (unit)
            {
                Task<PlayerColor> playerColor = unit.PlayerColors.Get(id);

                if (playerColor == null) return null;

                return await playerColor;
            }
        }

        public PlayerColor Put(PlayerColor entity)
        {
            using (unit)
            {
                PlayerColor playerColor = unit.PlayerColors.Update(entity);

                unit.Complete();

                return playerColor;
            }
        }
        public async Task<PlayerColor> Post(PlayerColor entity)
        {
            using (unit)
            {
                Task<PlayerColor> playerColor = unit.PlayerColors.Add(entity);

                unit.Complete();

                return await playerColor;
            }
        }

        public PlayerColor Delete(int id)
        {
            using (unit)
            {
                PlayerColor playerColor = unit.PlayerColors.Delete(id);

                unit.Complete();

                return playerColor;
            }
        }
    }
}
