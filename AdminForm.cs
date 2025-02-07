using System.Diagnostics;
using ID_Replacement.Services.Interface;
using ID_Replacement.Data.Models;
using ID_Replacement.Services.Class;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Services;
using ID_Replacement.Data.Repositories.Class;

namespace ID_Replacement
{
    public partial class AdminForm : Form
    {
        private IAdminViewModelRepository _adminViewModelRepository;
        private IAdminViewModelService _adminViewModelService;
        private ITransactionLogRepository _transactionLogRepository;
        private ITransactionLogService _transactionLogService;

        public AdminForm(IAdminViewModelRepository adminViewModelRepository)
        {
            _adminViewModelRepository = adminViewModelRepository;
            _adminViewModelService = new AdminViewModelService(_adminViewModelRepository);
            SetupUI();
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

            listView.Columns.Add("Full Name", 150);
            listView.Columns.Add("Student ID", 150);
            listView.Columns.Add("Appointment Date", 150);
            listView.Columns.Add("Status", 150);
            listView.Columns.Add("Remarks", 400);

            return listView;
        }

        private void LoadRequests()
        {
            pendingListView.Items.Clear();
            completedListView.Items.Clear();

            var pendingStudents = _adminViewModelService.GetAllPendingStudents().ToList();

            if (pendingStudents != null)
            {
                foreach (var request in pendingStudents)
                {
                    AddRequestToListView(pendingListView, request);
                }
            }

            var completedStudents = _adminViewModelService.GetAllCompletedStudents().ToList();

            if (completedStudents != null)
            {
                foreach (var request in completedStudents)
                {
                    AddRequestToListView(completedListView, request);
                }
            }
        }

        private void AddRequestToListView(ListView listView, AdminViewModel students)
        {
            var item = new ListViewItem(new[]
            {
                students.FullName,
                students.StudentID,
                students.AppointmentDate?.ToShortDateString() ?? "N/A",
                students.Status.ToString(),
                students.Remarks
            });
            item.Tag = students;
            listView.Items.Add(item);
        }

        private void SetupEventHandlers()
        {
            _transactionLogRepository = new TransactionLogRepository();
            _transactionLogService = new TransactionLogService(_transactionLogRepository);

            pendingListView.ItemSelectionChanged += ListView_ItemSelectionChanged;
            completedListView.ItemSelectionChanged += ListView_ItemSelectionChanged;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;


            transactionButton.Click += (s, e) =>
            {
                try
                {
                    // Ensure directory exists
                    string folderPath = @"D:\ID_Replacement\Transaction";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Generate file name with timestamp
                    string fileName = $"Transaction_{DateTime.Now:dd_MM_yyyy_HH_mm_ss_fff}.txt";
                    string filePath = Path.Combine(folderPath, fileName);

                    // Ensure the file is not locked
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            File.Delete(filePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Could not delete existing file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Open the file for writing
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        // Write the title
                        writer.WriteLine("Transaction Report");
                        writer.WriteLine($"Generated on: {DateTime.Now:dd_MM_yyyy_HH_mm_ss_fff}");
                        writer.WriteLine(new string('-', 50)); // Adds a separator

                        // Fetch transaction logs
                        var logs = _transactionLogService.GetLogs();
                        if (logs == null)
                        {
                            MessageBox.Show("Transaction logs are NULL!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (!logs.Any())
                        {
                            writer.WriteLine("No transaction logs available!");
                        }
                        else
                        {
                            // Write column headers
                            writer.WriteLine("Log ID | Table Name | Operation | Date | User ID");
                            writer.WriteLine(new string('-', 50)); // Adds a separator

                            foreach (var log in logs)
                            {
                                writer.WriteLine($"{log.LogID?.ToString() ?? "N/A"} | " +
                                                 $"{log.TableName ?? "N/A"} | " +
                                                 $"{log.Operation ?? "N/A"} | " +
                                                 $"{log.ChangeDate?.ToString("dd-MM-yyyy HH:mm") ?? "N/A"} | " +
                                                 $"{log.UserID ?? "N/A"}");
                            }
                        }
                    }

                    MessageBox.Show($"Transaction report saved: {filePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"File access error: {ex.Message}. Ensure the file is not open elsewhere.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating text file: {ex.Message}\n\nDetails: {ex.InnerException?.Message ?? "No additional details"}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"Error creating text file: {ex.Message}\n\nDetails: {ex.InnerException?.Message ?? "No additional details"}");
                }
            };




            viewButton.Click += (s, e) =>
            {
                if (viewButton.Tag is AdminViewModel request)
                {
                    if (string.IsNullOrEmpty(request.FilePath))
                    {
                        MessageBox.Show("No file associated with this request.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!File.Exists(request.FilePath))
                    {
                        MessageBox.Show("File not found: " + request.FilePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo
                        {
                            FileName = request.FilePath,
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to open file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No request selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };


            acceptButton.Click += (s, e) =>
            {
                if (acceptButton.Tag is AdminViewModel request)
                {
                    _adminViewModelRepository.AcceptRequest(request);
                    LoadRequests();
                }
            };

            denyButton.Click += (s, e) =>
            {
                if (denyButton.Tag is AdminViewModel request)
                {
                    _adminViewModelRepository.DenyRequest(request);
                    LoadRequests();
                }
            };

            deleteButton.Click += (s, e) =>
            {
                if (deleteButton.Tag is AdminViewModel request)
                {
                    if (MessageBox.Show($"Delete request from {request.FullName}?", "Confirm",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        admin.DeleteRequest(request.StudentID);
                        LoadRequests();
                    }
                }
            };
        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected && e.Item != null)
            {
                var request = (AdminViewModel)e.Item.Tag;
                ShowDetails(request);
            }
        }

        private void ShowDetails(AdminViewModel request)
        {
            detailsLabel.Text = $"Full Name: {request.FullName}\n\n" +
                               $"Student ID: {request.StudentID}\n\n" +
                               $"Scheduled Date: {request.AppointmentDate?.ToShortDateString() ?? "N/A"}\n\n" +
                               $"Status: {request.Status}\n\n" +
                               $"Remarks: {request.Remarks}\n\n";

            bool isPending = request.Statuses == AdminViewModel.RequestStatus.Pending;
            viewButton.Enabled = true;
            acceptButton.Enabled = isPending;
            denyButton.Enabled = isPending;
            deleteButton.Enabled = !isPending;

            viewButton.Tag = request;
            acceptButton.Tag = request;
            denyButton.Tag = request;
            deleteButton.Tag = request;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            deleteButton.Visible = tabControl.SelectedTab == completedTab;
            acceptButton.Visible = tabControl.SelectedTab == pendingTab;
            denyButton.Visible = tabControl.SelectedTab == pendingTab;

            viewButton.Enabled = false;
            acceptButton.Enabled = false;
            denyButton.Enabled = false;
            deleteButton.Enabled = false;
        }
    }
}
