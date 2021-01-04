using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;

namespace DataAccess.EFCore
{
    public class EfCorePlayerRepository : EfCoreRepository<Player, RizzyCoContext>, IEfCorePlayerRepository
    {
        public EfCorePlayerRepository(RizzyCoContext context) : base(context)
        {

        }
        // We can add new methods specific to the movie repository here in the future
    }
}
