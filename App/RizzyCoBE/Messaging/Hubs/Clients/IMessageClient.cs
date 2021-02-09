using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RizzyCoBE.Messaging.Hubs.Clients
{
    public interface IMessageClient
    {
        Task ReceiveStringMessage(string message);
        Task ReceiveListMessage(List<string> message);
        Task ReceiveTestMessage(TestDTO message);
    }
}
