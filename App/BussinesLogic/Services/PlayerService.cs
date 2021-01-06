using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;
using DataAccess.EFCore;

namespace BussinesLogic.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly RizzyCoContext context;

        public PlayerService(RizzyCoContext context)
        {
            this.context = context;
        }
        public async Task<List<Player>> GetAll()
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<List<Player>> players = unit.Players.GetAll();

                return await players;
            }
        }
        public async Task<Player> Get(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Player> player = unit.Players.Get(id);

                if (player == null) return null;

                return await player;
            }
        }

        public async Task<Player> Put(Player entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Player> player = unit.Players.Update(entity);

                unit.Complete();

                return await player;
            }
        }

        public async Task<Player> Post(Player entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Player> player = unit.Players.Add(entity);

                unit.Complete();

                return await player;
            }
        }

        public async Task<Player> Delete(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Player> player = unit.Players.Delete(id);

                unit.Complete();

                return await player;
            }
        }
    }
}
