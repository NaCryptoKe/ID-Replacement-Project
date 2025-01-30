using ID_Replacement.Data;
using ID_Replacement.Data.Models;
using Microsoft.Data.SqlClient;

namespace ID_Replacement
{
    public partial class MainFrame : Form
    {
        private readonly Student _loggedInStudent;
        private readonly SqlConnection _connection;
        private string attachedFilePath;

        public MainFrame(Student student)
        {
            InitializeComponents();
            _loggedInStudent = student;
            _connection = DatabaseContext.Instance.GetConnection();
        }

        /// <summary>
        /// Open the new appointment request tab.
        /// </summary>
        private void btnNewAppointment_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = requestTab;
        }

        /// <summary>
        /// Validate selected date before allowing request submission.
        /// </summary>
        private void requestCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            btnSubmitRequest.Enabled = e.Start >= DateTime.Today.AddDays(1);
            if (!btnSubmitRequest.Enabled)
            {
                MessageBox.Show("Please select a future date", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Attach a document for ID replacement.
        /// </summary>
        private void btnAttachDocument_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Submit a new ID replacement request.
        /// </summary>
        private void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            
        }
    }
}
