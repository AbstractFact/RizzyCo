using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Domain.Interfaces;

namespace Domain.Models
{
    public class Territory : IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }
        public List<Territory> Neighbours { get; set; }

    }
}
