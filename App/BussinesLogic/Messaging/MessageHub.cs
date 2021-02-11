using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace BussinesLogic.Messaging
{
    public struct dictionaryValue
    {
        public string username;
        public string connectionID;
    };
    public class MessageHub : Hub
    {
        private readonly IMemoryCache _memoryCache;
        public MessageHub(IMemoryCache memoryCache) : base()
        {
            _memoryCache = memoryCache;
        }
        public async Task SendStringMessage(string msg)
        {
            await Clients.All.SendAsync("ReceiveStringMessage", msg);
        }

        public async Task SendListMessage(List<string> messages)
        {
            await Clients.All.SendAsync("ReceiveListMessage", messages);
        }

        public async Task SendMessage(TestDTO message)
        {
            await Clients.All.SendAsync("ReceiveTestMessage", message);
        }

        public async Task<string> JoinGameGroup(CreateGameMsgDTO msg)
        {
            Dictionary<string, List<dictionaryValue>> dictionary = null;
            _memoryCache.TryGetValue("dictionary", out dictionary);

            List<dictionaryValue> res = new List<dictionaryValue>();
            dictionary.TryGetValue(msg.LobbyID, out res);

            res.ForEach(async element => {
                await Groups.AddToGroupAsync(element.connectionID, "Game" + msg.GameID);
                await Groups.RemoveFromGroupAsync(element.connectionID, "Lobby" + msg.LobbyID);
            });

            dictionary.Remove(msg.LobbyID);
            _memoryCache.Set("dictionary", dictionary);

            await NotifyOnGameChanges(msg.GameID, "ReceiveGameStarted", msg.GameID);

            return "Joined group \"Game" + msg.GameID + "\"";
        }

        public async Task<string> LeaveGameGroup(int gameID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Trip" + gameID);
            return "Left group \"Game" + gameID + "\"";
        }

        public async Task NotifyOnGameChanges(int gameID, String method, Object object_to_send)
        {
            await Clients.Group("Game" + gameID).SendAsync(method, object_to_send);
        }

        public async Task<string> JoinLobbyGroup(LobbyPlayerDTO msg)
        {
         
            Dictionary<string, List<dictionaryValue>> dictionary = null;
            _memoryCache.TryGetValue("dictionary", out dictionary);

            if (dictionary == null) dictionary = new Dictionary<string, List<dictionaryValue>>();

            List<dictionaryValue> res = new List<dictionaryValue>();
            dictionary.TryGetValue(msg.LobbyID, out res);

            if (res != null)
            {
                if (res.Count == 6)
                    return "Not joined group \"Lobby" + msg.LobbyID + "\"";
            }
            else
                dictionary.Add(msg.LobbyID, new List<dictionaryValue>());

            dictionaryValue a;
            a.username = msg.Username;
            a.connectionID = Context.ConnectionId;

            dictionary[msg.LobbyID].Add(a);
            _memoryCache.Set("dictionary", dictionary);

            await Groups.AddToGroupAsync(Context.ConnectionId, "Lobby" + msg.LobbyID);

            dictionary.TryGetValue(msg.LobbyID, out res);
            List<string> usernames = new List<string>();
            res.ForEach(element => {

                usernames.Add(element.username);
            });

            await NotifyOnLobbyChanges(msg.LobbyID, "ReceiveLobbyPlayerAdd", usernames);

            return "Joined group \"Lobby" + msg.LobbyID + "\"";

        }

        public async Task<string> LeaveLobbyGroup(string lobbyID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Lobby" + lobbyID);
            return "Left group \"Lobby" + lobbyID + "\"";
        }

        public async Task NotifyOnLobbyChanges(string lobbyID, String method, Object object_to_send)
        {
            await Clients.Group("Lobby" + lobbyID).SendAsync(method, object_to_send);
        }

      
    }
}


