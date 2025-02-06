using System;
using System.Windows.Forms;
using ID_Replacement.Data.Models;

namespace ID_Replacement
{
    public partial class MainFrame : Form
    {
        private readonly Student _loggedInStudent;
        private string selectedFilePath; // Store selected file path

        public MainFrame(Student student)
        {
            InitializeComponent();
            _loggedInStudent = student;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MainFrameLoad(object sender, EventArgs e)
        {

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


    }
}
