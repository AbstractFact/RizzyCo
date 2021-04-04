using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class ContinentStrategy : AbstractMissionStrategy
    {
        private int continentID;
        public ContinentStrategy(int playerID, int continentID)
        {
            this.playerID = playerID;
            this.continentID = continentID;
        }
        public override bool CheckComplete()
        {
            throw new NotImplementedException();
        }
    }
}
