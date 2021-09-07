using DataAccess.Models;
using System.Collections.Generic;

namespace DTOs
{
    public class PlayerInfoDTO
    {
        public int PlayerID { get; set; }
        public string PlayerColor { get; set; }
        public string Mission { get; set; }
        public int AvailableArmies { get; set; }
        public bool OnTurn { get; set; }
        public List<GameParticipantInfoDTO> Participants { get; set; }
        public PlayerInfoDTO(Player player, List<GameParticipantInfoDTO> participants)
        {
            this.PlayerID = player.ID;
            this.PlayerColor = player.PlayerColor.Value;
            this.Mission = player.Mission.Description;
            this.AvailableArmies = player.AvailableArmies;
            this.OnTurn = player.OnTurn == 0;
            this.Participants = participants;
        }
    }
}
