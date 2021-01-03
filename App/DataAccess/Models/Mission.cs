using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using DataAccess.Data;

namespace DataAccess.Models
{
    public class Mission : IEntity
    {
        public int ID { get; set; }
        public string Description { get; set; }

    }
}
