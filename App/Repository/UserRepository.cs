using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain.RepositoryInterfaces;

namespace Repository
{
    public class UserRepository : Repository<User, RizzyCoContext>, IUserRepository
    {
        public UserRepository(RizzyCoContext context) : base(context)
        {
           
        }

        public List<User> GetAllSync()
        {
            return  context.Set<User>().ToList();
        }

        // We can add new methods specific to the movie repository here in the future
    }
}
