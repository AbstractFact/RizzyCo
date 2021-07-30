using System.Text.Json.Serialization;
using System.Collections.Generic;
using System;

namespace DataAccess.Models
{
    public class Game 
    {
        public int ID { get; set; }
        public int NumberOfPlayers { get; set; }
        public bool Finished { get; set; }
        public DateTime CreationDate { get; set; }

        [JsonIgnore]
        public Map Map { get; set; }

    }
}
