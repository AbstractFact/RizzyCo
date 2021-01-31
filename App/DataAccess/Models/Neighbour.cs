using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Neighbour 
    {
        public int ID { get; set; }
        [JsonIgnore]
        public Territory Src { get; set; }
        public Territory Dst { get; set; }
    }
}
