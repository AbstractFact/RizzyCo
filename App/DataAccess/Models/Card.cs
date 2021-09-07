using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Card
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Picture { get; set; }
        public Territory Territory { get; set; }

        [JsonIgnore]
        public Map Map { get; set; }
    }
}
