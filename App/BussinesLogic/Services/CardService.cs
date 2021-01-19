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
    public class CardService : ICardService
    {
        private readonly IUnitOfWork unit;

        public CardService(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        public Card Delete(int id)
        {
            using (unit)
            {
                Card card =  unit.Cards.Delete(id);

                unit.Complete();

                return card;
            }
        }

        public async Task<Card> Get(int id)
        {
            using (unit)
            {
                Task<Card> card = unit.Cards.Get(id);

                if (card == null) return null;

                return await card;
            }
        }

        public async Task<List<Card>> GetAll()
        {
            using (unit)
            {
                Task<List<Card>> cards = unit.Cards.GetAll();

                return await cards;
            }
        }

        public async Task<Card> Post(Card entity)
        {
            using (unit)
            {
                Task<Card> card = unit.Cards.Add(entity);

                unit.Complete();

                return await card;
            }
        }

        public Card Put(Card entity)
        {
            using (unit)
            {
                Card card = unit.Cards.Update(entity);

                unit.Complete();

                return card;
            }
        }

        public async Task<Card> AddCard(Card entity, int mapID, int territoryID)
        {
            using (unit)
            {
                Map map = await unit.Maps.Get(mapID);
                Territory territory = await unit.Territories.Get(territoryID);

                entity.Map = map;
                entity.Territory = territory;

                await unit.Cards.Add(entity);

                unit.Complete();

                return entity;
            }
        }

        public async Task<Card> AddJokerCard(Card entity, int mapID)
        {
            using (unit)
            {
                Map map = await unit.Maps.Get(mapID);

                entity.Map = map;
                entity.Territory = null;

                await unit.Cards.Add(entity);

                unit.Complete();

                return entity;
            }
        }
    }
}