using DataAccess.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Services.Strategy
{
    public class DestroyPlayerStrategy : IMissionStrategy
    {
        private readonly IUnitOfWork unit;
        private string color;
        private string targetColor;
        private int conqID;
        private int gameID;
        public DestroyPlayerStrategy(IUnitOfWork unit, string color, string targetColor, int conqID, int gameID)
        {
            this.unit = unit;
            this.color = color;
            this.targetColor = targetColor;
            this.conqID = conqID;
            this.gameID = gameID;
        }
        public async Task<bool> CheckComplete(int playerID)
        {
            bool completed = false;
            int numTerr = await this.unit.PlayerTerritories.GetPlayerTerritoriesByColor(this.color, gameID);
            if(numTerr == 0 && playerID == conqID && color == targetColor)
                completed = true;
            else if(numTerr == 0)
            {
                List<PlayerTerritory> playerTerritories = await unit.PlayerTerritories.GetPlayerTerritories(playerID);

                if (playerTerritories.Count >= 24)
                    completed = true;
            }

            return completed;
        }
    }
}
