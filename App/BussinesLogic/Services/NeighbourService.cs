using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;
using DataAccess.EFCore;

namespace BussinesLogic.Services
{
    public class NeighbourService : INeighbourService
    {
        private readonly RizzyCoContext context;

        public NeighbourService(RizzyCoContext context)
        {
            this.context = context;
        } 
        public async Task<List<Neighbour>> GetAll()
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<List<Neighbour>> neighbours = unit.Neighbours.GetAll();

                return await neighbours;
            }
        }
        public async Task<Neighbour> Get(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Neighbour> neighbour = unit.Neighbours.Get(id);

                if (neighbour == null) return null;

                return await neighbour;
            }
        }

        public async Task<Neighbour> Put(Neighbour entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Neighbour> neighbour = unit.Neighbours.Update(entity);

                unit.Complete();

                return await neighbour;
            }
        }

        public async Task<Neighbour> Post(Neighbour entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Neighbour> neighbour = unit.Neighbours.Add(entity);

                unit.Complete();

                return await neighbour;
            }
        }

        public async Task<Neighbour> Delete(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Neighbour> neighbour = unit.Neighbours.Delete(id);

                unit.Complete();

                return await neighbour;
            }
        }
    }
}
