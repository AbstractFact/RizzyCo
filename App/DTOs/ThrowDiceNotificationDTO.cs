using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class ThrowDiceNotificationDTO
    {
        public List<int> DiceValues1 { get; set; }
        public List<int> DiceValues2 { get; set; }
        public bool Winner { get; set; }
        public int Player1ID { get; set; }
        public string Player1Color { get; set; }
        public int Player2ID { get; set; }
        public int Territory1ID { get; set; }
        public int Territory2ID { get; set; }
        public int NumArmies1 { get; set; }
        public int NumArmies2 { get; set; }
        public WinnerDTO WinnerDTO { get; set; }


    }
}
