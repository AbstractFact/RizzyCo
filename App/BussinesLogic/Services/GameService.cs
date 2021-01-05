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
            using (IUnitOfWork uw = new UnitOfWork(context))
            {
                Task<List<Game>> games = uw.Games.GetAll();

                return await games;
            }
        }
    }
}
