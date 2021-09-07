using System.Collections.Generic;

namespace DTOs
{
    public class CheckWaitingLobbyDTO
    {
        public List<string> PlayersJoined { get; set; }
        public int GameID { get; set; }
    }
}
