using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FreshApp.Models
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string address { get; set; }
        public byte[] image { get; set; }
        public string expiry_date { get; set; }
        public bool status { get; set; }
        public string color { get; set; }

        public ImageSource imageSource { get; set; } = null;
    }
}
