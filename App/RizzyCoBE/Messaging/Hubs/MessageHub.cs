using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using DTOs;
using RizzyCoBE.Messaging.Hubs.Clients;

namespace RizzyCoBE.Messaging.Hubs
{
    public class MessageHub : Hub
    {
        //public async Task SendMessage(string msg)
        //{
        //    await Clients.All.SendAsync("ReceiveStringMessage", msg);
        //}

        //public async Task SendListMessage(List<string> messages)
        //{
        //    await Clients.All.SendAsync("ReceiveListMessage", messages);
        //}

        public async Task SendMessage(TestDTO message)
        {
            await Clients.All.SendAsync("ReceiveTestMessage", message);
        }
    }
}


