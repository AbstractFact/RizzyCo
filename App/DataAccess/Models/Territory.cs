using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Territory 
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public Continent Continent { get; set; }

    }
}
