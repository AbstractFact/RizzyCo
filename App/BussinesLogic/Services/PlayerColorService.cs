using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;
using DataAccess.EFCore;

namespace BussinesLogic.Services
{
    public class PlayerColorService : IPlayerColorService
    {
        private readonly RizzyCoContext context;

        public PlayerColorService(RizzyCoContext context)
        {
            this.context = context;
        }
        public async Task<List<PlayerColor>> GetAll()
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<List<PlayerColor>> playerColors = unit.PlayerColors.GetAll();

                return await playerColors;
            }
        }
        public async Task<PlayerColor> Get(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<PlayerColor> playerColor = unit.PlayerColors.Get(id);

                if (playerColor == null) return null;

                return await playerColor;
            }
        }

        public async Task<PlayerColor> Put(PlayerColor entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<PlayerColor> playerColor = unit.PlayerColors.Update(entity);

                unit.Complete();

                return await playerColor;
            }
        }
        public async Task<PlayerColor> Post(PlayerColor entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<PlayerColor> playerColor = unit.PlayerColors.Add(entity);

                unit.Complete();

                return await playerColor;
            }
        }

        public async Task<PlayerColor> Delete(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<PlayerColor> playerColor = unit.PlayerColors.Delete(id);

                unit.Complete();

                return await playerColor;
            }
        }
    }
}
