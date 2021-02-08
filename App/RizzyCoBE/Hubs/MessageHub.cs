using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using DTOs;

namespace RizzyCoBE
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(string msg)
        {
            await Clients.All.SendAsync("ReceiveMessage", msg);
        }

        public async Task SendMQMessage(List<string> messages)
        {
            await Clients.All.SendAsync("ReceiveMQMessage", messages);
        }

        public async Task SendTestMessage(TestDTO message)
        {
            await Clients.All.SendAsync("ReceiveTestMessage", message);
        }
    }
}


