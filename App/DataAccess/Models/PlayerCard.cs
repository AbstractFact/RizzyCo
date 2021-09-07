namespace DataAccess.Models
{
    public class PlayerCard
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public Player Player { get; set; }
        public Card Card { get; set; }
    }
}
