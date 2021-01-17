using DataAccess;
using DataAccess.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GamePlayerColorRepository : Repository<GamePlayerColor, RizzyCoContext>, IGamePlayerColorRepository
    {
        public GamePlayerColorRepository(RizzyCoContext context) : base(context)
        {

        }
    }
}
