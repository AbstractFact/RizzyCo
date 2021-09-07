using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class RizzyCoContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerColor> PlayerColors { get; set; }
        public DbSet<Territory> Territories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Neighbour> Neighbours { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<PlayerTerritory> PlayerTerritories { get; set; }
        public DbSet<PlayerCard> PlayerCards { get; set; }
        public RizzyCoContext(DbContextOptions options) : base(options)
        {
        }

    }
}
