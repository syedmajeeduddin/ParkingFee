using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RateType
    {
        public string Name { get; set; }

        public string EntryTime { get; set; }

        public string ExitTime { get; set; }

        public double Charge { get; set; }

        public DateTime? ActualDate  { get; set; }

        
    }
}
