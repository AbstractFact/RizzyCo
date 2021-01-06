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
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<List<Territory>> territories = unit.Territories.GetAll();

                return await territories;
            }
        }
        public async Task<Territory> Get(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Territory> territory = unit.Territories.Get(id);

                if (territory == null) return null;

                return await territory;
            }
        }

        public async Task<Territory> Put(Territory entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Territory> territory = unit.Territories.Update(entity);

                unit.Complete();

                return await territory;
            }
        }

        public async Task<Territory> Post(Territory entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Territory> territory = unit.Territories.Add(entity);

                unit.Complete();

                return await territory;
            }
        }

        public async Task<Territory> Delete(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Territory> territory = unit.Territories.Delete(id);

                unit.Complete();

                return await territory;
            }
        }
    }
}
