using System.Text.Json.Serialization;
using System.Collections.Generic;
using DataAccess.Data;

namespace DataAccess.Models
{
    public class Game : IEntity
    {
        public int ID { get; set; }
        public int NumberOfPlayers { get; set; }
        public bool Finished { get; set; }

        [JsonIgnore]
        public Map Map { get; set; }
        public List<Player> Players { get; set; }
        public List<PlayerColor> PlayerColors { get; set; }

    }
}