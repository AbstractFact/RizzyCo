using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BussinesLogic.Messaging.Sender;
using DataAccess;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;

namespace BussinesLogic.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork unit;
       

        public GameService(IUnitOfWork unit)
        { 
            this.unit = unit;
        }
        public async Task<List<Game>> GetAll()
        {
            using (unit)
            {
                Task<List<Game>> games = unit.Games.GetAll();

                return await games;
            }
        }
        public async Task<Game> Get(int id)
        {
            using (unit)
            {
                Task<Game> game = unit.Games.Get(id);

                if (game == null) return null;

                return await game;
            }
        }
        public Game Put(Game entity)
        {
            using (unit)
            {
                Game game = unit.Games.Update(entity);

                unit.Complete();

                return  game;
            }
        }
        public async Task<Game> Post(Game entity)
        {
            using (unit)
            {
                Task<Game> game = unit.Games.Add(entity);

                unit.Complete();

                return await game;
            }
        }
        public Game Delete(int id)
        {
            using (unit)
            {
                Game game = unit.Games.Delete(id);

                unit.Complete();

                return  game;
            }
        }

    }
}
