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
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<List<User>> users = unit.Users.GetAll();

                return await users;
            }
        }
        public async Task<User> Get(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<User> user = unit.Users.Get(id);

                if (user == null) return null;

                return await user;
            }
        }

        public async Task<User> Put(User entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<User> user = unit.Users.Update(entity);

                unit.Complete();

                return await user;
            }
        }

        public async Task<User> Post(User entity)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<User> user = unit.Users.Add(entity);

                unit.Complete();

                return await user;
            }
        }

        public async Task<User> Delete(int id)
        {
            using (IUnitOfWork unit = new UnitOfWork(context))
            {
                Task<User> user = unit.Users.Delete(id);

                unit.Complete();

                return await user;
            }
        }
    }
}
