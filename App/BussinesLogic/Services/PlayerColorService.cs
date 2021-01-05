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
            using (IUnitOfWork uw = new UnitOfWork(context))
            {
                Task<List<PlayerColor>> playerColors = uw.PlayerColors.GetAll();

                return await playerColors;
            }
        }
    }
}
