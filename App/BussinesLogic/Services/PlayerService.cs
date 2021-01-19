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
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork unit;

        public PlayerService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<List<Player>> GetAll()
        {
            using (unit)
            {
                Task<List<Player>> players = unit.Players.GetAll();

                return await players;
            }
        }
        public async Task<Player> Get(int id)
        {
            using (unit)
            {
                Task<Player> player = unit.Players.Get(id);

                if (player == null) return null;

                return await player;
            }
        }

        public async Task<Player> GetPlayer(int id)
        {
            using (unit)
            {
                Task<Player> player = unit.Players.GetPlayer(id);

                if (player == null) return null;

                return await player;
            }
        }

        public Player Put(Player entity)
        {
            using (unit)
            {
                Player player = unit.Players.Update(entity);

                unit.Complete();

                return player;
            }
        }

        public async Task<Player> Post(Player entity)
        {
            using (unit)
            {
                Task<Player> player = unit.Players.Add(entity);

                unit.Complete();

                return await player;
            }
        }

        public Player Delete(int id)
        {
            using (unit)
            {
                Player player = unit.Players.Delete(id);

                unit.Complete();

                return player;
            }
        }

     
    }
}
