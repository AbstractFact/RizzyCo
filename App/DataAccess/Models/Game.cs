using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public class Game 
    {
        public int ID { get; set; }
        public int NumberOfPlayers { get; set; }
        public bool Finished { get; set; }

        [JsonIgnore]
        public Map Map { get; set; }

        [JsonIgnore]
        public User Creator { get; set; }

        public List<Player> Players { get; set; }

    }
}
