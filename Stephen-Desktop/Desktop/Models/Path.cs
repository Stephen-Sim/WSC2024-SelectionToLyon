using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Models
{
    public class Path
    {
        public decimal TotalCost { get; set; }
        public int TotalDay { get; set; }
        public List<long> PathTo = new List<long>();
    }
}
