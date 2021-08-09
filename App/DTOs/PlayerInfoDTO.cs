﻿using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class PlayerInfoDTO
    {
        public int PlayerID { get; set; }
        public string PlayerColor { get; set; }
        public string Mission { get; set; }
        public int AvailableArmies { get; set; }
        public List<GameParticipantInfoDTO> Participants {get;set;}
        public PlayerInfoDTO(Player player, List<GameParticipantInfoDTO> participants)
        {
            this.PlayerID = player.ID;
            this.PlayerColor = player.PlayerColor.Value;
            this.Mission = player.Mission.Description;
            this.AvailableArmies = player.AvailableArmies;
            this.Participants = participants;
        }
    }
}
