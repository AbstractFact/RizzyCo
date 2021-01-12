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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unit;

        public UserService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<List<User>> GetAll()
        {
           using (unit)
            {
                Task<List<User>> users = unit.Users.GetAll();

                return await users;
            }
        }
        public async Task<User> Get(int id)
        {
            using (unit)
            {
                Task<User> user = unit.Users.Get(id);

                if (user == null) return null;

                return await user;
            }
        }

        public User Put(User entity)
        {
            using (unit)
            {
                User user = unit.Users.Update(entity);

                unit.Complete();

                return user;
            }
        }

        public async Task<User> Post(User entity)
        {
            using (unit)
            {
                Task<User> user = unit.Users.Add(entity);

                unit.Complete();

                return await user;
            }
        }

        public User Delete(int id)
        {
            using (unit)
            {
                User user = unit.Users.Delete(id);

                unit.Complete();

                return user;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
               return await unit.Users.GetAllUsers();
        }

        public async Task<User> CreateGame(int userId, int numPlayers)
        {
            Task<User> user = unit.Users.Get(userId);
            User realUser = await user;

            Game realGame = new Game();
            realGame.NumberOfPlayers = numPlayers;
            realGame.Finished = false;
            realGame.User = realUser;
            await unit.Games.Add(realGame);
            //unit.Complete();

            realUser.Games.Add(realGame);
            unit.Users.Update(realUser);

            //unit.Complete();

            Player player = new Player();
            player.User = realUser;
            player.Game = realGame;
            await unit.Players.Add(player);
            realGame.Players.Add(player);

            unit.Complete();
            unit.Games.Update(realGame);

            unit.Complete();


            return await user;
        }
    }
}
