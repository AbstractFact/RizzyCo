using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain.Models;
using Domain.Interfaces;
using DataAccess.EFCore;

namespace BussinesLogic.Services
{
    public class UserService : IUserService
    {
        private readonly RizzyCoContext context;

        public UserService(RizzyCoContext context)
        {
            this.context = context;
        }
        public async Task<List<User>> GetAll()
        {
            using (IUnitOfWork uw = new UnitOfWork(context))
            {
                Task<List<User>> users = uw.Users.GetAll();

                return await users;
            }
        }
    }
}
