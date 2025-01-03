namespace ID_Replacement_Project
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.lblDashboardHistory = new System.Windows.Forms.Label();
            this.btnDashboardRequest = new System.Windows.Forms.Button();
            this.lblDashboardTopic = new System.Windows.Forms.Label();
            this.lblDashboardSubHeader = new System.Windows.Forms.Label();
            this.lblDashboardHeader = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDashboardHistory
            // 
            this.lblDashboardHistory.AutoSize = true;
            this.lblDashboardHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDashboardHistory.Location = new System.Drawing.Point(119, 531);
            this.lblDashboardHistory.Name = "lblDashboardHistory";
            this.lblDashboardHistory.Size = new System.Drawing.Size(333, 54);
            this.lblDashboardHistory.TabIndex = 10;
            this.lblDashboardHistory.Text = "Your Requests";
            // 
            // btnDashboardRequest
            // 
            this.btnDashboardRequest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(36)))), ((int)(((byte)(114)))));
            this.btnDashboardRequest.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboardRequest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.btnDashboardRequest.Location = new System.Drawing.Point(131, 367);
            this.btnDashboardRequest.Name = "btnDashboardRequest";
            this.btnDashboardRequest.Size = new System.Drawing.Size(247, 54);
            this.btnDashboardRequest.TabIndex = 9;
            this.btnDashboardRequest.Text = "Start a Request";
            this.btnDashboardRequest.UseVisualStyleBackColor = false;
            // 
            // lblDashboardTopic
            // 
            this.lblDashboardTopic.AutoSize = true;
            this.lblDashboardTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDashboardTopic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(145)))), ((int)(((byte)(166)))));
            this.lblDashboardTopic.Location = new System.Drawing.Point(125, 288);
            this.lblDashboardTopic.Name = "lblDashboardTopic";
            this.lblDashboardTopic.Size = new System.Drawing.Size(904, 32);
            this.lblDashboardTopic.TabIndex = 8;
            this.lblDashboardTopic.Text = "Lost or damaged your ID card? No problem. You can request anew one";
            // 
            // lblDashboardSubHeader
            // 
            this.lblDashboardSubHeader.AutoSize = true;
            this.lblDashboardSubHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblDashboardSubHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDashboardSubHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(73)))), ((int)(((byte)(92)))));
            this.lblDashboardSubHeader.Location = new System.Drawing.Point(119, 214);
            this.lblDashboardSubHeader.Name = "lblDashboardSubHeader";
            this.lblDashboardSubHeader.Size = new System.Drawing.Size(506, 54);
            this.lblDashboardSubHeader.TabIndex = 7;
            this.lblDashboardSubHeader.Text = "Request a replacement";
            // 
            // lblDashboardHeader
            // 
            this.lblDashboardHeader.AutoSize = true;
            this.lblDashboardHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDashboardHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(19)))), ((int)(((byte)(0)))));
            this.lblDashboardHeader.Location = new System.Drawing.Point(112, 106);
            this.lblDashboardHeader.Name = "lblDashboardHeader";
            this.lblDashboardHeader.Size = new System.Drawing.Size(821, 91);
            this.lblDashboardHeader.TabIndex = 6;
            this.lblDashboardHeader.Text = "ID Card Replacement";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.lblDashboardHistory);
            this.Controls.Add(this.btnDashboardRequest);
            this.Controls.Add(this.lblDashboardTopic);
            this.Controls.Add(this.lblDashboardSubHeader);
            this.Controls.Add(this.lblDashboardHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formDashboardLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDashboardHistory;
        private System.Windows.Forms.Button btnDashboardRequest;
        private System.Windows.Forms.Label lblDashboardTopic;
        private System.Windows.Forms.Label lblDashboardSubHeader;
        private System.Windows.Forms.Label lblDashboardHeader;
    }
}