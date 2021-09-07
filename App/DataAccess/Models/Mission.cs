using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Mission
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int MissionType { get; set; }
        public string Continent1 { get; set; }
        public string Continent2 { get; set; }
        public bool Continent3 { get; set; }
        public int NumTerritories { get; set; }
        public string TargetPlayerColor { get; set; }

        [JsonIgnore]
        public Map Map { get; set; }

    }
}
