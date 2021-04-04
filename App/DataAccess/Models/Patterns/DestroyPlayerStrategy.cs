using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class DestroyPlayerStrategy : AbstractMissionStrategy
    {
        private string color;
        public DestroyPlayerStrategy(int playerID, string color)
        {
            this.playerID = playerID;
            this.color = color;
        }
        public override bool CheckComplete()
        {
            throw new NotImplementedException();
        }
    }
}
