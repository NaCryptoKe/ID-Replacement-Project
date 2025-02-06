namespace ID_Replacement
{
    partial class MainFrame
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabControl1;
        private TabPage dashboardTab;
        private Label historyLabel;
        private Button requestIdButton;
        private TabPage requestTab;
        private Panel historyPanel;
        private DataGridView historyGrid;
        private Button attachDocumentButton;
        private MonthCalendar calendar;
        private Button submitButton;
        private RichTextBox reasonTextBox;
        private Label reasonLabel;
        private ListView historyListView;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl1 = new TabControl
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Name = "tabControl1",
                SelectedIndex = 0,
                Size = new Size(800, 450)
            };

            dashboardTab = new TabPage
            {
                Font = new Font("Segoe UI", 12F),
                Location = new Point(4, 29),
                Name = "dashboardTab",
                Size = new Size(792, 417),
                Text = "Dashboard",
                UseVisualStyleBackColor = true
            };

            historyPanel = new Panel
            {
                AutoScroll = true,
                Dock = DockStyle.Bottom,
                Location = new Point(0, 82),
                Name = "historyPanel",
                Size = new Size(792, 335)
            };

            historyListView = CreateListView();
            historyPanel.Controls.Add(historyListView);

            historyLabel = new Label
            {
                AutoSize = true,
                Location = new Point(8, 48),
                Name = "historyLabel",
                Size = new Size(75, 28),
                Text = "History"
            };

            requestIdButton = new Button
            {
                Dock = DockStyle.Top,
                Location = new Point(0, 0),
                Name = "requestIdButton",
                Size = new Size(792, 42),
                Text = "Request An ID",
                UseVisualStyleBackColor = true
            };
            requestIdButton.Click += requestIdButton_Click;

            dashboardTab.Controls.Add(historyPanel);
            dashboardTab.Controls.Add(historyLabel);
            dashboardTab.Controls.Add(requestIdButton);

            requestTab = new TabPage
            {
                Font = new Font("Segoe UI", 12F),
                Location = new Point(4, 29),
                Name = "requestTab",
                Size = new Size(792, 417),
                Text = "Request",
                UseVisualStyleBackColor = true
            };

            submitButton = new Button
            {
                Font = new Font("Segoe UI", 10.2F),
                Location = new Point(483, 219),
                Name = "submitButton",
                Size = new Size(118, 40),
                Text = "Submit",
                UseVisualStyleBackColor = true
            };
            submitButton.Click += SubmitButton_Click;

            reasonTextBox = new RichTextBox
            {
                Location = new Point(308, 43),
                Name = "reasonTextBox",
                Size = new Size(452, 164)
            };

            reasonLabel = new Label
            {
                AutoSize = true,
                Location = new Point(308, 3),
                Name = "reasonLabel",
                Size = new Size(74, 28),
                Text = "Reason"
            };

            attachDocumentButton = new Button
            {
                Font = new Font("Segoe UI", 10.2F),
                Location = new Point(23, 219),
                Name = "attachDocumentButton",
                Size = new Size(178, 40),
                Text = "Attach Document",
                UseVisualStyleBackColor = true
            };
            attachDocumentButton.Click += AttachDocumentButton_Click;

            calendar = new MonthCalendar
            {
                Location = new Point(0, 0),
                Name = "calendar"
            };

            requestTab.Controls.Add(submitButton);
            requestTab.Controls.Add(reasonTextBox);
            requestTab.Controls.Add(reasonLabel);
            requestTab.Controls.Add(attachDocumentButton);
            requestTab.Controls.Add(calendar);

            tabControl1.Controls.Add(dashboardTab);
            tabControl1.Controls.Add(requestTab);

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "MainFrame";
            Text = "MainFrame";
        }
    }
}
