using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;

namespace BussinesLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unit;

        public UserService(IUnitOfWork unit)
        {
            this.unit = unit;
        }
        public async Task<List<User>> GetAll()
        {
           using (unit)
            {
                Task<List<User>> users = unit.Users.GetAll();

                return await users;
            }
        }
        public async Task<User> Get(int id)
        {
            using (unit)
            {
                Task<User> user = unit.Users.Get(id);

                if (user == null) return null;

                return await user;
            }
        }

        public User Put(User entity)
        {
            using (unit)
            {
                User user = unit.Users.Update(entity);

                unit.Complete();

                return user;
            }
        }

        public async Task<User> Post(User entity)
        {
            using (unit)
            {
                Task<User> user = unit.Users.Add(entity);

                unit.Complete();

                return await user;
            }
        }

        public User Delete(int id)
        {
            using (unit)
            {
                User user = unit.Users.Delete(id);

                unit.Complete();

                return user;
            }
        }
    }
}
