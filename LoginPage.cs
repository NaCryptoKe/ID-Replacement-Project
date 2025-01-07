using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ID_Replacement_Project
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=QUANTUMEDGE\SQLEXPRESS;Initial Catalog=StudentIDCardSystem;Integrated Security=True;");

        private void button1_Click(object sender, EventArgs e)
        {
            string username, password;

            username = usernameText.Text;
            password = passwordText.Text;

            try
            {
                string querry = "select * from Students where StudentID = '" + usernameText.Text + "' and Password = '" + passwordText.Text + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(querry, conn);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0 )
                {
                    username = usernameText.Text;
                    password = passwordText.Text;

                    MainForm dashboard = new MainForm();
                    dashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    usernameText.Clear();
                    passwordText.Clear();

                    usernameText.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                conn.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginPage_Load(object sender, EventArgs e)
        {

        }
    }
}
