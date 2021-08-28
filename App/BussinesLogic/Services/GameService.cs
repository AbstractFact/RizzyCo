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

        public async Task<List<PlayerTerritoryDTO>> GetGameTerritories(int id)
        {
            using (unit)
            {
                List<Player> players = await unit.Players.GetPlayers(id);
                List<PlayerTerritory> territories = new List<PlayerTerritory>();
                List<PlayerTerritoryDTO> territoriesDTO = new List<PlayerTerritoryDTO>();

                foreach (Player player in players)
                {
                    List<PlayerTerritory> list = await unit.PlayerTerritories.GetPlayerTerritories(player.ID);
                    territories.AddRange(list);
                };

                territories.ForEach(element =>
                {
                    territoriesDTO.Add(new PlayerTerritoryDTO(element));
                });

                return territoriesDTO;
            }
        }

        public async Task<int> NextStage(int gameID, int playerID, int mapID)
        {
            using (unit)
            {
                Game game = await unit.Games.NextStage(gameID);
                int bonus = await CalculateBonusArmies(playerID, mapID);
                await unit.Players.UpdateAvailableReinforcements(playerID, bonus);
                return bonus;
            }
        }

        public async Task<int> CalculateBonusArmies(int playerID, int mapID)
        {
            int bonus = 0;
            List<PlayerTerritory> playerTerritories = await unit.PlayerTerritories.GetPlayerTerritories(playerID);
            List<Territory> territories = new List<Territory>();
            playerTerritories.ForEach(pt =>
            {
                territories.Add(pt.Territory);
            });
            bonus = territories.Count / 3;

            List<Continent> continents = await unit.Continents.GetMapContinents(mapID);

            foreach (Continent c in continents)
            {
                List<Territory> continentTerritories = await unit.Territories.GetContinentTerritories(c.ID);
                if (continentTerritories.Intersect(territories).Count() == continentTerritories.Count())
                {
                    switch (c.Name)
                    {
                        case "Europe":
                            bonus += 5;
                            break;
                        case "Africa":
                            bonus += 3;
                            break;
                        case "Australia":
                            bonus += 2;
                            break;
                        case "Northern America":
                            bonus += 5;
                            break;
                        case "Southern America":
                            bonus += 2;
                            break;
                        default:
                            bonus += 7;
                            break;
                    }

                }
            }

            return bonus;
        }

        public async Task<NextPlayerDTO> EndTurn(int gameID, int mapID)
        {
            using (unit)
            {
                NextPlayerDTO nextPlayer = await unit.Players.EndTurn(gameID);
                int bonus = await CalculateBonusArmies(nextPlayer.NextPlayerID, mapID);
                await unit.Players.UpdateAvailableReinforcements(nextPlayer.NextPlayerID, bonus);
                nextPlayer.Bonus = bonus;
                unit.Complete();
                return nextPlayer;
            }

        }
    }
}
