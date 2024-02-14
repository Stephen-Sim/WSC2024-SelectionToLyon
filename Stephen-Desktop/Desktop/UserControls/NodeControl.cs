using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop.UserControls
{
    public partial class NodeControl : UserControl
    {
        private string location;

        public string Location
        {
            get { return location; }
            set { location = value; label1.Text = location; }
        }

        public NodeControl()
        {
            InitializeComponent();
        }
    }
}
