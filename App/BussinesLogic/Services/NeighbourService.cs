using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<Neighbour> Post(int srcID, int dstID)
        {
            using (unit)
            {
                Territory src = await unit.Territories.Get(srcID);
                Territory dst = await unit.Territories.Get(dstID);

                Neighbour neighbour1 = new Neighbour();
                neighbour1.Src = src;
                neighbour1.Dst = dst;

                await unit.Neighbours.Add(neighbour1);

                Neighbour neighbour2 = new Neighbour();
                neighbour2.Src = dst;
                neighbour2.Dst = src;

                await unit.Neighbours.Add(neighbour2);

                unit.Complete();

                return neighbour2;
            }
        }

        public async Task<List<PlayerTerritoryDTO>> GetTargetTerritories(int playerID, int terrID, int gameID)
        {
            using (unit)
            {
                Territory t = await unit.Territories.Get(terrID);
                List<Neighbour> neighbours = await unit.Neighbours.GetTerritoryNeighbours(t);
                List<PlayerTerritoryDTO> result = new List<PlayerTerritoryDTO>();

                foreach (Neighbour n in neighbours)
                {
                    PlayerTerritory pt = await unit.PlayerTerritories.GetTargetTerritory(playerID, n.Dst.ID, gameID);
                    if (pt != null)
                        result.Add(new PlayerTerritoryDTO(pt));
                };

                return result;
            }
        }

    }
}
