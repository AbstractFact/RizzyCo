using DataAccess.Models;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinesLogic.Services.Strategy
{
    public class DestroyPlayerStrategy : IMissionStrategy
    {
        private readonly IUnitOfWork unit;
        private string color;
        private string targetColor;
        private string playerColor;
        private int conqID;
        private int gameID;
        public DestroyPlayerStrategy(IUnitOfWork unit, string color, string targetColor, string playerColor, int conqID, int gameID)
        {
            this.unit = unit;
            this.color = color;
            this.targetColor = targetColor;
            this.playerColor = playerColor;
            this.conqID = conqID;
            this.gameID = gameID;
        }
        public async Task<bool> CheckComplete(int playerID)
        {
            bool completed = false;
            int numTerr = await this.unit.PlayerTerritories.GetPlayerTerritoriesByColor(this.color, gameID);
            if (numTerr == 0 && playerID == conqID && color == targetColor)
                completed = true;
            else if (numTerr == 0 || color == playerColor)
            {
                List<PlayerTerritory> playerTerritories = await unit.PlayerTerritories.GetPlayerTerritories(playerID);

                if (playerTerritories.Count >= 24)
                    completed = true;
            }

            return completed;
        }
    }
}
