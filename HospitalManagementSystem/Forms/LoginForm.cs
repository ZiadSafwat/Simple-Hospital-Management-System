using System;
using System.Windows.Forms;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utilities;

namespace HospitalManagementSystem.Forms
{
    public partial class LoginForm : Form
    {
        private AuthService _authService;

        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                FormHelper.ShowErrorMessage("Please enter both username and password.");
                return;
            }

            try
            {
                var user = _authService.Login(username, password);

                if (user != null)
                {
                    FormHelper.ShowSuccessMessage($"Welcome {user.Username}!");
                    MainForm mainForm = new MainForm(user);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    FormHelper.ShowErrorMessage("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                FormHelper.ShowErrorMessage($"Login failed: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}
