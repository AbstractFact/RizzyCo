using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Map 
    { 
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumberOfContinents { get; set; }
        public int NumberOfTerritories { get; set; }
        public int NumberOfAvailableArmies { get; set; }

        public List<Mission> Missions { get; set; }
        public List<Card> Cards { get; set; }
        public List<Continent> Continents { get; set; }

    }
}
