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
        public async Task<List<Card>> GetAllCards()
        {
            using (IUnitOfWork uw = new UnitOfWork(context))
            {
                Task<List<Card>> cards = uw.Cards.GetAll();

                return await cards;
            }
        }
    }
}