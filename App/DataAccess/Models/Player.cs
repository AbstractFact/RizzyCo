using System.Text.Json.Serialization;

namespace DataAccess.Models
{
    public class Player
    {
        public int ID { get; set; }
        public bool Creator { get; set; }
        public int OnTurn { get; set; }
        public int AvailableArmies { get; set; }
        public bool WonCard { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }
        public PlayerColor PlayerColor { get; set; }
        public Mission Mission { get; set; }

    }
}
