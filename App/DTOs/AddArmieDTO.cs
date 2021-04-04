using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class AddArmieDTO
    {
        public string PlayerName { get; set; }
        public int TerritoryID { get; set; }
        public string TerritoryName { get; set; }

        public AddArmieDTO(PlayerTerritory pt)
        {
            this.PlayerName = pt.Player.User.Username;
            this.TerritoryID = pt.Territory.ID;
            this.TerritoryName = pt.Territory.Name;
        }
    }
}
