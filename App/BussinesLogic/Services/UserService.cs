using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BussinesLogic.Messaging;
using DataAccess;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using DTOs;
using Microsoft.AspNetCore.SignalR;

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

        public async Task<List<Territory>> GetMapTerritories(int mapID)
        {
            using (unit)
            {
                List<Continent> mapContinents = await unit.Continents.GetMapContinents(mapID);
                List<Territory> territories = new List<Territory>();

                foreach (Continent continent in mapContinents)
                {

                    List<Territory> territories1 = await unit.Territories.GetContinentTerritories(continent.ID);
                    territories.AddRange(territories1);

                };

                return territories;
            }
        }

        public async Task<int> CreateGame(List<string> users, int mapID)
        {
            using (unit)
            {
                List<Player> players = new List<Player>();
                int counter = 0;

                Map map = await unit.Maps.Get(mapID);
                User user = await unit.Users.GetUserByUsername(users.ElementAt(0));
                users.RemoveAt(0);

                Stack<PlayerColor> colors = new Stack<PlayerColor>();
                (await unit.PlayerColors.GetAll()).ForEach(elem =>
                {
                    colors.Push(elem);
                });

                List<Mission> missions = await unit.Missions.GetMapMissions(mapID);
                int missonCount = missions.Count + 1;
                Random rnd = new Random();
                int randomInd = rnd.Next(0, missonCount - 1);

                Mission mission = missions.ElementAt(randomInd);
                missions.RemoveAt(randomInd);

                Game game = new Game();
                game.NumberOfPlayers = users.Count + 1;
                game.Finished = false;
                game.Map = map;
                game.CreationDate = DateTime.UtcNow;
                await unit.Games.Add(game);

                int availableArmies;

                switch (game.NumberOfPlayers)
                {
                    case 3:
                        availableArmies = 35;
                        break;
                    case 4:
                        availableArmies = 30;
                        break;
                    case 5:
                        availableArmies = 25;
                        break;
                    default:
                        availableArmies = 20;
                        break;
                }

                Player player = new Player();
                player.Creator = true;
                player.OnTurn = counter++;
                player.User = user;
                player.Game = game;
                player.PlayerColor = colors.Pop();
                player.Mission = mission;
                player.AvailableArmies = availableArmies;
                await unit.Players.Add(player);
                players.Add(player);

                foreach (string username in users)
                {
                    User u = await unit.Users.GetUserByUsername(username);

                    Player invitedPlayer = new Player();
                    invitedPlayer.Creator = false;
                    invitedPlayer.OnTurn = counter++;
                    invitedPlayer.User = u;
                    invitedPlayer.Game = game;
                    invitedPlayer.PlayerColor = colors.Pop();
                    invitedPlayer.AvailableArmies = availableArmies;

                    missonCount = missions.Count;
                    randomInd = rnd.Next(0, missonCount - 1);

                    Mission invitedPlayerMission = missions.ElementAt(randomInd);
                    missions.RemoveAt(randomInd);

                    invitedPlayer.Mission = invitedPlayerMission;
                    await unit.Players.Add(invitedPlayer);
                    players.Add(invitedPlayer);

                }

                List<Continent> mapContinents = await unit.Continents.GetMapContinents(mapID);
                List<Territory> territories = new List<Territory>();

                foreach (Continent continent in mapContinents)
                {

                    List<Territory> territories1 = await unit.Territories.GetContinentTerritories(continent.ID);
                    territories.AddRange(territories1);

                };

                int playersCount = players.Count;
                int i = 0;
                while (territories.Count > 0)
                {
                    PlayerTerritory playerTerritory = new PlayerTerritory();
                    randomInd = rnd.Next(0, territories.Count - 1);

                    playerTerritory.Armies = 1;
                    Player p= players.ElementAt(i % playersCount);
                    playerTerritory.Player = p;
                    players.ElementAt(i % playersCount).AvailableArmies--;
                    playerTerritory.Territory = territories.ElementAt(randomInd);
                    territories.RemoveAt(randomInd);
                    await unit.PlayerTerritories.Add(playerTerritory);

                    i++;
                }

                unit.Complete();

                players.ForEach(element => {
                    unit.Players.Update(element);
                });

                unit.Complete();

                return game.ID;
            }
        }

    }
}
