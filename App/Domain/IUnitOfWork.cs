using System;
using System.Collections.Generic;
using System.Text;
using Domain.RepositoryInterfaces;

namespace Domain
{
    public interface IUnitOfWork : IDisposable
    {
        ICardRepository Cards { get; }
        IGameRepository Games { get; }
        IMapRepository Maps { get; }
        IMissionRepository Missions { get; }
        IPlayerRepository Players { get; }
        IPlayerColorRepository PlayerColors { get; }
        ITerritoryRepository Territories { get; }
        IUserRepository Users { get; }
        INeighbourRepository Neighbours { get; }
        IGameUserRepository GamesUser { get; }
        IContinentRepository Continents { get; }
        IGamePlayerColorRepository GamePlayerColors { get; }
        public int Complete();

    }
}

