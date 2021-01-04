using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;

namespace DataAccess.EFCore
{
    public class EfCoreMissionRepository : EfCoreRepository<Mission, RizzyCoContext>, IEfCoreMissionRepository
    {
        public EfCoreMissionRepository(RizzyCoContext context) : base(context)
        {

        }
        // We can add new methods specific to the movie repository here in the future
    }
}
