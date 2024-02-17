using Desktop.Models;
using Desktop.UserControls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Desktop
{
    public partial class Form1 : Desktop.Core
    {
        public WSC2024Selection_Desktop_StephenEntities ent { get; set; }

        public Form1()
        {
            InitializeComponent();
            ent = new WSC2024Selection_Desktop_StephenEntities();
            loadInitData();

            this.panel1.Visible = false;
            this.panel2.Visible = false;
        }

        public List<Item> Items { get; set; }
        public string token { get; set; }

        async void loadInitData()
        {
            string input = "logistics is the future.";

            using (var sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                token = sb.ToString();
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("token", token);
                var res1 = await client.GetStringAsync("http://localhost:5000/api/Item/GetItems");

                Items = JsonConvert.DeserializeObject<List<Item>>(res1);
                
                var res = await client.GetStringAsync("http://localhost:5000/api/Item/GetItemTypes");
                var itemTypes = JsonConvert.DeserializeObject<List<string>>(res);

                comboBox1.DataSource = itemTypes;
                comboBox1.SelectedIndex = -1;
            }
        }

        void loadData()
        {
            var temps = comboBox1.SelectedIndex == -1 ? Items.Where(x => !x.status).ToList() : Items.Where(x => x.type == (string)comboBox1.SelectedValue && !x.status).OrderBy(x => x.expiry_date).ToList();

            flowLayoutPanel1.Controls.Clear();

            foreach (var item in temps)
            {
                var itemControl = new ItemControl();
                itemControl.Item = item;

                itemControl.PictureBoxClicked += (s, ev) =>
                {
                    var uc = s as ItemControl;
                    if (uc != null)
                    {
                        SelectedItem = uc.Item;
                        loadSelectedItem();
                    }
                };

                flowLayoutPanel1.Controls.Add(itemControl);
            }
        }

        public Item SelectedItem { get; set; } = null;
        public Path BestPath { get; set; } = null;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        void loadSelectedItem()
        {
            flowLayoutPanel2.Controls.Clear();
            BestPath = null;

            Graph g = new Graph(ent.routes.Count() * 2);

            var routes = ent.routes.ToList();

            // add all edges
            foreach (var route in routes)
            {
                g.AddEdge(route.location.id, route.location1.id);
                g.AddEdge(route.location1.id, route.location.id);
            }

            List<List<long>> _paths = g.FindAllPaths(SelectedItem.departure_location_id, SelectedItem.arrival_location_id);

            // add all path
            var Paths = new List<Path>();

            foreach (var _path in _paths)
            {
                var path = new Path();
                var totalcost = 0.0m;
                var day = 0;

                for (int i = 0; i < _path.Count - 1; i++)
                {
                    var route = ent.routes.ToList().FirstOrDefault(x => (x.location1Id == _path[i] && x.location2Id == _path[i + 1])
                    || (x.location1Id == _path[i + 1] && x.location2Id == _path[i]));

                    if (route != null)
                    {
                        totalcost += route.cost;
                        day += route.duration;
                    }
                }

                path.TotalCost = totalcost;
                path.TotalDay = day;
                path.PathTo = _path;

                Paths.Add(path);
            }

            BestPath = Paths.Where(x => (new DateTime(2024, 2, 27).AddDays(x.TotalDay)).Date <= SelectedItem.expiry_date.Date).OrderBy(x => x.TotalCost).FirstOrDefault();

            label2.Text = $"Name: {SelectedItem.name}";
            label3.Text = $"Type: {SelectedItem.type.Replace("&", "&&")}";
            
            if (BestPath != null)
            {
                label4.Text = $"Estimated Cost: RM {BestPath.TotalCost.ToString("0.00")}";
                label5.Text = $"Estimated Delivered Date: {new DateTime(2024, 2, 27).AddDays(BestPath.TotalDay).ToString("dd/MM/yyyy")}";

                button1.Text = $"Complete Delivery";

                for (int i = 0; i < BestPath.PathTo.Count; i++)
                {
                    var location = ent.locations.ToList().FirstOrDefault(x => x.id == BestPath.PathTo[i]);

                    var node = new NodeControl();
                    node.Location = location.name;

                    flowLayoutPanel2.Controls.Add(node);

                    if (i != BestPath.PathTo.Count - 1)
                    {
                        var edge = new EdgeControl();

                        flowLayoutPanel2.Controls.Add(edge);
                    }
                }

                this.panel2.Visible = true;
            }
            else
            {
                label4.Text = "This item is not able to deliver within the date.";
                label5.Text = string.Empty;

                button1.Text = $"Suspend Delivery";

                this.panel2.Visible = false;
            }

            this.panel1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure to update the status?", "", MessageBoxButtons.YesNo);
            
            if (dialogResult == DialogResult.Yes)
            {
                var smtp = new SmtpClient()
                {
                    Port = 25,
                    Host = "localhost"
                };

                MailMessage message= new MailMessage()
                {
                    From = new MailAddress("admin@logis.my"),
                    To =
                    {
                        SelectedItem.email
                    },
                    IsBodyHtml = true, 
                };

                if (button1.Text == "Complete Delivery")
                {
                    message.Subject = "Complete Delivery";

                    message.Body = $"<p>Dear {SelectedItem.receiver_name},</p><br>" +
                        $"<p>We are pleased to inform you that the item, <strong>{SelectedItem.name} </strong>" +
                        $"was successfully delivered on {new DateTime(2024, 2, 27).AddDays(BestPath.TotalDay).ToString("dd/MM/yyyy")}." +
                        $"The total cost of the delivery amounted to RM {BestPath.TotalCost.ToString("0.00")}.</p>" +
                        $"<br><p>Thank you for your business with us.</p>";
                }
                else if (button1.Text == "Suspend Delivery")
                {
                    message.Subject = "Suspend Delivery";

                    message.Body = $"<p>Dear {SelectedItem.receiver_name},</p><br>" +
                        $"<p>We regret to inform you that we are unable to deliver the item, <strong>{SelectedItem.name}</strong>" +
                        $", within the expected timeframe. We sincerely apologize for any inconvenience this may cause.</p>" +
                        $"<br><p>Thank you for your understanding.</p>";
                }

                smtp.Send(message);

                var temp = Items;
                var item = temp.FirstOrDefault(x => x.id == SelectedItem.id);
                item.status = true;

                MessageBox.Show("Item Status is udpated. An email is sent to the reciever.");

                this.panel1.Visible = false;
                loadData();
            }
        }
    }
}
