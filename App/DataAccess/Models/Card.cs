using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Card 
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Picture { get; set; }

        [JsonIgnore]
        public Player Player { get; set; }

        [JsonIgnore]
        public Territory Territory { get; set; }
    }
}
