using System;
using System.Windows.Forms;
using HospitalManagementSystem.Forms;

namespace HospitalManagementSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

           
            try
            {
                using (HospitalDbContext context = new HospitalDbContext())
                {
                    context.Database.Initialize(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database initialization failed: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.Run(new LoginForm());
        }
    }
}