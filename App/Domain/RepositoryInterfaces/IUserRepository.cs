using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> GetAllSync();
    }
}
