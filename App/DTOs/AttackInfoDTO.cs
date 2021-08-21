using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class AttackInfoDTO
    {
        public int PlayerAttackedID { get; set; }
        public string PlayerAttackedName { get; set; }
        public int NumDice { get; set; }
        public string TargetPlayer { get; set; }
        public int AttackFromTerritory { get; set; }
        public string AttackFromTerritoryName { get; set; }
        public int TargetTerritory { get; set; }
        public string TargetTerritoryName { get; set; }
    }
}
