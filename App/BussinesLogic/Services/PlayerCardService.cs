using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinesLogic.Services
{
    public class PlayerCardService : IPlayerCardService
    {
        private readonly IUnitOfWork unit;

        public PlayerCardService(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public PlayerCard Delete(int id)
        {
            using (unit)
            {
                PlayerCard playerCard = unit.PlayerCards.Delete(id);

                unit.Complete();

                return playerCard;
            }
        }

        public async Task<PlayerCard> Get(int id)
        {
            using (unit)
            {
                Task<PlayerCard> playerCard = unit.PlayerCards.Get(id);

                if (playerCard == null) return null;

                return await playerCard;
            }
        }

        public async Task<List<PlayerCard>> GetAll()
        {
            using (unit)
            {
                Task<List<PlayerCard>> playerCards = unit.PlayerCards.GetAll();

                return await playerCards;
            }
        }

        public async Task<PlayerCard> Post(PlayerCard entity)
        {
            using (unit)
            {
                Task<PlayerCard> playerCard = unit.PlayerCards.Add(entity);

                unit.Complete();

                return await playerCard;
            }
        }

        public PlayerCard Put(PlayerCard entity)
        {
            using (unit)
            {
                PlayerCard playerCard = unit.PlayerCards.Update(entity);

                unit.Complete();

                return playerCard;
            }
        }

        public async Task<PlayerCard> AddPlayerCard(int playerID, int cardID)
        {
            using (unit)
            {
                Player player = await unit.Players.Get(playerID);
                Card card = await unit.Cards.Get(cardID);

                PlayerCard playerCard = new PlayerCard();
                playerCard.Player = player;
                playerCard.Card = card;

                await unit.PlayerCards.Add(playerCard);

                unit.Complete();

                return playerCard;
            }
        }

        public async Task<List<GetCardDTO>> GetPlayerCards(int playerID)
        {
            using (unit)
            {
                List<GetCardDTO> listDTOs = new List<GetCardDTO>();
                List<PlayerCard> playerCards = await unit.PlayerCards.GetPlayerCards(playerID);

                playerCards.ForEach(playerCard =>
                {
                    listDTOs.Add(new GetCardDTO
                    {
                        ID = playerCard.ID,
                        Type = playerCard.Card.Type,
                        Picture = playerCard.Card.Picture,
                        TerritoryID = playerCard.Card.Territory != null ? playerCard.Card.Territory.ID : -1,
                        TerritoryName = playerCard.Card.Territory != null ? playerCard.Card.Territory.Name : "",
                        PlayerID = playerCard.Player.ID
                    });
                });

                return listDTOs;
            }
        }

        public async Task<int> UseCards(int card1ID, int card2ID, int card3ID)
        {
            using (unit)
            {
                int bonus = 0;

                PlayerCard card1 = await unit.PlayerCards.GetCard(card1ID);
                PlayerCard card2 = await unit.PlayerCards.GetCard(card2ID);
                PlayerCard card3 = await unit.PlayerCards.GetCard(card3ID);

                if (card1 == null || card2 == null || card3 == null)
                    return -1;

                if (card1.Card.Type == card2.Card.Type && card1.Card.Type == card3.Card.Type)
                {
                    switch (card1.Card.Type)
                    {
                        case "Tank":
                            bonus += 4;
                            break;
                        case "Troop":
                            bonus += 6;
                            break;
                        default:
                            bonus += 8;
                            break;
                    }
                }

                if (card1.Card.Type != card2.Card.Type && card1.Card.Type != card3.Card.Type && card2.Card.Type != card3.Card.Type && card1.Card.Type != "Joker" && card2.Card.Type != "Joker" && card3.Card.Type != "Joker")
                    bonus += 10;

                if (card1.Card.Type == card2.Card.Type && card1.Card.Type != "Joker" && card3.Card.Type == "Joker")
                    bonus += 12;

                if (card1.Card.Type == card3.Card.Type && card1.Card.Type != "Joker" && card2.Card.Type == "Joker")
                    bonus += 12;

                if (card2.Card.Type == card3.Card.Type && card2.Card.Type != "Joker" && card1.Card.Type == "Joker")
                    bonus += 12;

                if (bonus != 0)
                {
                    if (card1.Card.Type != "Joker")
                    {
                        if (await unit.PlayerTerritories.GetPlayerTerritory(card1.Player.ID, card1.Card.Territory.ID) != null)
                            bonus += 2;
                    }

                    if (card2.Card.Type != "Joker")
                    {
                        if (await unit.PlayerTerritories.GetPlayerTerritory(card2.Player.ID, card2.Card.Territory.ID) != null)
                            bonus += 2;
                    }

                    if (card3.Card.Type != "Joker")
                    {
                        if (await unit.PlayerTerritories.GetPlayerTerritory(card3.Player.ID, card3.Card.Territory.ID) != null)
                            bonus += 2;
                    }

                    await unit.Players.UpdateAvailableReinforcements(card1.Player.ID, bonus);

                    card1.Player = null;
                    card2.Player = null;
                    card3.Player = null;

                    unit.PlayerCards.Update(card1);
                    unit.PlayerCards.Update(card2);
                    unit.PlayerCards.Update(card3);

                    unit.Complete();
                }

                return bonus;
            }

        }
    }
}
