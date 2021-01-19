using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Mission 
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int MissionNum { get; set; }

        [JsonIgnore]
        public Map Map { get; set; }

    }
}
