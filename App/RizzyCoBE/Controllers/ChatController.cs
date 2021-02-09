//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using RizzyCoBE.Messaging.Hubs;
//using RizzyCoBE.Messaging.Hubs.Clients;

//namespace RizzyCoBE.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class ChatController : ControllerBase
//    {
//        private readonly IHubContext<MessageHub> _chatHub;

//        public ChatController(IHubContext<MessageHub> hub)
//        {
//            _chatHub = hub;
//        }

//        [HttpPost("messages")]
//        public async Task Post(string message)
//        {
//            // run some logic...

//            await _chatHub.Clients.All.ReceiveTestMessage(message);
//        }
//    }
//}