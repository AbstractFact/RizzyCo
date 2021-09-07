using System.Collections.Generic;

namespace DTOs
{
    public class CreateGameDTO
    {
        public List<string> Users { get; set; }
        public int MapID { get; set; }
    }
}
