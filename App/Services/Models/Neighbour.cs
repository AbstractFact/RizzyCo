using System;
using System.Collections.Generic;
using System.Text;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Neighbour : IEntity
    {
        public int ID { get; set; }
        public Territory Src { get; set; }
        public Territory Dst { get; set; }
    }
}
