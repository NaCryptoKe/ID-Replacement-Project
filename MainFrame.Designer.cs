namespace ID_Replacement
{
    partial class MainFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
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

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            tabControl1 = new TabControl();
            dashboardTab = new TabPage();
            historyPanel = new Panel();
            historyGrid = new DataGridView();
            historyLabel = new Label();
            requestIdButton = new Button();
            requestTab = new TabPage();
            submitButton = new Button();
            reasonTextBox = new RichTextBox();
            reasonLabel = new Label();
            attachDocumentButton = new Button();
            calendar = new MonthCalendar();
            tabControl1.SuspendLayout();
            dashboardTab.SuspendLayout();
            historyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)historyGrid).BeginInit();
            requestTab.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(dashboardTab);
            tabControl1.Controls.Add(requestTab);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 450);
            tabControl1.TabIndex = 0;
            // 
            // dashboardTab
            // 
            dashboardTab.Controls.Add(historyPanel);
            dashboardTab.Controls.Add(historyLabel);
            dashboardTab.Controls.Add(requestIdButton);
            dashboardTab.Font = new Font("Segoe UI", 12F);
            dashboardTab.Location = new Point(4, 29);
            dashboardTab.Name = "dashboardTab";
            dashboardTab.Size = new Size(792, 417);
            dashboardTab.TabIndex = 0;
            dashboardTab.Text = "Dashboard";
            dashboardTab.UseVisualStyleBackColor = true;
            // 
            // historyPanel
            // 
            historyPanel.AutoScroll = true;
            historyPanel.Controls.Add(historyGrid);
            historyPanel.Dock = DockStyle.Bottom;
            historyPanel.Location = new Point(0, 82);
            historyPanel.Name = "historyPanel";
            historyPanel.Size = new Size(792, 335);
            historyPanel.TabIndex = 0;
            // 
            // historyGrid
            // 
            historyGrid.AllowUserToAddRows = false;
            historyGrid.AllowUserToDeleteRows = false;
            historyGrid.AllowUserToResizeColumns = false;
            historyGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            historyGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            historyGrid.ColumnHeadersHeight = 29;
            historyGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            historyGrid.Dock = DockStyle.Fill;
            historyGrid.Location = new Point(0, 0);
            historyGrid.Name = "historyGrid";
            historyGrid.RowHeadersWidth = 51;
            historyGrid.Size = new Size(792, 335);
            historyGrid.TabIndex = 0;
            // 
            // historyLabel
            // 
            historyLabel.AutoSize = true;
            historyLabel.Location = new Point(8, 48);
            historyLabel.Name = "historyLabel";
            historyLabel.Size = new Size(75, 28);
            historyLabel.TabIndex = 1;
            historyLabel.Text = "History";
            // 
            // requestIdButton
            // 
            requestIdButton.Dock = DockStyle.Top;
            requestIdButton.Location = new Point(0, 0);
            requestIdButton.Name = "requestIdButton";
            requestIdButton.Size = new Size(792, 42);
            requestIdButton.TabIndex = 2;
            requestIdButton.Text = "Request An ID";
            requestIdButton.UseVisualStyleBackColor = true;
            // 
            // requestTab
            // 
            requestTab.Controls.Add(submitButton);
            requestTab.Controls.Add(reasonTextBox);
            requestTab.Controls.Add(reasonLabel);
            requestTab.Controls.Add(attachDocumentButton);
            requestTab.Controls.Add(calendar);
            requestTab.Font = new Font("Segoe UI", 12F);
            requestTab.Location = new Point(4, 29);
            requestTab.Name = "requestTab";
            requestTab.Size = new Size(792, 417);
            requestTab.TabIndex = 1;
            requestTab.Text = "Request";
            requestTab.UseVisualStyleBackColor = true;
            // 
            // submitButton
            // 
            submitButton.Font = new Font("Segoe UI", 10.2F);
            submitButton.Location = new Point(483, 219);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(118, 40);
            submitButton.TabIndex = 0;
            submitButton.Text = "Submit";
            submitButton.UseVisualStyleBackColor = true;
            // 
            // reasonTextBox
            // 
            reasonTextBox.Location = new Point(308, 43);
            reasonTextBox.Name = "reasonTextBox";
            reasonTextBox.Size = new Size(452, 164);
            reasonTextBox.TabIndex = 1;
            reasonTextBox.Text = "";
            // 
            // reasonLabel
            // 
            reasonLabel.AutoSize = true;
            reasonLabel.Location = new Point(308, 3);
            reasonLabel.Name = "reasonLabel";
            reasonLabel.Size = new Size(74, 28);
            reasonLabel.TabIndex = 2;
            reasonLabel.Text = "Reason";
            // 
            // attachDocumentButton
            // 
            attachDocumentButton.Font = new Font("Segoe UI", 10.2F);
            attachDocumentButton.Location = new Point(23, 219);
            attachDocumentButton.Name = "attachDocumentButton";
            attachDocumentButton.Size = new Size(178, 40);
            attachDocumentButton.TabIndex = 3;
            attachDocumentButton.Text = "Attach Document";
            attachDocumentButton.UseVisualStyleBackColor = true;
            // 
            // calendar
            // 
            calendar.Location = new Point(0, 0);
            calendar.Name = "calendar";
            calendar.TabIndex = 4;
            // 
            // MainFrame
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainFrame";
            Text = "MainFrame";
            tabControl1.ResumeLayout(false);
            dashboardTab.ResumeLayout(false);
            dashboardTab.PerformLayout();
            historyPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)historyGrid).EndInit();
            requestTab.ResumeLayout(false);
            requestTab.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
