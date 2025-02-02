using System;
using System.Windows.Forms;
using ID_Replacement.Data.Models;

namespace ID_Replacement
{
    public partial class MainFrame : Form
    {
        private readonly Student _loggedInStudent;

        public MainFrame(Student student)
        {
            InitializeComponent();
            _loggedInStudent = student;
            this.Load += new EventHandler(MainFrameLoad);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainFrameLoad(object sender, EventArgs e)
        {
            historyGrid.ColumnCount = 3;
            historyGrid.Columns[0].Name = "Request Date";
            historyGrid.Columns[1].Name = "Appointment Date";
            historyGrid.Columns[2].Name = "Status";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
