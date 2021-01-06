using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;
using DataAccess.EFCore;


namespace BussinesLogic.Services
{
    public class CardService : ICardService
    {
        private readonly RizzyCoContext context;

        public CardService(RizzyCoContext context)
        {
            this.context = context;
        }

        public async Task<Card> Delete(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Card> card =  unit.Cards.Delete(id);

                unit.Complete();

                return await card;
            }
        }

        public async Task<Card> Get(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Card> card = unit.Cards.Get(id);

                if (card == null) return null;

                return await card;
            }
        }

        public async Task<List<Card>> GetAll()
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<List<Card>> cards = unit.Cards.GetAll();

                return await cards;
            }
        }

        public async Task<Card> Post(Card entity)
        { 
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Card> card = unit.Cards.Add(entity);

                unit.Complete();

                return await card;
            }
        }

        public async Task<Card> Put(Card entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<Card> card = unit.Cards.Update(entity);

                unit.Complete();

                return await card;
            }
        }
    }
}