using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Models;

namespace BussinesLogic.Authentication
{
    public interface IUserAuthService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
