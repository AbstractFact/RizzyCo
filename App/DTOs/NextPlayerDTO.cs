namespace DTOs
{
    public class NextPlayerDTO
    {
        public int NextPlayerID { get; set; }
        public string NextPlayerUsername { get; set; }
        public int Bonus { get; set; }
        public GetCardDTO Card { get; set; }
        public NextPlayerDTO() { }
    }
}
