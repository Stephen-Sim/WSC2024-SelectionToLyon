using Desktop.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Desktop.UserControls
{
    public partial class ItemControl : UserControl
    {
        public event EventHandler PictureBoxClicked;

        public ItemControl()
        {
            InitializeComponent();

            pictureBox2.Click += PictureBox_Click;
        }

        private Item item;

        public Item Item
        {
            get { return item; }
            set { item = value; _ = LoadItemInfoAsync(); }
        }

        async Task LoadItemInfoAsync()
        {
            label1.Text = item.name;
            label2.Text = item.address;

            var type = item.type.Replace("&", "%26");

            using (HttpClient client = new HttpClient())
            {
                var res = await client.PostAsync($"http://localhost:5000/api/Item/GetItemTypeImage?type={type}", null);
                var result = await res.Content.ReadAsStreamAsync();
                pictureBox1.Image = Image.FromStream(result);
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBoxClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
