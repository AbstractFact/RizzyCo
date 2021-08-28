using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class ThrowDiceDTO
    {
        public int GameID { get; set; }
        public int MapID { get; set; }
        public int NumDice1 { get; set; }
        public int NumDice2 { get; set; }
        public int Territory1ID { get; set; }
        public int Territory2ID { get; set; }
        public int Player1ID { get; set; }
        public int Player2ID { get; set; }
    }
}
