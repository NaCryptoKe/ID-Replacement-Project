using System;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace ID_Replacement
{
    public partial class Form1 : Form
    {
        private string _username, _password;
        public Form1()
        {
            InitializeComponent(); // Initialize form components
        }

        SqlConnection connectionString = new SqlConnection("Data Source=QUANTUMEDGE\\SQLEXPRESS;Initial Catalog=IDRepSysstem;Integrated Security=True;Trust Server Certificate=True");

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _username = txtUsername.Text;
            _password = txtPassword.Text;

            try
            {
                string querry = $"Select * from Students where StudentID = '{_username}' and Password = '{_password}'";
                SqlDataAdapter adapter = new SqlDataAdapter(querry, connectionString);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0 )
                {
                    MainFrame mainFrame = new MainFrame();
                    mainFrame.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error", "Invalid Log In Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtPassword.Clear();

                    //To focus on the username
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", "Error connecting to the server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectionString.Close();
            }
        }
    }
}
