using System;
using System.Collections.Generic;
using System.Text;
using Domain.Interfaces;

namespace DataAccess.EFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RizzyCoContext _context;
        private bool disposed = false;

        private IEfCoreCardRepository cards;
        private IEfCoreGameRepository games;
        private IEfCoreMapRepository maps;
        private IEfCoreMissionRepository missions;
        private IEfCorePlayerRepository players;
        private IEfCorePlayerColorRepository playerColors;
        private IEfCoreTerritoryRepository territories;
        private IEfCoreUserRepository users;

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
        public IEfCoreCardRepository Cards
        {
            get
            {
                if (cards == null)
                    cards = new EfCoreCardRepository(_context);
                return cards;
            }
            private set { }
        }
        public IEfCoreGameRepository Games
        {
            get
            {
                if (games == null)
                    games = new EfCoreGameRepository(_context);
                return games;
            }
            private set { }
        }
        public IEfCoreMapRepository Maps
        {
            get
            {
                if (maps == null)
                    maps = new EfCoreMapRepository(_context);
                return maps;
            }
            private set { }
        }
        public IEfCoreMissionRepository Missions
        {
            get
            {
                if (missions == null)
                    missions = new EfCoreMissionRepository(_context);
                return missions;
            }
            private set { }
        }
        public IEfCorePlayerRepository Players 
        {
            get
            {
                if (players == null)
                    players = new EfCorePlayerRepository(_context);
                return players;
            }
            private set { }
        }
        public IEfCorePlayerColorRepository PlayerColors
        {
            get
            {
                if (playerColors == null)
                    playerColors = new EfCorePlayerColorRepository(_context);
                return playerColors;
            }
            private set { }
        }
        public IEfCoreTerritoryRepository Territories
        {
            get
            {
                if (territories == null)
                    territories = new EfCoreTerritoryRepository(_context);
                return territories;
            }
            private set { }
        }
        public IEfCoreUserRepository Users
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
            return  _context.SaveChanges();
        }
        //public void Dispose()
        //{
        //    _context.Dispose();
        //}

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
