using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class PlayerTerritory
    {
        public int ID { get; set; }
        public Player Player { get; set; }
        public Territory Territory { get; set; }
        public int Armies { get; set; }
    }
}
