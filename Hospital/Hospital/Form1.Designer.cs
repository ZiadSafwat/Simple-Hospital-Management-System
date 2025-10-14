using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class MainMenuForm : Form
    {
        private DatabaseManager dbManager;
        private Label lblTitle;
        private Button btnPatients, btnDoctors, btnAppointments, btnReports, btnExit;

        public MainMenuForm(DatabaseManager dbManager)
        {
            this.dbManager = dbManager;
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized; // تغيير من Normal إلى Maximized
        }

        private void InitializeComponent()
        {
            this.Text = "القائمة الرئيسية - نظام إدارة المستشفى";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.AutoSize = false;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.FormClosed += (s, e) => Application.Exit();

            CreateControls();
            this.SizeChanged += MainMenuForm_SizeChanged;
            this.Load += MainMenuForm_Load;
        }

        private void CreateControls()
        {
            // عنوان النموذج
            lblTitle = new Label();
            lblTitle.Text = "القائمة الرئيسية";
            lblTitle.Font = new Font("Arial", 18, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkBlue;
            lblTitle.Size = new Size(300, 40);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // زر المرضى
            btnPatients = CreateMenuButton("المرضى");
            btnPatients.Click += (s, e) => ShowPatients();

            // زر الأطباء
            btnDoctors = CreateMenuButton("الأطباء");
            btnDoctors.Click += (s, e) => ShowDoctors();

            // زر المواعيد
            btnAppointments = CreateMenuButton("المواعيد");
            btnAppointments.Click += (s, e) => ShowAppointments();

            // زر التقارير
            btnReports = CreateMenuButton("التقارير");
            btnReports.Click += (s, e) => ShowMessage("التقارير والإحصائيات");

            // زر الخروج
            btnExit = new Button();
            btnExit.Text = "خروج";
            btnExit.Size = new Size(100, 35);
            btnExit.BackColor = Color.OrangeRed;
            btnExit.ForeColor = Color.White;
            btnExit.Font = new Font("Arial", 10, FontStyle.Bold);
            btnExit.Click += (s, e) => Application.Exit();

            // إضافة العناصر للنموذج
            this.Controls.AddRange(new Control[] {
                lblTitle, btnPatients, btnDoctors,
                btnAppointments, btnReports, btnExit
            });
        }

        private Button CreateMenuButton(string text)
        {
            Button button = new Button();
            button.Text = text;
            button.Size = new Size(200, 50);
            button.BackColor = Color.DodgerBlue;
            button.ForeColor = Color.White;
            button.Font = new Font("Arial", 12, FontStyle.Bold);
            button.TextAlign = ContentAlignment.MiddleCenter;
            return button;
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            ArrangeControls();
        }

        private void MainMenuForm_SizeChanged(object sender, EventArgs e)
        {
            ArrangeControls();
        }

        private void ArrangeControls()
        {
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            // ترتيب جميع العناصر في المركز
            lblTitle.Location = new Point(centerX - 150, centerY - 150);
            btnPatients.Location = new Point(centerX - 100, centerY - 80);
            btnDoctors.Location = new Point(centerX - 100, centerY - 10);
            btnAppointments.Location = new Point(centerX - 100, centerY + 60);
            btnReports.Location = new Point(centerX - 100, centerY + 130);
            btnExit.Location = new Point(centerX - 50, centerY + 200);
        }

        private void ShowPatients()
        {
            var patients = dbManager.GetPatients();
            string message = $"عدد المرضى: {patients.Rows.Count}\n\n";

            foreach (DataRow row in patients.Rows)
            {
                message += $"الاسم: {row["Name"]} - الهاتف: {row["Phone"]}\n";
            }

            MessageBox.Show(message, "قائمة المرضى", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowDoctors()
        {
            var doctors = dbManager.GetDoctors();
            string message = $"عدد الأطباء: {doctors.Rows.Count}\n\n";

            foreach (DataRow row in doctors.Rows)
            {
                message += $"الاسم: {row["Name"]} - التخصص: {row["Specialization"]}\n";
            }

            MessageBox.Show(message, "قائمة الأطباء", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowAppointments()
        {
            var appointments = dbManager.GetAppointments();
            string message = $"عدد المواعيد: {appointments.Rows.Count}\n\n";

            foreach (DataRow row in appointments.Rows)
            {
                message += $"المريض: {row["PatientName"]} - الطبيب: {row["DoctorName"]}\n";
            }

            MessageBox.Show(message, "قائمة المواعيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowMessage(string moduleName)
        {
            MessageBox.Show($"تم فتح وحدة: {moduleName}\n\nهذه نسخة تجريبية",
                          moduleName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}