using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess.Models;
using DataAccess.Data.EFCore;

namespace DataAccess.Data.EFCore
{
    public class EfCorePlayerRepository : EfCoreRepository<Player, RizzyCoContext>
    {
        public EfCorePlayerRepository(RizzyCoContext context) : base(context)
        {

        }
        // We can add new methods specific to the movie repository here in the future
    }
}
