using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class Neighbour 
    {
        public int ID { get; set; }
        public Territory Src { get; set; }
        public Territory Dst { get; set; }
    }
}
