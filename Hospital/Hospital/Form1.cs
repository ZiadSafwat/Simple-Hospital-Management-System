using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class LoginForm : Form
    {
        private DatabaseManager dbManager = new DatabaseManager();
        private Label lblTitle, lblUsername, lblPassword;
        private TextBox txtUsername, txtPassword;
        private Button btnLogin;

        public LoginForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Normal; // تكبير النافذة
        }

        private void InitializeComponent()
        {
            this.Text = "تسجيل الدخول - نظام إدارة المستشفى";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.AutoSize = false;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // إنشاء العناصر
            CreateControls();

            // أحداث التحكم في الحجم
            this.SizeChanged += LoginForm_SizeChanged;
            this.Load += LoginForm_Load;
        }

        private void CreateControls()
        {
            // عنوان النموذج - نفس التصميم
            lblTitle = new Label();
            lblTitle.Text = "تسجيل الدخول";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkBlue;
            lblTitle.Size = new Size(200, 30);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            lblUsername = new Label();
            lblUsername.Text = "اسم المستخدم";
            lblUsername.Font = new Font("Arial", 11, FontStyle.Regular);
            lblUsername.AutoSize = true; // يتسع تلقائياً للنص
            lblUsername.TextAlign = ContentAlignment.MiddleRight;

            txtUsername = new TextBox();
            txtUsername.Size = new Size(200, 25);
            txtUsername.Font = new Font("Arial", 11, FontStyle.Regular);
            txtUsername.TextAlign = HorizontalAlignment.Right;

            lblPassword = new Label();
            lblPassword.Text = "كلمة المرور";
            lblPassword.Font = new Font("Arial", 11, FontStyle.Regular);
            lblPassword.AutoSize = true; // يتسع تلقائياً للنص
            lblPassword.TextAlign = ContentAlignment.MiddleRight;

            txtPassword = new TextBox();
            txtPassword.Size = new Size(200, 25);
            txtPassword.PasswordChar = '*';
            txtPassword.Font = new Font("Arial", 11, FontStyle.Regular);
            txtPassword.TextAlign = HorizontalAlignment.Right;

            // زر تسجيل الدخول - نفس التصميم
            btnLogin = new Button();
            btnLogin.Text = "تسجيل الدخول";
            btnLogin.Size = new Size(100, 35);
            btnLogin.BackColor = Color.DodgerBlue;
            btnLogin.ForeColor = Color.White;
            btnLogin.Font = new Font("Arial", 10, FontStyle.Bold);
            btnLogin.Click += BtnLogin_Click;

            // إضافة العناصر للنموذج
            this.Controls.AddRange(new Control[] {
                lblTitle, lblUsername, txtUsername,
                lblPassword, txtPassword, btnLogin
            });
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // ترتيب العناصر عند التحميل الأول
            ArrangeControls();
        }

        private void LoginForm_SizeChanged(object sender, EventArgs e)
        {
            // إعادة ترتيب العناصر عند تغيير الحجم
            ArrangeControls();
        }

        private void ArrangeControls()
        {
            // حساب مركز النافذة
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;

            // نفس التصميم الأصلي ولكن في المركز
            lblTitle.Location = new Point(centerX - 100, centerY - 80);  // العنوان في المركز
            lblUsername.Location = new Point(centerX + 50, centerY - 30); // تسمية المستخدم (يمين)
            txtUsername.Location = new Point(centerX - 150, centerY - 30); // حقل المستخدم (يسار)
            lblPassword.Location = new Point(centerX + 50, centerY + 20);  // تسمية كلمة المرور (يمين)
            txtPassword.Location = new Point(centerX - 150, centerY + 20); // حقل كلمة المرور (يسار)
            btnLogin.Location = new Point(centerX - 50, centerY + 70);     // زر الدخول في المركز
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (dbManager.ValidateUser(txtUsername.Text, txtPassword.Text))
            {
                MessageBox.Show("تم تسجيل الدخول بنجاح!", "نجاح",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                MainMenuForm mainMenu = new MainMenuForm(dbManager);
                mainMenu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة!", "خطأ",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}