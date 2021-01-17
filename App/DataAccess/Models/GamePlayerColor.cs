using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class GamePlayerColor
    {
        public int ID { get; set; }
        public Game Game { get; set; }
        public PlayerColor PlayerColor { get; set; }
        public bool Available { get; set; }
    }
}
