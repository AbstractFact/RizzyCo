using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BussinesLogic.Messaging;
using DataAccess;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using DTOs;
using Microsoft.AspNetCore.SignalR;

namespace BussinesLogic.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork unit;
        private HubService hubService;

        public PlayerService(IUnitOfWork unit, IHubContext<MessageHub> hubContext)
        {
            this.unit = unit;
            hubService = new HubService(hubContext);
        }
        public async Task<List<Player>> GetAll()
        {
            using (unit)
            {
                Task<List<Player>> players = unit.Players.GetAll();

                return await players;
            }
        }
        public async Task<Player> Get(int id)
        {
            using (unit)
            {
                Task<Player> player = unit.Players.Get(id);

                if (player == null) return null;

                return await player;
            }
        }

        public async Task<Player> GetPlayer(int id)
        {
            using (unit)
            {
                Task<Player> player = unit.Players.GetPlayer(id);

                if (player == null) return null;

                return await player;
            }
        }

        public async Task<List<Player>> GetPlayers(int id)
        {
            using (unit)
            {
                Task<List<Player>> player = unit.Players.GetPlayers(id);

                if (player == null) return null;

                return await player;
            }
        }

        public Player Put(Player entity)
        {
            using (unit)
            {
                Player player = unit.Players.Update(entity);

                unit.Complete();

                return player;
            }
        }

        public async Task<Player> Post(Player entity)
        {
            using (unit)
            {
                Task<Player> player = unit.Players.Add(entity);

                unit.Complete();

                return await player;
            }
        }

        public Player Delete(int id)
        {
            using (unit)
            {
                Player player = unit.Players.Delete(id);

                unit.Complete();

                return player;
            }
        }

        public async Task<PlayerInfoDTO> GetPlayerInfo(int gameID, int userID)
        {
            using (unit)
            {
                Player player = await unit.Players.GetPlayerInfo(gameID, userID);
                List<GameParticipantInfoDTO> participants = new List<GameParticipantInfoDTO>();
                List<Player> p = await unit.Players.GetPlayers(gameID);
                p.ForEach(pl =>
                {
                    participants.Add(new GameParticipantInfoDTO() { Username = pl.User.Username, PlayerColor = pl.PlayerColor.Value , OnTurn=pl.OnTurn});
                });

                if (player == null) return null;

                return new PlayerInfoDTO(player, participants);
            }
        }

        public async Task<List<GameInfoDTO>> GetUserGames(int userID)
        {
            List<Player> players = await unit.Players.GetUserPlayers(userID);
            List<GameInfoDTO> result = new List<GameInfoDTO>();

            foreach(Player el in players)
            {
                List<GameParticipantInfoDTO> participants = new List<GameParticipantInfoDTO>();
                List<Player> p = await unit.Players.GetPlayers(el.Game.ID);
                p.ForEach(pl =>
                {
                    participants.Add(new GameParticipantInfoDTO() { Username = pl.User.Username, PlayerColor = pl.PlayerColor.Value });
                });

                result.Add(new GameInfoDTO() { GameID = el.Game.ID, CreationDate = el.Game.CreationDate, Finished = el.Game.Finished, Participants = participants});
            }

            return result;
        }

        public async Task<bool> FullWaitingLobby(List<string> playersJoined, int gameID)
        {
            List<Player> players = await GetPlayers(gameID);
            List<string> playerUsernames = new List<string>();
            players.ForEach(el =>
            {
                playerUsernames.Add(el.User.Username);
            });

            bool result = false;
            if (playerUsernames.Intersect(playersJoined).Count() == playerUsernames.Count())
            {
                await hubService.NotifyOnWaitingLobbyChanges(gameID, "ReceiveGameContinued", gameID);
                result = true;
            }

            return result;
        }

    }
}
