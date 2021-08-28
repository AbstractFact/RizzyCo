using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Services.Strategy
{
    public class MissionContext: IMissionContext
    {
        private IMissionStrategy _strategy;

        public MissionContext()
        { }

        public MissionContext(IMissionStrategy strategy)
        {
            this._strategy = strategy;
        }

        public void SetStrategy(IMissionStrategy strategy)
        {
            this._strategy = strategy;
        }

        public async Task<bool> CheckComplete(int playerID)
        {
            return await this._strategy.CheckComplete(playerID);
        }
    }
}
