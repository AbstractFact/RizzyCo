using System;
using System.Collections.Generic;
using System.Text;

namespace DTOs
{
    public class TransferArmiesNotificationDTO
    {
        public TransferArmiesDTO TransferInfo { get; set; }
        public string TerrFromName { get; set; }
        public string TerrToName { get; set; }
        public string PlayerUsername { get; set; }
    }
}
