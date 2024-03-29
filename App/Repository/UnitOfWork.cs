﻿using System;

using Domain.RepositoryInterfaces;
using Domain;
using DataAccess;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RizzyCoContext _context;
        private bool disposed = false;

        private ICardRepository cards;
        private IGameRepository games;
        private IMapRepository maps;
        private IMissionRepository missions;
        private IPlayerRepository players;
        private IPlayerColorRepository playerColors;
        private ITerritoryRepository territories;
        private IUserRepository users;
        private INeighbourRepository neighbours;
        private IPlayerCardRepository playerCard;
        private IContinentRepository continent;
        private IPlayerTerritoryRepository playerTerritory;
        public UnitOfWork(RizzyCoContext context)
        {
            _context = context;
            Cards = new CardRepository(_context);
            Games = new GameRepository(_context);
            Maps = new MapRepository(_context);
            Missions = new MissionRepository(_context);
            Players = new PlayerRepository(_context);
            PlayerColors = new PlayerColorRepository(_context);
            Territories = new TerritoryRepository(_context);
            Users = new UserRepository(_context);
            Neighbours = new NeighbourRepository(context);
            playerCard = new PlayerCardRepository(context);
            continent = new ContinentRepository(context);
            playerTerritory = new PlayerTerritoryRepository(context);

        }
        public ICardRepository Cards
        {
            get
            {
                if (cards == null)
                    cards = new CardRepository(_context);
                return cards;
            }
            private set { }
        }
        public IGameRepository Games
        {
            get
            {
                if (games == null)
                    games = new GameRepository(_context);
                return games;
            }
            private set { }
        }
        public IMapRepository Maps
        {
            get
            {
                if (maps == null)
                    maps = new MapRepository(_context);
                return maps;
            }
            private set { }
        }
        public IMissionRepository Missions
        {
            get
            {
                if (missions == null)
                    missions = new MissionRepository(_context);
                return missions;
            }
            private set { }
        }
        public IPlayerRepository Players 
        {
            get
            {
                if (players == null)
                    players = new PlayerRepository(_context);
                return players;
            }
            private set { }
        }
        public IPlayerColorRepository PlayerColors
        {
            get
            {
                if (playerColors == null)
                    playerColors = new PlayerColorRepository(_context);
                return playerColors;
            }
            private set { }
        }
        public ITerritoryRepository Territories
        {
            get
            {
                if (territories == null)
                    territories = new TerritoryRepository(_context);
                return territories;
            }
            private set { }
        }
        public IUserRepository Users
        {
            get
            {
                if (users == null)
                    users = new UserRepository(_context);
                return users;
            }
            private set { }
        }

        public INeighbourRepository Neighbours
        {
            get
            {
                if (neighbours == null)
                    neighbours = new NeighbourRepository(_context);
                return neighbours;
            }
            private set { }
        }

        public IPlayerCardRepository PlayerCards
        {
            get
            {
                if (playerCard == null)
                    playerCard = new PlayerCardRepository(_context);
                return playerCard;
            }
            private set { }
        }
        public IContinentRepository Continents
        {
            get
            {
                if (continent == null)
                    continent = new ContinentRepository(_context);
                return continent;
            }
            private set { }
        }
        public IPlayerTerritoryRepository PlayerTerritories
        {
            get
            {
                if (playerTerritory == null)
                    playerTerritory = new PlayerTerritoryRepository(_context);
                return playerTerritory;
            }
            private set { }
        }
        public int Complete()
        {
            return  _context.SaveChanges();
        }

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
