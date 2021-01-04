using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;

namespace DataAccess.EFCore
{
    public class EfCoreTerritoryRepository : EfCoreRepository<Territory, RizzyCoContext>, IEfCoreTerritoryRepository
    {
        public EfCoreTerritoryRepository(RizzyCoContext context) : base(context)
        {

        }
        // We can add new methods specific to the movie repository here in the future
    }
}
