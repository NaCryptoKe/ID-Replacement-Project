using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Data.Repositories.Class;
using ID_Replacement.Services.Class;
using ID_Replacement.Services.Interface;
using ID_Replacement.Data.Models;

namespace ID_Replacement
{
    public partial class AdminForm : Form
    {
        private IAdminViewModelRepository _adminViewModelRepository;
        private IAdminViewModelService _adminViewModelService;

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
            listView.Columns.Add("Student ID", 100);
            listView.Columns.Add("Appointment Date", 120);
            listView.Columns.Add("Status", 100);
            listView.Columns.Add("Remarks", 200);

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
            pendingListView.ItemSelectionChanged += ListView_ItemSelectionChanged;
            completedListView.ItemSelectionChanged += ListView_ItemSelectionChanged;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            viewButton.Click += (s, e) =>
            {
                if (viewButton.Tag is AdminViewModel request && !string.IsNullOrEmpty(request.FilePath))
                {
                    if (File.Exists(request.FilePath))
                    {
                        Console.WriteLine("Test");
                    }
                    else
                    {
                        MessageBox.Show("File not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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
