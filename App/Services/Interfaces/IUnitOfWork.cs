using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEfCoreCardRepository Cards { get; }
        IEfCoreGameRepository Games { get; }
        IEfCoreMapRepository Maps { get; }
        IEfCoreMissionRepository Missions { get; }
        IEfCorePlayerRepository Players { get; }
        IEfCorePlayerColorRepository PlayerColors { get; }
        IEfCoreTerritoryRepository Territories { get; }
        IEfCoreUserRepository Users { get; }
        public int Complete();
    }
}

