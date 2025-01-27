using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ID_Replacement
{
    public partial class MainFrame : Form
    {
        private string attachedFilePath;
        private List<AppointmentRecord> appointments;

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
                MessageBox.Show("Document attached successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Please provide a reason for your request.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Add new appointment
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
        }
    }
}
