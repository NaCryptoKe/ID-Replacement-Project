using System.Globalization;

namespace ID_Replacement
{
    partial class MainFrame
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabControl;
        private TabPage dashboardTab;
        private TabPage requestTab;
        private MonthCalendar requestCalendar;
        private Button btnSubmitRequest;
        private Label lblTitle;
        private Label lblAppointment;
        private Label lblDocumentPath;
        private Button btnNewAppointment;
        private Button btnAttachDocument;
        private OpenFileDialog openFileDialog;
        private DataGridView appointmentGrid;
        private Panel scrollPanel;
        private TextBox txtReason;

        
        private void InitializeComponents()
        {
            components = new System.ComponentModel.Container();
            this.Size = new Size(800, 600);
            this.Text = "ID Replacement System";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Tab control
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill
            };

            dashboardTab = new TabPage("Dashboard");
            requestTab = new TabPage("New Request");

            // Initialize dashboard tab components
            lblTitle = new Label
            {
                Text = "ID REPLACEMENT",
                Location = new Point(20, 20),
                Size = new Size(700, 40),
                Font = new Font("Arial", 24, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            lblAppointment = new Label
            {
                Text = "No upcoming appointments",
                Location = new Point(20, 80),
                Size = new Size(700, 30),
                Font = new Font("Arial", 14, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnNewAppointment = new Button
            {
                Text = "Schedule New Appointment",
                Location = new Point(250, 130),
                Size = new Size(250, 40),
                Font = new Font("Arial", 12, FontStyle.Regular)
            };

            scrollPanel = new Panel
            {
                Location = new Point(20, 190),
                Size = new Size(740, 330),
                AutoScroll = true
            };

            appointmentGrid = new DataGridView
            {
                Size = new Size(720, 310),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BorderStyle = BorderStyle.None,
                BackgroundColor = Color.White
            };

            appointmentGrid.Columns.Add("RequestDate", "Request Date");
            appointmentGrid.Columns.Add("AppointmentDate", "Appointment Date");
            appointmentGrid.Columns.Add("Status", "Status");

            // Add components to the dashboard tab
            dashboardTab.Controls.Add(lblTitle);
            dashboardTab.Controls.Add(lblAppointment);
            dashboardTab.Controls.Add(btnNewAppointment);
            scrollPanel.Controls.Add(appointmentGrid);
            dashboardTab.Controls.Add(scrollPanel);

            // Initialize request tab components
            requestCalendar = new MonthCalendar
            {
                Location = new Point(20, 20),
                MaxSelectionCount = 1,
                MinDate = DateTime.Today.AddDays(1)
            };

            btnAttachDocument = new Button
            {
                Text = "Attach Document",
                Location = new Point(20, 200),
                Size = new Size(150, 30)
            };

            Label lblReason = new Label
            {
                Text = "Reason for Request:",
                Location = new Point(300, 20),
                Size = new Size(200, 20),
                Font = new Font("Arial", 10)
            };

            txtReason = new TextBox
            {
                Location = new Point(300, 45),
                Size = new Size(400, 250),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Arial", 10)
            };

            btnSubmitRequest = new Button
            {
                Text = "Submit Request",
                Location = new Point(300, 310),
                Size = new Size(150, 30),
                Enabled = false
            };

            openFileDialog = new OpenFileDialog
            {
                Filter = "All Files (*.*)|*.*",
                Title = "Select Document to Attach"
            };

            lblDocumentPath = new Label
            {
                Text = "No document selected",
                Location = new Point(requestCalendar.Location.X, requestCalendar.Location.Y + requestCalendar.Height + 40), // Place it below the calendar
                Size = new Size(requestCalendar.Width, 30), // Match the calendar's width initially
                Font = new Font("Arial", 10, FontStyle.Italic),
                ForeColor = Color.Gray,
                AutoSize = false, // Disable auto-sizing to enable word wrapping
                MaximumSize = new Size(requestCalendar.Width, 0) // Allow unlimited height but limit the width to the calendar
            };

            // Add components to the request tab
            requestTab.Controls.Add(requestCalendar);
            requestTab.Controls.Add(btnAttachDocument);
            requestTab.Controls.Add(lblReason);
            requestTab.Controls.Add(txtReason);
            requestTab.Controls.Add(btnSubmitRequest);
            requestTab.Controls.Add(lblDocumentPath);

            // Add tabs to tab control
            tabControl.TabPages.Add(dashboardTab);
            tabControl.TabPages.Add(requestTab);

            this.Controls.Add(tabControl);
        }

        /// <summary>
        /// Dispose method for cleaning up resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
