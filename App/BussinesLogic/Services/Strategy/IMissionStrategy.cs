using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Services.Strategy
{
    public interface IMissionStrategy
    {
        Task<bool> CheckComplete(int playerID);
    }
}
