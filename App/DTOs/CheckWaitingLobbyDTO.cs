using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class CheckWaitingLobbyDTO
    {
        public List<string> PlayersJoined { get; set; } 
        public int GameID { get; set; }
    }
}
