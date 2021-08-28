using DataAccess.Models;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinesLogic.Services.Strategy
{
    public class NumTerritoriesStrategy : IMissionStrategy
    {
        private readonly IUnitOfWork unit;
        private int numTerritories;

        public NumTerritoriesStrategy(IUnitOfWork unit, int num)
        {
            this.unit = unit;
            this.numTerritories = num;
        }
        public async Task<bool> CheckComplete(int playerID)
        {

            List<PlayerTerritory> playerTerritories = await unit.PlayerTerritories.GetPlayerTerritories(playerID);
                
            if (numTerritories == 24)
                return playerTerritories.Count >= 24;

            if (numTerritories == 18 && playerTerritories.Count >= 18)
            {
                int complete = 0;
                playerTerritories.ForEach(territory =>
                {
                    if (territory.Armies >= 2)
                        complete++;
                });

                return complete >= 18;
            }

            return false;
            
        }

    }
}
