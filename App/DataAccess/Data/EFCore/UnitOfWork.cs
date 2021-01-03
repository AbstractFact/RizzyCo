using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.EFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RizzyCoContext _context;

        private EfCoreCardRepository cards;
        private EfCoreGameRepository games;
        private EfCoreMapRepository maps;
        private EfCoreMissionRepository missions;
        private EfCorePlayerRepository players;
        private EfCorePlayerColorRepository playerColors;
        private EfCoreTerritoryRepository territories;
        private EfCoreUserRepository users;

        //private bool disposed = false;
        public UnitOfWork(RizzyCoContext context)
        {
            _context = context;
            Cards = new EfCoreCardRepository(_context);
            Games = new EfCoreGameRepository(_context);
            Maps = new EfCoreMapRepository(_context);
            Missions = new EfCoreMissionRepository(_context);
            Players = new EfCorePlayerRepository(_context);
            PlayerColors = new EfCorePlayerColorRepository(_context);
            Territories = new EfCoreTerritoryRepository(_context);
            Users = new EfCoreUserRepository(_context);
 
        }
        public EfCoreCardRepository Cards
        {
            get
            {
                if (cards == null)
                    cards = new EfCoreCardRepository(_context);
                return cards;
            }
            private set { }
        }
        public EfCoreGameRepository Games
        {
            get
            {
                if (games == null)
                    games = new EfCoreGameRepository(_context);
                return games;
            }
            private set { }
        }
        public EfCoreMapRepository Maps
        {
            get
            {
                if (maps == null)
                    maps = new EfCoreMapRepository(_context);
                return maps;
            }
            private set { }
        }
        public EfCoreMissionRepository Missions
        {
            get
            {
                if (missions == null)
                    missions = new EfCoreMissionRepository(_context);
                return missions;
            }
            private set { }
        }
        public EfCorePlayerRepository Players 
        {
            get
            {
                if (players == null)
                    players = new EfCorePlayerRepository(_context);
                return players;
            }
            private set { }
        }
        public EfCorePlayerColorRepository PlayerColors
        {
            get
            {
                if (playerColors == null)
                    playerColors = new EfCorePlayerColorRepository(_context);
                return playerColors;
            }
            private set { }
        }
        public EfCoreTerritoryRepository Territories
        {
            get
            {
                if (territories == null)
                    territories = new EfCoreTerritoryRepository(_context);
                return territories;
            }
            private set { }
        }
        public EfCoreUserRepository Users
        {
            get
            {
                if (users == null)
                    users = new EfCoreUserRepository(_context);
                return users;
            }
            private set { }
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
