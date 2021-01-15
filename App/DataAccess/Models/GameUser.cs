using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class GameUser
    {
        public int ID { get; set; }
        public User User { get; set; }
        public Game Game { get; set; }
    }
}
