using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class GameInfoDTO
    { 
        public DateTime CreationDate { get; set; }
        public List<GameParticipantInfoDTO> Participants { get; set; }

    }
}
