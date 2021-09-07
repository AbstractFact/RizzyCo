using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

                return game;
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

                return game;
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

                unit.Complete();

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
                    bonus += c.Value;
            }

            return bonus;
        }

        public async Task<NextPlayerDTO> EndTurn(int gameID, int mapID)
        {
            using (unit)
            {
                PlayerCard playerCard = await GetCard(gameID);
                GetCardDTO cardDTO;
                if (playerCard != null)
                    cardDTO = new GetCardDTO
                    {
                        ID = playerCard.ID,
                        Type = playerCard.Card.Type,
                        Picture = playerCard.Card.Picture,
                        TerritoryID = playerCard.Card.Territory != null ? playerCard.Card.Territory.ID : -1,
                        TerritoryName = playerCard.Card.Territory != null ? playerCard.Card.Territory.Name : "",
                        PlayerID = playerCard.Player.ID
                    };
                else
                    cardDTO = null;

                NextPlayerDTO nextPlayer = await unit.Players.EndTurn(gameID);
                int bonus = await CalculateBonusArmies(nextPlayer.NextPlayerID, mapID);
                await unit.Players.UpdateAvailableReinforcements(nextPlayer.NextPlayerID, bonus);
                nextPlayer.Bonus = bonus;
                nextPlayer.Card = cardDTO;

                unit.Complete();

                return nextPlayer;
            }
        }

        public async Task<PlayerCard> GetCard(int gameID)
        {
            List<Player> players = await unit.Players.GetPlayers(gameID);
            Player player = players.Where(player => player.WonCard == true).FirstOrDefault();

            if (player == null)
                return null;

            List<PlayerCard> existingCards = await unit.PlayerCards.GetPlayerCards(player.ID);
            if (existingCards.Count == 5)
                return null;

            List<PlayerCard> availableCards = await unit.PlayerCards.GetAvailableCards(gameID);

            Random rnd = new Random();
            int randomInd = rnd.Next(0, availableCards.Count);

            PlayerCard playerCard = availableCards.ElementAt(randomInd);
            playerCard.Player = player;
            unit.PlayerCards.Update(playerCard);

            return playerCard;
        }
    }
}
