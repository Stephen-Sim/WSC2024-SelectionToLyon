using System;
using System.Collections.Generic;
using System.Text;

namespace FreshApp.Models
{
    public class ItemDTO
    {
        public string name { get; set; }
        public string type { get; set; }
        public string address { get; set; }
        public byte[] image { get; set; }
        public string expiry_date { get; set; }
        public long userId { get; set; }
    }
}
