using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;
using DataAccess.EFCore;
namespace BussinesLogic.Services
{
    public class TerritoryService : ITerritoryService
    {
        private readonly RizzyCoContext context;

        public TerritoryService(RizzyCoContext context)
        {
            this.context = context;
        }
        public async Task<List<Territory>> GetAll()
        {
            using (IUnitOfWork uw = new UnitOfWork(context))
            {
                Task<List<Territory>> territories = uw.Territories.GetAll();

                return await territories;
            }
        }
    }
}
