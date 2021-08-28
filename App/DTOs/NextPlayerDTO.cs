using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class NextPlayerDTO
    {
        public int NextPlayerID { get; set; }
        public string NextPlayerUsername { get; set; }
        public int Bonus { get; set; }
        public NextPlayerDTO() { }
    }
}
