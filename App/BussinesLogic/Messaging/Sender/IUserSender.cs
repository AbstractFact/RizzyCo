using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLogic.Messaging.Sender
{
    public interface IUserSender
    {
        void SendInvitation(User user);
    }
}
