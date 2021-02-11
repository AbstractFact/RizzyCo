using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class CreateGameDTO
    {
        public List<string> Users { get; set; }
        public int MapID { get; set; }
        public string LobbyID { get; set; }
    }
}
