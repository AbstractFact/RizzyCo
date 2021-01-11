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
    public class NeighbourService : INeighbourService
    {
        private readonly IUnitOfWork unit;

        public NeighbourService(IUnitOfWork unit)
        {
            this.unit = unit;
        } 
        public async Task<List<Neighbour>> GetAll()
        {
            using (unit)
            {
                Task<List<Neighbour>> neighbours = unit.Neighbours.GetAll();

                return await neighbours;
            }
        }
        public async Task<Neighbour> Get(int id)
        {
            using (unit)
            {
                Task<Neighbour> neighbour = unit.Neighbours.Get(id);

                if (neighbour == null) return null;

                return await neighbour;
            }
        }

        public Neighbour Put(Neighbour entity)
        {
            using (unit)
            {
                Neighbour neighbour = unit.Neighbours.Update(entity);

                unit.Complete();

                return neighbour;
            }
        }

        public async Task<Neighbour> Post(Neighbour entity)
        {
            using (unit)
            {
                Task<Neighbour> neighbour = unit.Neighbours.Add(entity);

                unit.Complete();

                return await neighbour;
            }
        }

        public Neighbour Delete(int id)
        {
            using (unit)
            {
                Neighbour neighbour = unit.Neighbours.Delete(id);

                unit.Complete();

                return neighbour;
            }
        }
    }
}
