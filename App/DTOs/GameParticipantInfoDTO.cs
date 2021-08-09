using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class GameParticipantInfoDTO
    {
        public string Username { get; set; }
        public string PlayerColor { get; set; }
        public bool OnTurn { get; set; }
    }
}
