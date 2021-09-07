using System;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Game
    {
        public int ID { get; set; }
        public int NumberOfPlayers { get; set; }
        public bool Finished { get; set; }
        public DateTime CreationDate { get; set; }
        public int Stage { get; set; }

        [JsonIgnore]
        public Map Map { get; set; }
    }
}
