﻿using System;
using System.Windows.Forms;
using ID_Replacement.Data;
using ID_Replacement.Data.Models;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Service.Interface;
using ID_Replacement.Services.Class;
using Microsoft.Data.SqlClient;

namespace ID_Replacement
{
    public partial class MainFrame : Form
    {
        private readonly Student _loggedInStudent;
        private string selectedFilePath; // Store selected file path
        private HashSet<DateTime> overbookedDates;
        private IHistoryRepository _historyRepository;
        private IHistoryService _historyService;
        int maxDays = 365; // Limit search to 1 year
        int daysChecked = 0;

        public MainFrame(Student student, IHistoryRepository historyRepository)
        {
            InitializeComponent();
            _loggedInStudent = student;
            _historyRepository = historyRepository;
            _historyService = new HistoryService(_historyRepository);
            LoadRequests();
        }

        private ListView CreateListView()
        {
            ListView listView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false
            };

            listView.Columns.Add("Submitted Date", 200);
            listView.Columns.Add("Appointment Date", 200);
            listView.Columns.Add("Status", 300);

            return listView;
        }

        private void LoadRequests()
        {
            historyListView.Items.Clear();
            var history = _historyService.GetHistoryByStudentID(_loggedInStudent.StudentID);

            if (history != null)
            {
                foreach (var request in history)
                {
                    AddRequestToListView(historyListView, request);
                }
            }
        }

        private void AddRequestToListView(ListView listView, History history)
        {
            var item = new ListViewItem(new[]
            {
                history.RequestDate?.ToShortDateString() ?? "N/A",
                history.AppointmentDate?.ToShortDateString() ?? "N/A",
                history.Status
            });
            item.Tag = history;
            listView.Items.Add(item);
        }

        private void requestIdButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = requestTab;
        }

        private void AttachDocumentButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg)|*.jpg|PDF Files (*.pdf)|*.pdf";
                openFileDialog.Title = "Select a Document";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                    MessageBox.Show($"Selected File: {selectedFilePath}", "File Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            // Check if a document has been selected
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Please attach a document before submitting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the reason text box is not empty
            if (string.IsNullOrWhiteSpace(reasonTextBox.Text))
            {
                MessageBox.Show("Please provide a reason before submitting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if a date is selected from the calendar
            DateTime selectedDate = calendar.SelectionStart;
            if (selectedDate == DateTime.MinValue)
            {
                MessageBox.Show("Please select a date before submitting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Display preview of entered details
            string previewMessage = $"Document: {selectedFilePath}\nReason: {reasonTextBox.Text}\nDate: {selectedDate.ToShortDateString()}";
            MessageBox.Show(previewMessage, "Submission Preview", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DisableOverbookedDates()
        {
            overbookedDates = new HashSet<DateTime>(GetOverbookedDates());

            foreach (var date in overbookedDates)
            {
                calendar.AddBoldedDate(date); // Visually mark overbooked dates
            }

            calendar.UpdateBoldedDates();
        }

        private List<DateTime> GetOverbookedDates()
        {
            List<DateTime> overbookedDates = new List<DateTime>();

            try
            {
                using (var connection = DatabaseContext.Instance.GetConnection())
                {
                    connection.Open();
                    string query = @"SELECT OverbookedDate FROM dbo.GetOverbookedDates()";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader[0] != DBNull.Value)
                                    overbookedDates.Add(Convert.ToDateTime(reader[0]));
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Database Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }

            return overbookedDates;
        }

        private DateTime GetNextAvailableAppointment()
        {
            DateTime nextAvailable = DateTime.Today;

            while ((overbookedDates.Contains(nextAvailable) || IsWeekend(nextAvailable)) && daysChecked < maxDays)
            {
                nextAvailable = nextAvailable.AddDays(1);
                daysChecked++;
            }
            if (daysChecked == maxDays)
            {
                throw new Exception("No available appointment dates found within the next year.");
            }

            return nextAvailable;
        }

        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        private void Calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            if (IsWeekend(e.Start))
            {
                MessageBox.Show("This date falls on a weekend. Please select another date.");
                calendar.SelectionStart = GetNextAvailableAppointment();
            }
            
            if (overbookedDates.Contains(e.Start))
            {
                MessageBox.Show("This date is fully booked. Please select another date.");
                calendar.SelectionStart = GetNextAvailableAppointment();
            }
        }
    }
}
