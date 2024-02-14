using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Models
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string receiver_name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public long departure_location_id { get; set; }
        public long arrival_location_id { get; set; }
        public DateTime expiry_date { get; set; }
        public string type { get; set; }
        public bool status { get; set; } = false;
    }
}
