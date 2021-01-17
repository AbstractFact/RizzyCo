using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;
using Domain;
using Domain.ServiceInterfaces;

namespace BussinesLogic.Services
{
    public class GamePlayerColorService : IGamePlayerColorService
    {
        private readonly IUnitOfWork unit;
        private readonly RizzyCoContext context;

        public GamePlayerColorService(IUnitOfWork unit, RizzyCoContext context)
        {
            this.unit = unit;
            this.context = context;
        }
        public async Task<GamePlayerColor> AddGamePlayerColor(int gameID, int playerColorID)
        {

            using (unit)
            {
                Game game = await unit.Games.Get(gameID);
                PlayerColor color = await unit.PlayerColors.Get(playerColorID);

                GamePlayerColor gamePlayerColor = new GamePlayerColor();
                gamePlayerColor.Game = game;
                gamePlayerColor.PlayerColor = color;
                gamePlayerColor.Available = false;

                await unit.GamePlayerColors.Add(gamePlayerColor);

                unit.Complete();

                return gamePlayerColor;
            }
        }

        public async Task<List<GamePlayerColor>> GetAll()
        {
            using (unit)
            {
                Task<List<GamePlayerColor>> gamePlayerColors = unit.GamePlayerColors.GetAll();

                return await gamePlayerColors;
            }
        }
        public async Task<GamePlayerColor> Get(int id)
        {
            using (unit)
            {
                Task<GamePlayerColor> gamePlayerColor = unit.GamePlayerColors.Get(id);

                if (gamePlayerColor == null) return null;

                return await gamePlayerColor;
            }
        }
        public GamePlayerColor Put(GamePlayerColor entity)
        {
            using (unit)
            {
                GamePlayerColor gamePlayerColor = unit.GamePlayerColors.Update(entity);

                unit.Complete();

                return gamePlayerColor;
            }
        }
        public async Task<GamePlayerColor> Post(GamePlayerColor entity)
        {
            using (unit)
            {
                Task<GamePlayerColor> gamePlayerColor = unit.GamePlayerColors.Add(entity);

                unit.Complete();

                return await gamePlayerColor;
            }
        }
        public GamePlayerColor Delete(int id)
        {
            using (unit)
            {
                GamePlayerColor gamePlayerColor = unit.GamePlayerColors.Delete(id);

                unit.Complete();

                return gamePlayerColor;
            }
        }
    }
}
