using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public abstract class AbstractCard
    {
        public int ID { get; set; }
        public bool Taken { get; set; }
        public string Picture { get; set; }

        //public void Take();
    }
}
