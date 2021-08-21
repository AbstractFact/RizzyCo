using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class TransferArmiesDTO
    {
        public int GameID { get; set; }
        public int TerrFromID { get; set; }
        public int TerrToID { get; set; }
        public int PlayerID { get; set; }
        public int NumArmies { get; set; }
    }
}
