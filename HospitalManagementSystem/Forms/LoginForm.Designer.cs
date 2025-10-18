using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagementSystem.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel1;

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
            this.panel1 = new Panel();
            this.label3 = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnCancel = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();

            // panel1
            this.panel1.BackColor = Color.SteelBlue;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(434, 80);
            this.panel1.TabIndex = 0;

            // label3
            this.label3.AutoSize = true;
            this.label3.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.label3.ForeColor = Color.White;
            this.label3.Location = new Point(120, 25);
            this.label3.Name = "label3";
            this.label3.Size = new Size(194, 30);
            this.label3.TabIndex = 0;
            this.label3.Text = "Hospital Login";

            // txtUsername
            this.txtUsername.Location = new Point(150, 120);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(200, 23);
            this.txtUsername.TabIndex = 1;

            // txtPassword
            this.txtPassword.Location = new Point(150, 160);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new Size(200, 23);
            this.txtPassword.TabIndex = 2;

            // btnLogin
            this.btnLogin.BackColor = Color.SteelBlue;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(150, 200);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(95, 35);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.Gray;
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(255, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(95, 35);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new Point(80, 123);
            this.label1.Name = "label1";
            this.label1.Size = new Size(63, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Username:";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new Point(80, 163);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Password:";

            // LoginForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(434, 261);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hospital Management System - Login";
            this.FormClosing += new FormClosingEventHandler(this.LoginForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
