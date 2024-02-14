using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desktop
{
    public partial class Core : Form
    {
        PrivateFontCollection pfc1 = new PrivateFontCollection();
        PrivateFontCollection pfc2 = new PrivateFontCollection();

        public Core()
        {
            InitializeComponent();
        }

        private void Core_Load(object sender, EventArgs e)
        {
            try
            {
                this.closeButton.BackColor = Color.FromArgb(229, 26, 46);
                this.headerPanel.BackColor = Color.FromArgb(229, 26, 46);
                this.BackColor = Color.FromArgb(187, 187, 187);

                pfc1.AddFontFile("WSC2022SE_TP09_Gotham-Bold_actual_en.otf");
                pfc2.AddFontFile("OpenSans-Regular.ttf");

                this.headerPanel.Font = new Font(pfc1.Families[0], 9f);
                this.Font = new Font(pfc2.Families[0], 8.25f);
            }
            catch (Exception)
            {

            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
