using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class PlayerTerritoryDTO
    {
        public int TerritoryID { get; set; }
        public string TerritoryName { get; set; }
        public int NumArmies { get; set; }
        public string PlayerColor { get; set; }

        public PlayerTerritoryDTO(PlayerTerritory playerTerr)
        {
            this.TerritoryID = playerTerr.Territory.ID;
            this.TerritoryName = playerTerr.Territory.Name;
            this.NumArmies = playerTerr.Armies;
            this.PlayerColor = playerTerr.Player.PlayerColor.Value;
        }

    }
}
