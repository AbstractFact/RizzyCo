using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class NumTerritoriesStrategy : AbstractMissionStrategy
    {
        private int numTerritories;

        public NumTerritoriesStrategy(int playerID, int num)
        {
            this.playerID = playerID;
            this.numTerritories = num;
        }
        public override bool CheckComplete()
        {
            throw new NotImplementedException();
        }
 
    }
}
