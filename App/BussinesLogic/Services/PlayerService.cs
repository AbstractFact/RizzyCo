using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataAccess;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;
using DTOs;

namespace BussinesLogic.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork unit;

        public PlayerService(IUnitOfWork unit)
        {
            this.unit = unit;
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

                if (player == null) return null;

                return new PlayerInfoDTO(player);
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

                result.Add(new GameInfoDTO() { CreationDate = el.Game.CreationDate, Participants = participants});
            }

            return result;
        }
    }
}
