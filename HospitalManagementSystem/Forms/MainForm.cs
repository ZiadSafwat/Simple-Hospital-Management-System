using System;

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HospitalManagementSystem.Forms;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem
{
    public partial class MainForm : Form
    {
        private User _currentUser;
        private Panel sidebar;
        private Panel topBar;
        private Panel statusBar;
        private Label lblTitle;
        private Label lblStatus;

        public MainForm(User user)
        {
            InitializeComponent();
            _currentUser = user;
            InitializeCustomUI();
        }

        private void InitializeCustomUI()
        {
          
            this.Text = "Hospital Management System";
            this.BackColor = Color.FromArgb(40, 40, 45);
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;

            
            sidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 200,
                BackColor = Color.FromArgb(30, 30, 35)
            };
            this.Controls.Add(sidebar);

            
            topBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(50, 50, 55)
            };
            lblTitle = new Label
            {
                Text = "🏥 Hospital Management System",
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            topBar.Controls.Add(lblTitle);
            this.Controls.Add(topBar);
            topBar.BringToFront();

           
            statusBar = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 25,
                BackColor = Color.FromArgb(25, 25, 30)
            };
            lblStatus = new Label
            {
                Text = $"Logged in as {_currentUser?.Username ?? "Guest"} ({_currentUser?.Role ?? "N/A"})",
                Dock = DockStyle.Fill,
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleLeft
            };
            statusBar.Controls.Add(lblStatus);
            this.Controls.Add(statusBar);

           
            AddSidebarButton("Patients", OpenPatients);
            AddSidebarButton("Doctors", OpenDoctors);
            AddSidebarButton("Appointments", OpenAppointments);
            AddSidebarButton("Reports", OpenReports);
            AddSidebarButton("Logout", Logout);

           
            FadeInForm();
        }

        private void AddSidebarButton(string text, Action onClick)
        {
            Button btn = new Button
            {
                Text = text,
                Dock = DockStyle.Top,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(45, 45, 50),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(70, 70, 80);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.FromArgb(45, 45, 50);
            btn.Click += (s, e) => onClick();
            sidebar.Controls.Add(btn);
            sidebar.Controls.SetChildIndex(btn, 0);
        }

        private void OpenPatients() => OpenChildForm(new PatientsForm());
        private void OpenDoctors() => OpenChildForm(new DoctorsForm());
        private void OpenAppointments() => OpenChildForm(new AppointmentsForm());
        private void OpenReports() => OpenChildForm(new ReportsForm());

        private void Logout()
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                LoginForm login = new LoginForm();
                login.Show();
            }
        }

        private void OpenChildForm(Form childForm)
        {
            foreach (Form frm in this.MdiChildren)
                frm.Close();

            childForm.MdiParent = this;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }

        private void FadeInForm()
        {
            this.Opacity = 0;
            Timer fadeTimer = new Timer();
            fadeTimer.Interval = 15;
            fadeTimer.Tick += (s, e) => {
                if (this.Opacity < 1)
                    this.Opacity += 0.05;
                else
                    fadeTimer.Stop();
            };
            fadeTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                Color.FromArgb(45, 45, 48),
                Color.FromArgb(28, 28, 30), 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            if (this.MdiChildren.Length == 0)
            {
                using (Font font = new Font("Segoe UI", 32, FontStyle.Bold))
                {
                    TextRenderer.DrawText(e.Graphics, "Welcome to Hospital Management System",
                        font, new Point(250, 300), Color.LightGray);
                }
            }
        }
    }
}
