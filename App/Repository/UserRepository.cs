using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<User> GetUserByUsername(string username)
        {
            return await context.Set<User>().SingleOrDefaultAsync(u => u.Username == username);
        }
  
    }
}
