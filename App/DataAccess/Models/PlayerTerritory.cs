using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class PlayerTerritory
    {
        public int ID { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }
        public Territory Territory { get; set; }
        public int Armies { get; set; }
    }
}
