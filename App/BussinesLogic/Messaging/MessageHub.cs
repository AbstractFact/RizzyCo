using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using DTOs;
using Microsoft.Extensions.Caching.Memory;
using BussinesLogic.Services;
using DataAccess.Models;
using System.Linq;

namespace BussinesLogic.Messaging
{
    public class MessageHub : Hub
    {
        private readonly IMemoryCache _memoryCache;
        public MessageHub(IMemoryCache memoryCache) : base()
        {
            _memoryCache = memoryCache;
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

        public async Task<string> LeaveLobbyGroup(string lobbyID, string username)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Lobby" + lobbyID);
            await NotifyOnLobbyChanges(lobbyID, "ReceivePlayerLeftLobby", username);
            return "Left group \"Lobby" + lobbyID + "\"";
        }

        public async Task NotifyOnLobbyChanges(string lobbyID, String method, Object object_to_send)
        {
            await Clients.Group("Lobby" + lobbyID).SendAsync(method, object_to_send);
        }
        public async Task<string> CreateGame(JoinGameDTO msg)
        {
            await NotifyOnLobbyChanges(msg.LobbyID, "ReceiveGameStarted", new GameStartedDTO { GameID = msg.GameID, MapID = msg.MapID });

            return "Created game";
        }

        public async Task<string> JoinGameGroup(string lobbyID, string gameID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Lobby" + lobbyID);
            await Groups.AddToGroupAsync(Context.ConnectionId, "Game" + gameID);

            return "Joined group \"Game" + gameID + "\"";
        }

        public async Task<string> LeaveInterruptedGameGroup(int gameID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Game" + gameID);

            return "Left group \"Game" + gameID + "\"";
        }
        public async Task<string> LeaveGameGroup(int gameID, string username)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Game" + gameID);
            await Clients.Group("Game" + gameID).SendAsync("PlayerLeft", username);

            return "Left group \"Game" + gameID + "\"";
        }

        public async Task<string> JoinWaitingLobbyGroup(LobbyPlayerDTO msg)
        {
            Dictionary<string, List<string>> dictionary = null;
            _memoryCache.TryGetValue("dictionary", out dictionary);

            if (dictionary == null) dictionary = new Dictionary<string, List<string>>();

            List<string> res = new List<string>();
            dictionary.TryGetValue(msg.LobbyID, out res);

            if (res != null)
            {
                if (res.Count == 6)
                    return "Not joined group \"Waiting Lobby" + msg.LobbyID + "\"";
            }
            else
                dictionary.Add(msg.LobbyID, new List<string>());

            if (!dictionary[msg.LobbyID].Contains(msg.Username))
                dictionary[msg.LobbyID].Add(msg.Username);

            _memoryCache.Set("dictionary", dictionary);

            await Groups.AddToGroupAsync(Context.ConnectionId, "Waiting Lobby" + msg.LobbyID);

            await NotifyOnWaitingLobbyChanges(msg.LobbyID, "ReceiveWaitingLobbyPlayerAdd", dictionary[msg.LobbyID]);

            return "Joined group \"Waiting Lobby" + msg.LobbyID + "\"";

        }

        public async Task<string> LeaveWaitingLobbyGroup(string lobbyID, string username)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Waiting Lobby" + lobbyID);
            await NotifyOnWaitingLobbyChanges(lobbyID, "ReceivePlayerLeftWaitingLobby", username);
            return "Left group \"Waiting Lobby" + lobbyID + "\"";
        }

        public async Task NotifyOnWaitingLobbyChanges(string lobbyID, String method, Object object_to_send)
        {
            await Clients.Group("Waiting Lobby" + lobbyID).SendAsync(method, object_to_send);
        }

    }
}


