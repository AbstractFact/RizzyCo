using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain.RepositoryInterfaces;

namespace Repository
{
    public class CardRepository : Repository<Card, RizzyCoContext>, ICardRepository
    {
        public CardRepository(RizzyCoContext context) : base(context)
        {

        }

      
    }
}
