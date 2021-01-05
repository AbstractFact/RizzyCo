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
            using (IUnitOfWork uw = new UnitOfWork(context))
            {
                Task<List<Player>> players = uw.Players.GetAll();

                return await players;
            }
        }
    }
}
