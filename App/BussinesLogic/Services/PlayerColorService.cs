using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using DTOs;

namespace BussinesLogic.Services
{
    public class PlayerColorService : IPlayerColorService
    {
        private readonly IUnitOfWork unit;

        public PlayerColorService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<List<PlayerColorDTO>> GetAll()
        {
            using (unit)
            {
                List<PlayerColor> playerColors = await unit.PlayerColors.GetAll();

                return PlayerColorDTO.FromEntityList(playerColors);
                
            }
        }
        public async Task<PlayerColorDTO> Get(int id)
        {
            using (unit)
            {
                PlayerColor playerColor = await unit.PlayerColors.Get(id);

                if (playerColor == null) return null;

                return new PlayerColorDTO(playerColor);
            }
        }

        public PlayerColorDTO Put(PlayerColorDTO entity)
        {
            using (unit)
            {
                PlayerColor playerColor = unit.PlayerColors.Update(PlayerColorDTO.FromDTO(entity));

                unit.Complete();

                return new PlayerColorDTO(playerColor);
            }
        }
        public async Task<PlayerColorDTO> Post(PlayerColorDTO entity)
        {
            using (unit)
            {

                PlayerColor playerColor = await unit.PlayerColors.Add(PlayerColorDTO.FromDTO(entity));

                unit.Complete();

                return  new PlayerColorDTO(playerColor);
            }
        }

        public PlayerColorDTO Delete(int id)
        {
            using (unit)
            {
                PlayerColor playerColor = unit.PlayerColors.Delete(id);

                unit.Complete();

                return new PlayerColorDTO(playerColor);
            }
        }
    }
}
