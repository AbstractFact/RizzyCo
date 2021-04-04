using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public abstract class AbstractMissionStrategy
    {
        protected int playerID;

        public abstract bool CheckComplete();
    }
}
