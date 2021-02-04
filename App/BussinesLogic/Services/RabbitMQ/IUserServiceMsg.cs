using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLogic.Services.RabbitMQ
{
    public interface IUserServiceMsg
    {
        void ReceiveMsg(User user);
    }
}