using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;
using DataAccess.EFCore;

namespace BussinesLogic.Services
{
    public class GameService : IGameService
    {
        private readonly RizzyCoContext context;

        public GameService(RizzyCoContext context)
        {
            this.context = context;
        }
        public async Task<List<Game>> GetAll()
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<List<Game>> games = unit.Games.GetAll();

                return await games;
            }
        }
        public async Task<Game> Get(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Game> game = unit.Games.Get(id);

                if (game == null) return null;

                return await game;
            }
        }
        public async Task<Game> Put(Game entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Game> game = unit.Games.Update(entity);

                unit.Complete();

                return await game;
            }
        }
        public async Task<Game> Post(Game entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Game> game = unit.Games.Add(entity);

                unit.Complete();

                return await game;
            }
        }
        public async Task<Game> Delete(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Game> game = unit.Games.Delete(id);

                unit.Complete();

                return await game;
            }
        }
    }
}
