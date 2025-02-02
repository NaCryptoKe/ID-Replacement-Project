using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ID_Replacement
{
    public partial class AdminForm : Form
    {
        private Admin admin;
        private TabControl tabControl;
        private TabPage pendingTab;
        private TabPage completedTab;
        private ListView pendingListView;
        private ListView completedListView;
        private Panel detailsPanel;
        private Label detailsLabel;
        private Button viewButton;
        private Button acceptButton;
        private Button denyButton;
        private Button deleteButton;
        private Panel buttonPanel;

        public AdminForm()
        {
            admin = new Admin();
            SetupUI();
            LoadRequests();
        }

        private void SetupUI()
        {
            this.Size = new Size(1000, 800);
            this.Text = "Admin Review Panel";

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1
            };

            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));

            tabControl = new TabControl { Dock = DockStyle.Fill };

            pendingTab = new TabPage("Pending Requests");
            completedTab = new TabPage("Completed Requests");

            pendingListView = CreateListView();
            completedListView = CreateListView();

            pendingTab.Controls.Add(pendingListView);
            completedTab.Controls.Add(completedListView);

            tabControl.TabPages.Add(pendingTab);
            tabControl.TabPages.Add(completedTab);

            detailsPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            detailsLabel = new Label
            {
                AutoSize = true,
                Location = new Point(10, 10),
                Font = new Font("Arial", 10)
            };

            buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 90
            };

            viewButton = new Button
            {
                Text = "View File",
                Width = 100,
                Location = new Point(10, 10),
                BackColor = Color.Blue,
                ForeColor = Color.White,
                Enabled = false
            };

            acceptButton = new Button
            {
                Text = "Accept",
                Width = 100,
                Location = new Point(10, 50),
                BackColor = Color.Gray,
                Enabled = false
            };

            denyButton = new Button
            {
                Text = "Deny",
                Width = 100,
                Location = new Point(120, 50),
                BackColor = Color.Gray,
                Enabled = false
            };

            deleteButton = new Button
            {
                Text = "Delete",
                Width = 100,
                Location = new Point(230, 50),
                BackColor = Color.Gray,
                Enabled = false,
                Visible = false
            };

            buttonPanel.Controls.AddRange(new Control[] { viewButton, acceptButton, denyButton, deleteButton });
            detailsPanel.Controls.AddRange(new Control[] { detailsLabel, buttonPanel });

            mainPanel.Controls.Add(tabControl, 0, 0);
            mainPanel.Controls.Add(detailsPanel, 0, 1);

            this.Controls.Add(mainPanel);

            SetupEventHandlers();
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
            listView.Columns.Add("User ID", 100);
            listView.Columns.Add("Scheduled Date", 120);
            listView.Columns.Add("Status", 100);
            listView.Columns.Add("Remarks", 200);

            return listView;
        }

        private void LoadRequests()
        {
            pendingListView.Items.Clear();
            completedListView.Items.Clear();

            foreach (var request in admin.GetPendingRequests())
            {
                AddRequestToListView(pendingListView, request);
            }

            foreach (var request in admin.GetCompletedRequests())
            {
                AddRequestToListView(completedListView, request);
            }
        }

        private void AddRequestToListView(ListView listView, Admin.Request request)
        {
            var item = new ListViewItem(new[]
            {
                request.FullName,
                request.UserId,
                request.ScheduledDate.ToShortDateString(),
                request.Status.ToString(),
                request.Remarks
            });
            item.Tag = request;
            listView.Items.Add(item);
        }

        private void SetupEventHandlers()
        {
            pendingListView.ItemSelectionChanged += ListView_ItemSelectionChanged;
            completedListView.ItemSelectionChanged += ListView_ItemSelectionChanged;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            viewButton.Click += (s, e) =>
            {
                if (viewButton.Tag is Admin.Request request && !string.IsNullOrEmpty(request.FilePath))
                {
                    if (File.Exists(request.FilePath))
                    {
                        Process.Start(request.FilePath);
                    }
                    else
                    {
                        MessageBox.Show("File not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            };

            acceptButton.Click += (s, e) =>
            {
                if (acceptButton.Tag is Admin.Request request)
                {
                    admin.AcceptRequest(request.UserId);
                    LoadRequests();
                }
            };

            denyButton.Click += (s, e) =>
            {
                if (denyButton.Tag is Admin.Request request)
                {
                    admin.DenyRequest(request.UserId);
                    LoadRequests();
                }
            };

            deleteButton.Click += (s, e) =>
            {
                if (deleteButton.Tag is Admin.Request request)
                {
                    if (MessageBox.Show($"Delete request from {request.FullName}?", "Confirm",
                        MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        admin.DeleteRequest(request.UserId);
                        LoadRequests();
                    }
                }
            };
        }

        private void ListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected && e.Item != null)
            {
                var request = (Admin.Request)e.Item.Tag;
                ShowDetails(request);
            }
        }

        private void ShowDetails(Admin.Request request)
        {
            detailsLabel.Text = $"Full Name: {request.FullName}\n\n" +
                               $"User ID: {request.UserId}\n\n" +
                               $"Scheduled Date: {request.ScheduledDate.ToShortDateString()}\n\n" +
                               $"Status: {request.Status}\n\n" +
                               $"Remarks: {request.Remarks}\n\n" +
                               $"Facts: {request.Facts}";

            bool isPending = request.Status == Admin.RequestStatus.Pending;
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
