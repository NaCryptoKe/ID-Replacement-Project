using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ID_Replacement_Project
{
    public partial class V : Form
    {
        Dashboard Dashboard;
        Help Help;
        Request Request;

        public V()
        {
            InitializeComponent();
            mdiProp();
        }

        private void mdiProp()
        {
            this.SetBevel(false);
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.FromArgb(253, 247, 250);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Dashboard == null)
            {
                Dashboard = new Dashboard();
                Dashboard.FormClosed += Dashboard_FormClosed;
                Dashboard.MdiParent = this;
                Dashboard.Show();
            }
            else
            {
                Dashboard.Activate();
            }
        }

        private void Dashboard_FormClosed(object sender, EventArgs e)
        {
            Dashboard = null;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void V_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            if (Request == null)
            {
                Request = new Request();
                Request.FormClosed += Request_FormClosed;
                Request.MdiParent = this;
                Request.Show();
            }
            else
            {
                Request.Activate();
            }
        }

        private void Request_FormClosed(object sender, EventArgs e)
        {
            Request = null;
        }
    }
}
