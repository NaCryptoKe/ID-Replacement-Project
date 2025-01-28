using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ID_Rplacment;
using Microsoft.Data.SqlClient;

namespace ID_Replacement
{
    public partial class MainFrame : Form
    {
        private string attachedFilePath;
        private List<AppointmentRecord> appointments;

        private readonly SqlConnection _connection = new SqlConnection("Data Source=QUANTUMEDGE\\SQLEXPRESS;Initial Catalog=IDRepSysstem;Integrated Security=True;Trust Server Certificate=True");

        // Model for appointment data
        public class AppointmentRecord
        {
            public DateTime RequestDate { get; set; }
            public DateTime AppointmentDate { get; set; }
            public string Status { get; set; }
        }

        public MainFrame()
        {
            appointments = new List<AppointmentRecord>();
            InitializeComponents();
            HookEventHandlers();
            LoadAppointments(); // Load appointments from the database
        }

        private void HookEventHandlers()
        {
            btnNewAppointment.Click += btnNewAppointment_Click;
            requestCalendar.DateSelected += requestCalendar_DateSelected;
            btnAttachDocument.Click += btnAttachDocument_Click;
            btnSubmitRequest.Click += btnSubmitRequest_Click;
        }

        private void btnNewAppointment_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = requestTab;
        }

        private void requestCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (e.Start >= DateTime.Today.AddDays(1))
            {
                btnSubmitRequest.Enabled = true;
            }
            else
            {
                btnSubmitRequest.Enabled = false;
                MessageBox.Show("Please select a future date", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAttachDocument_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                attachedFilePath = openFileDialog.FileName;

                lblDocumentPath.Text = $"Selected: {attachedFilePath}";
                lblDocumentPath.ForeColor = Color.Black;

                // Adjust the height dynamically to fit the text
                lblDocumentPath.Height = CalculateLabelHeight(lblDocumentPath, attachedFilePath);
                MessageBox.Show("Document attached successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblDocumentPath.Text = "No document selected";
                lblDocumentPath.ForeColor = Color.Gray;
                lblDocumentPath.Height = 30; // Reset the height
            }
        }

        // Helper function to calculate label height
        private int CalculateLabelHeight(Label label, string text)
        {
            using (Graphics g = label.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(text, label.Font, label.MaximumSize.Width);
                return (int)Math.Ceiling(textSize.Height) + 50; // Add padding
            }
        }

        private void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Please provide a reason for your request.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (attachedFilePath == null)
            {
                MessageBox.Show("Please attach the required document.", "Missing Document", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Open the database connection
                _connection.Open();

                using (var transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        // Insert into IDRequests table
                        var requestId = 0;
                        string insertRequestQuery = @"
                    INSERT INTO IDRequests (StudentID, Status, NotificationSent) 
                    OUTPUT INSERTED.RequestID
                    VALUES (@StudentID, @Status, 0)";

                        using (SqlCommand cmd = new SqlCommand(insertRequestQuery, _connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@StudentID", GlobalVariables.LoggedInStudentID);
                            cmd.Parameters.AddWithValue("@Status", "Pending");

                            requestId = (int)cmd.ExecuteScalar(); // Retrieve the inserted RequestID
                        }

                        // Insert into Appointments table
                        string insertAppointmentQuery = @"
                    INSERT INTO Appointments (RequestID, AppointmentDate) 
                    VALUES (@RequestID, @AppointmentDate)";

                        using (SqlCommand cmd = new SqlCommand(insertAppointmentQuery, _connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@RequestID", requestId);
                            cmd.Parameters.AddWithValue("@AppointmentDate", requestCalendar.SelectionStart);

                            cmd.ExecuteNonQuery();
                        }

                        // Insert into Documents table
                        string insertDocumentQuery = @"
                    INSERT INTO Documents (RequestID, DocumentPath) 
                    VALUES (@RequestID, @DocumentPath)";

                        using (SqlCommand cmd = new SqlCommand(insertDocumentQuery, _connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@RequestID", requestId);
                            cmd.Parameters.AddWithValue("@DocumentPath", attachedFilePath);

                            cmd.ExecuteNonQuery();
                        }

                        // Insert into TransactionLogs table
                        string logTransactionQuery = @"
                    INSERT INTO TransactionLogs (TableName, Operation, ChangeDate, Details, UserID) 
                    VALUES (@TableName, @Operation, GETDATE(), @Details, @UserID)";

                        using (SqlCommand cmd = new SqlCommand(logTransactionQuery, _connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@TableName", "IDRequests, Appointments, Documents");
                            cmd.Parameters.AddWithValue("@Operation", "Insert");
                            cmd.Parameters.AddWithValue("@Details", $"New request created with RequestID {requestId}");
                            cmd.Parameters.AddWithValue("@UserID", GlobalVariables.LoggedInStudentID);

                            cmd.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();

                        // Add new appointment to the local list
                        var newAppointment = new AppointmentRecord
                        {
                            RequestDate = DateTime.Now,
                            AppointmentDate = requestCalendar.SelectionStart,
                            Status = "Pending"
                        };

                        appointments.Add(newAppointment);
                        UpdateAppointmentDisplay();

                        MessageBox.Show("Request submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Reset fields
                        tabControl.SelectedTab = dashboardTab;
                        attachedFilePath = null;
                        btnSubmitRequest.Enabled = false;
                        txtReason.Text = "";
                    }
                    catch (Exception ex)
                    {
                        // Roll back the transaction in case of an error
                        transaction.Rollback();
                        MessageBox.Show($"Error submitting the request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        private void UpdateAppointmentDisplay()
        {
            appointmentGrid.Rows.Clear();

            foreach (var appointment in appointments.OrderByDescending(a => a.AppointmentDate))
            {
                appointmentGrid.Rows.Add(
                    appointment.RequestDate.ToString("MMMM dd, yyyy HH:mm"),
                    appointment.AppointmentDate.ToString("MMMM dd, yyyy"),
                    appointment.Status
                );
            }

            if (appointments.Any())
            {
                var nextAppointment = appointments
                    .Where(a => a.AppointmentDate >= DateTime.Today)
                    .OrderBy(a => a.AppointmentDate)
                    .FirstOrDefault();

                lblAppointment.Text = nextAppointment != null
                    ? $"Next Appointment: {nextAppointment.AppointmentDate.ToString("MMMM dd, yyyy")}"
                    : "No upcoming appointments";
            }
            else
            {
                lblAppointment.Text = "No upcoming appointments";
            }
        }

        private void LoadAppointments()
        {
            try
            {
                // Open the database connection
                _connection.Open();

                string query = @"
                    SELECT 
                        IDRequests.RequestDate,
                        Appointments.AppointmentDate,
                        IDRequests.Status
                    FROM 
                        IDRequests
                    INNER JOIN 
                        Appointments ON IDRequests.RequestID = Appointments.RequestID
                    WHERE 
                        IDRequests.StudentID = @StudentID";

                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@StudentID", GlobalVariables.LoggedInStudentID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        appointments.Clear();
                        while (reader.Read())
                        {
                            appointments.Add(new AppointmentRecord
                            {
                                RequestDate = reader.GetDateTime(0),
                                AppointmentDate = reader.GetDateTime(1),
                                Status = reader.GetString(2)
                            });
                        }
                    }
                }

                UpdateAppointmentDisplay(); // Update the UI with the retrieved appointments
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
    }
}
