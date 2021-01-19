using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class PlayerCard
    {
        public int ID { get; set; }
        public Player Player { get; set; }
        public Card Card { get; set; }
    }
}
