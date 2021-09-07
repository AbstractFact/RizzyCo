namespace DTOs
{
    public class AttackDTO
    {
        public int PlayerID { get; set; }
        public string PlayerUsername { get; set; }
        public int AttackFromID { get; set; }
        public string AttackFromName { get; set; }
        public int TargetID { get; set; }
        public int NumDice { get; set; }
        public int GameID { get; set; }
    }
}
