namespace ID_Replacement
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblUsername;
        private Label lblPassword;

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
            // Initialize components
            this.components = new System.ComponentModel.Container();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.lblUsername = new Label();
            this.lblPassword = new Label();

            // Form settings
            this.Text = "Login Page";
            this.Size = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Username label
            lblUsername.Text = "Username:";
            lblUsername.Location = new Point(50, 50);
            lblUsername.Size = new Size(80, 20);

            // Username textbox
            txtUsername.Location = new Point(150, 50);
            txtUsername.Size = new Size(180, 20);

            // Password label
            lblPassword.Text = "Password:";
            lblPassword.Location = new Point(50, 90);
            lblPassword.Size = new Size(80, 20);

            // Password textbox
            txtPassword.Location = new Point(150, 90);
            txtPassword.Size = new Size(180, 20);
            txtPassword.PasswordChar = '*';

            // Login button
            btnLogin.Text = "Login";
            btnLogin.Location = new Point(150, 130);
            btnLogin.Size = new Size(100, 30);
            btnLogin.Click += new EventHandler(btnLogin_Click);

            // Add controls to form
            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
        }
    }
}
