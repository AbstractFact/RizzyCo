using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess.Models;
using DataAccess.Data.EFCore;

namespace DataAccess.Data.EFCore
{
    public class EfCoreUserRepository : EfCoreRepository<User, RizzyCoContext>
    {
        public EfCoreUserRepository(RizzyCoContext context) : base(context)
        {

        }
        // We can add new methods specific to the movie repository here in the future
    }
}
