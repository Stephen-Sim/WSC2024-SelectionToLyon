using System;
using System.Collections.Generic;
using System.Text;

namespace FreshApp.Models
{
    public class Captcha
    {
        public byte[] Image { get; set; }
        public string Key { get; set; }
    }
}
