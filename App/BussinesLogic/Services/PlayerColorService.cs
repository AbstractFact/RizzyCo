using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinesLogic.Services
{
    public class PlayerColorService : IPlayerColorService
    {
        private readonly IUnitOfWork unit;

        public PlayerColorService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<List<PlayerColor>> GetAll()
        {
            using (unit)
            {
                List<PlayerColor> playerColors = await unit.PlayerColors.GetAll();

                return playerColors;
            }
        }
        public async Task<PlayerColor> Get(int id)
        {
            using (unit)
            {
                PlayerColor playerColor = await unit.PlayerColors.Get(id);

                if (playerColor == null) return null;

                return playerColor;
            }
        }

        public PlayerColor Put(PlayerColor entity)
        {
            using (unit)
            {
                PlayerColor playerColor = unit.PlayerColors.Update(entity);

                unit.Complete();

                return playerColor;
            }
        }
        public async Task<PlayerColor> Post(PlayerColor entity)
        {
            using (unit)
            {

                PlayerColor playerColor = await unit.PlayerColors.Add(entity);

                unit.Complete();

                return playerColor;
            }
        }

        public PlayerColor Delete(int id)
        {
            using (unit)
            {
                PlayerColor playerColor = unit.PlayerColors.Delete(id);

                unit.Complete();

                return playerColor;
            }
        }
    }
}
