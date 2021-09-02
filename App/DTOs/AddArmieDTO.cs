using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class AddArmieDTO
    {
        public int TerritoryID { get; set; }
        public int NumArmies { get; set; }
        public string NextPlayer { get; set; }
        public string PrevPlayer { get; set; }
        public string TerritoryName { get; set; }
    }
}
