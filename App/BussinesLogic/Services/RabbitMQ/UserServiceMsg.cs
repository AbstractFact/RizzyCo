using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLogic.Services.RabbitMQ
{
    public class UserServiceMsg : IUserServiceMsg
    {
        public void ReceiveMsg(User user)
        {
            User u = user;
        }
    }
}
