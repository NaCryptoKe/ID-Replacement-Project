using System;
using System.Windows.Forms;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Services.Interface;

namespace ID_Replacement
{
    public partial class LoginForm : Form
    {
        private readonly IStudentService _studentService;
        private IHistoryRepository _historyRepository;

        public LoginForm(IStudentService studentService)
        {
            InitializeComponent();
            _studentService = studentService;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                if (_studentService.ValidateCredentials(username, password))
                {
                    var student = _studentService.GetStudentById(username);
                    if (student != null)
                    {
                        this.Hide();
                        new MainFrame(student, _historyRepository).ShowDialog(); // Pass student object
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error retrieving student details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid credentials.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login failed: {ex.Message}");
            }
        }
    }
}
