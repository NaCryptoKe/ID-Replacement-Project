using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_Replacement
{
    partial class AdminForm
    {
        private Admin admin;
        private TabControl tabControl;
        private TabPage pendingTab;
        private TabPage completedTab;
        private ListView pendingListView;
        private ListView completedListView;
        private Panel detailsPanel;
        private Label detailsLabel;
        private Button transactionButton;
        private Button viewButton;
        private Button acceptButton;
        private Button denyButton;
        private Panel buttonPanel;
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

            transactionButton = new Button
            {
                Text = "Print Report",
                Width = 100,
                Location = new Point(340, 50),
                BackColor = Color.Black,
                ForeColor = Color.White
            };

            viewButton = new Button
            {
                Text = "View File",
                Width = 100,
                Location = new Point(230, 50),
                BackColor = Color.Blue,
                ForeColor = Color.White,
                Enabled = false
            };

            acceptButton = new Button
            {
                Text = "Accept",
                Width = 100,
                Location = new Point(10, 50),
                BackColor = Color.Green,
                Enabled = false
            };

            denyButton = new Button
            {
                Text = "Deny",
                Width = 100,
                Location = new Point(120, 50),
                BackColor = Color.Red,
                Enabled = false
            };

            buttonPanel.Controls.AddRange(new Control[] { viewButton, acceptButton, denyButton, transactionButton});
            detailsPanel.Controls.AddRange(new Control[] { detailsLabel, buttonPanel });

            mainPanel.Controls.Add(tabControl, 0, 0);
            mainPanel.Controls.Add(detailsPanel, 0, 1);

            Controls.Add(mainPanel);
            MaximizeBox = false;
            MinimizeBox = false;

            SetupEventHandlers();
        }
    }
}
