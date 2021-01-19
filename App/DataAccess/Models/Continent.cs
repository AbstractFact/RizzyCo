using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Continent
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumTerritories { get; set; }
        public int Value { get; set; }

        
        public Map Map { get; set; }
    }
}
