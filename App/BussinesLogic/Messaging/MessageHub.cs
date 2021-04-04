using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using DTOs;
using Microsoft.Extensions.Caching.Memory;

namespace BussinesLogic.Messaging
{
    public class MessageHub : Hub
    {
        private readonly IMemoryCache _memoryCache;
        public MessageHub(IMemoryCache memoryCache) : base()
        {
            _memoryCache = memoryCache;
        }

        public async Task<string> CreateGame(JoinGameDTO msg)
        {
            await NotifyOnLobbyChanges(msg.LobbyID, "ReceiveGameStarted", msg.GameID);

            return "Created game";
        }

        public async Task<string> JoinGameGroup(JoinGameDTO msg)
        {    
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Lobby" + msg.LobbyID);
            await Groups.AddToGroupAsync(Context.ConnectionId, "Game" + msg.GameID);
            
            return "Joined group \"Game" + msg.GameID + "\"";
        }

        public async Task<string> LeaveGameGroup(int gameID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Trip" + gameID);
            return "Left group \"Game" + gameID + "\"";
        }

        public async Task<string> NotifyOnGameChanges(int gameID, String method, Object object_to_send)
        {
            await Clients.Group("Game" + gameID).SendAsync(method, object_to_send);
            return "Left group \"Game" + gameID + "\"";
        }

        public async Task<string> NotifyOnGameChanges1(AddArmieSendDTO msg)
        {
            await Clients.Group("Game" + msg.GameID).SendAsync(msg.Method, "Dodate armije");
            return "Left group \"Game" + msg.GameID + "\"";
        }

        public async Task<string> JoinLobbyGroup(LobbyPlayerDTO msg)
        {
         
            Dictionary<string, List<string>> dictionary = null;
            _memoryCache.TryGetValue("dictionary", out dictionary);

            if (dictionary == null) dictionary = new Dictionary<string, List<string>>();

            List<string> res = new List<string>();
            dictionary.TryGetValue(msg.LobbyID, out res);

            if (res != null)
            {
                if (res.Count == 6)
                    return "Not joined group \"Lobby" + msg.LobbyID + "\"";
            }
            else
                dictionary.Add(msg.LobbyID, new List<string>());
            
            if (!dictionary[msg.LobbyID].Contains(msg.Username))
                dictionary[msg.LobbyID].Add(msg.Username);

            _memoryCache.Set("dictionary", dictionary);

            await Groups.AddToGroupAsync(Context.ConnectionId, "Lobby" + msg.LobbyID);

            await NotifyOnLobbyChanges(msg.LobbyID, "ReceiveLobbyPlayerAdd", dictionary[msg.LobbyID]);

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


