using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;
using DataAccess.EFCore;

namespace BussinesLogic.Services
{
    public class MapService : IMapService
    {
        private readonly RizzyCoContext context;

        public MapService(RizzyCoContext context)
        {
            this.context = context;
        }
        public async Task<List<Map>> GetAll()
        {
            using (IUnitOfWork uw = new UnitOfWork(context))
            {
                Task<List<Map>> maps = uw.Maps.GetAll();

                return await maps;
            }
        }
    }
}
