using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain.RepositoryInterfaces;

namespace Repository
{
    public class PlayerColorRepository : Repository<PlayerColor, RizzyCoContext>, IPlayerColorRepository
    {
        public PlayerColorRepository(RizzyCoContext context) : base(context)
        {

        }
        
    }
}
