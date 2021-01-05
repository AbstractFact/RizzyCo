using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;
using DataAccess.EFCore;

namespace BussinesLogic.Services
{
    public class MissionService : IMissionService
    {
        private readonly RizzyCoContext context;

        public MissionService(RizzyCoContext context)
        {
            this.context = context;
        }
        public async Task<List<Mission>> GetAll()
        {
            using (IUnitOfWork uw = new UnitOfWork(context))
            {
                Task<List<Mission>> missions = uw.Missions.GetAll();

                return await missions;
            }
        }
    }
}
