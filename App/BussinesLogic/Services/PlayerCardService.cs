using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;

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

        public async Task<List<PlayerCard>> GetPlayerCards(int playerID)
        {
            using (unit)
            {
                Task<List<PlayerCard>> playerCards = unit.PlayerCards.GetPlayerCards(playerID);

                return await playerCards;
            }
        }

    }
}
