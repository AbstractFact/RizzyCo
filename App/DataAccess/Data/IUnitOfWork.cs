using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Data.EFCore;

namespace DataAccess.Data
{
    public interface IUnitOfWork : IDisposable
    {
        EfCoreCardRepository Cards { get; }
        EfCoreGameRepository Games { get; }
        EfCoreMapRepository Maps { get; }
        EfCoreMissionRepository Missions { get; }
        EfCorePlayerRepository Players { get; }
        EfCorePlayerColorRepository PlayerColors { get; }
        EfCoreTerritoryRepository Territories { get; }
        EfCoreUserRepository Users { get; }
        int Complete();
    }
}

