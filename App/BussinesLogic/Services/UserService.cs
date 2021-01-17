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


        public async Task<User> CreateGame(List<string> players, int userId, int numPlayers, int mapID)
        {
            
            Map map = await unit.Maps.Get(mapID);
            

            Task<User> uu = unit.Users.Get(userId);
            User user = await uu;

            Game realGame = new Game();
            realGame.NumberOfPlayers = numPlayers;
            realGame.Finished = false;
            realGame.Creator = user;
            realGame.Map = map;
            await unit.Games.Add(realGame);

            Player player = new Player();
            player.User = user;
            player.Game = realGame;
            await unit.Players.Add(player);

            realGame.Players.Add(player);

            user.Games.Add(realGame);
            user.Players.Add(player);
            unit.Users.Update(user);

            await unit.GamesUser.AddGameUser(realGame, user);

            Stack<PlayerColor> colors = new Stack<PlayerColor>();
            (await unit.PlayerColors.GetAll()).ForEach(elem =>
            {
                colors.Push(elem);
            });

            GamePlayerColor gamePlayerColor = new GamePlayerColor();
            PlayerColor color = colors.Pop();
            gamePlayerColor.PlayerColor = color;
            gamePlayerColor.Game = realGame;
            await unit.GamePlayerColors.Add(gamePlayerColor);
            player.PlayerColor = color;
            

            foreach (string playerUsername in players)
            {
                Task<User> u = unit.Users.GetUserByUsername(playerUsername);
                User realU = await u;
                Player invitedPlayer = new Player();
                invitedPlayer.User = realU;
                invitedPlayer.Game = realGame;
                await unit.Players.Add(invitedPlayer);
                realGame.Players.Add(invitedPlayer);

                await unit.GamesUser.AddGameUser(realGame, realU);

                realU.Players.Add(invitedPlayer);
                unit.Users.Update(realU);

                GamePlayerColor gamePlayerColor1 = new GamePlayerColor();
                PlayerColor color1 = colors.Pop();
                gamePlayerColor1.PlayerColor = color1;
                gamePlayerColor1.Game = realGame;
                await unit.GamePlayerColors.Add(gamePlayerColor1);
                invitedPlayer.PlayerColor = color1;

                unit.Complete();
                unit.Players.Update(invitedPlayer);

            }

            unit.Complete();

            unit.Players.Update(player);
            unit.Games.Update(realGame);

            unit.Complete();

            return await uu;
        }
    }
}
