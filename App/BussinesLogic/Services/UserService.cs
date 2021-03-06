﻿using System;
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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unit;

        public UserService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        //public async Task<List<User>> GetAll()
        //{
        //    using (unit)
        //    {
        //        Task<List<User>> users = unit.Users.GetAll();

        //        return await users;
        //    }
        //}
        //public async Task<User> Get(int id)
        //{
        //    using (unit)
        //    {
        //        Task<User> user = unit.Users.Get(id);

        //        if (user == null) return null;

        //        return await user;
        //    }
        //}

        //public User Put(User entity)
        //{
        //    using (unit)
        //    {
        //        User user = unit.Users.Update(entity);

        //        unit.Complete();

        //        return user;
        //    }
        //}

        //public async Task<User> Post(User entity)
        //{
        //    using (unit)
        //    {
        //        Task<User> user = unit.Users.Add(entity);

        //        unit.Complete();

        //        return await user;
        //    }
        //}

        //public User Delete(int id)
        //{
        //    using (unit)
        //    {
        //        User user = unit.Users.Delete(id);

        //        unit.Complete();

        //        return user;
        //    }
        //}

        public async Task<List<UserDTO>> GetAll()
        {
            using (unit)
            {
                List<User> users = await unit.Users.GetAll();

                return UserDTO.FromEntityList(users);
            }
        }
        public async Task<UserDTO> Get(int id)
        {
            using (unit)
            {
                User user = await unit.Users.Get(id);

                if (user == null) return null;

                return new UserDTO(user);
            }
        }

        public UserDTO Put(UserDTO entity)
        {
            using (unit)
            {

                User user = unit.Users.Update(UserDTO.FromDTO(entity));

                unit.Complete();

                return new UserDTO(user);
            }
        }

        public async Task<UserDTO> Post(UserDTO entity)
        {
            using (unit)
            {
                User user = await unit.Users.Add(UserDTO.FromDTO(entity));

                unit.Complete();

                return new UserDTO(user);
            }
        }

        public UserDTO Delete(int id)
        {
            using (unit)
            {
                User user = unit.Users.Delete(id);

                unit.Complete();

                return new UserDTO(user);
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

        public async Task<User> CreateGame(List<string> users, int creatorId, int mapID)
        {
            using (unit)
            {
                List<Player> players = new List<Player>();

                Map map = await unit.Maps.Get(mapID);
                Task<User> uu = unit.Users.Get(creatorId);
                User user = await uu;

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
                await unit.Games.Add(game);


                Player player = new Player();
                player.Creator = true;
                player.OnTurn = true;
                player.User = user;
                player.Game = game;
                player.PlayerColor = colors.Pop();
                player.Mission = mission;
                await unit.Players.Add(player);
                players.Add(player);

                foreach (string username in users)
                {
                    User u = await unit.Users.GetUserByUsername(username);

                    Player invitedPlayer = new Player();
                    invitedPlayer.Creator = false;
                    invitedPlayer.OnTurn = false;
                    invitedPlayer.User = u;
                    invitedPlayer.Game = game;
                    invitedPlayer.PlayerColor = colors.Pop();

                    missonCount = missions.Count;
                    randomInd = rnd.Next(0, missonCount - 1);

                    Mission invitedPlayerMission = missions.ElementAt(randomInd);
                    missions.RemoveAt(randomInd);

                    invitedPlayer.Mission = invitedPlayerMission;
                    await unit.Players.Add(invitedPlayer);
                    players.Add(invitedPlayer);

                }

                //List<Territory> territories = await GetMapTerritories(mapID);

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
                    playerTerritory.Player = players.ElementAt(i % playersCount);
                    playerTerritory.Territory = territories.ElementAt(randomInd);
                    territories.RemoveAt(randomInd);

                    await unit.PlayerTerritories.Add(playerTerritory);

                    i++;
                }

                unit.Complete();

                return await uu;
            }
        }
    }
}
