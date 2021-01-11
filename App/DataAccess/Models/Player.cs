using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Player 
    {
        public int ID { get; set; }

        [JsonIgnore]
        public Game Game { get; set; }

        [JsonIgnore]
        public Mission Mission { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        public PlayerColor PlayerColor { get; set; }
        public List<Territory> Territories { get; set; }
        public List<Card> Cards { get; set; }
    }
}
