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
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<List<Mission>> missions = unit.Missions.GetAll();

                return await missions;
            }
        }
        public async Task<Mission> Get(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Mission> mission = unit.Missions.Get(id);

                if (mission == null) return null;

                return await mission;
            }
        }

        public async Task<Mission> Put(Mission entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Mission> mission = unit.Missions.Update(entity);

                unit.Complete();

                return await mission;
            }
        }

        public async Task<Mission> Post(Mission entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Mission> mission = unit.Missions.Add(entity);

                unit.Complete();

                return await mission;
            }
        }

        public async Task<Mission> Delete(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Mission> mission = unit.Missions.Delete(id);

                unit.Complete();

                return await mission;
            }
        }
    }
}
