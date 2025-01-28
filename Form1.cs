using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ID_Rplacment;
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

        // Corrected variable name and connection initialization
        private readonly SqlConnection _connection = new SqlConnection("Data Source=QUANTUMEDGE\\SQLEXPRESS;Initial Catalog=IDRepSysstem;Integrated Security=True;Trust Server Certificate=True");

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _username = txtUsername.Text.Trim(); // Trim whitespace
            _password = txtPassword.Text.Trim();

            try
            {
                // Open the connection before executing the query
                _connection.Open();

                string query = @"
            SELECT StudentID, FullName FROM Students 
            WHERE (Email = @username AND Password = @password) 
               OR (StudentID = @username AND Password = @password)";

                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    // Use parameterized queries to prevent SQL injection
                    command.Parameters.AddWithValue("@username", _username);
                    command.Parameters.AddWithValue("@password", _password);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Store the StudentID and FullName in GlobalVariables
                        GlobalVariables.LoggedInStudentID = dt.Rows[0]["StudentID"].ToString();
                        GlobalVariables.LoggedInStudentName = dt.Rows[0]["FullName"].ToString();

                        MainFrame mainFrame = new MainFrame();
                        mainFrame.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Log In Credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUsername.Clear();
                        txtPassword.Clear();
                        txtUsername.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                // Show exception details for debugging purposes (optional)
                MessageBox.Show($"Error connecting to the server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the connection in the finally block to ensure it is closed even if an exception occurs
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

    }
}
