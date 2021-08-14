using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class AddReinforcementDTO
    {
        public int GameID { get; set; }
        public int PlayerID { get; set; }
        public int TerritoryID { get; set; }
        public int NumArmies { get; set; }
    }
}
