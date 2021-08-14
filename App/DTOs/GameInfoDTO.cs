using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class GameInfoDTO
    { 
        public int GameID { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Finished { get; set; }
        public int MapID { get; set; }
        public List<GameParticipantInfoDTO> Participants { get; set; }
    }
}
