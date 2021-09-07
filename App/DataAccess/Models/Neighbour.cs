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
