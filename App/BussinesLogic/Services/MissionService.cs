using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;

namespace BussinesLogic.Services
{
    public class MissionService : IMissionService
    {
        private readonly IUnitOfWork unit;

        public MissionService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<List<Mission>> GetAll()
        {
            using (unit)
            {
                Task<List<Mission>> missions = unit.Missions.GetAll();

                return await missions;
            }
        }
        public async Task<Mission> Get(int id)
        {
            using (unit)
            {
                Task<Mission> mission = unit.Missions.Get(id);

                if (mission == null) return null;

                return await mission;
            }
        }

        public Mission Put(Mission entity)
        {
            using (unit)
            {
                Mission mission = unit.Missions.Update(entity);

                unit.Complete();

                return mission;
            }
        }

        public async Task<Mission> Post(Mission entity)
        {
            using (unit)
            {
                Task<Mission> mission = unit.Missions.Add(entity);

                unit.Complete();

                return await mission;
            }
        }

        public Mission Delete(int id)
        {
            using (unit)
            {
                Mission mission = unit.Missions.Delete(id);

                unit.Complete();

                return mission;
            }
        }
    }
}
