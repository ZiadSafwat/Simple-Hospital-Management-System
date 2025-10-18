using System;
using System.Windows.Forms;

namespace HospitalManagementSystem.Utilities
{
    public static class FormHelper
    {
        public static void ShowSuccessMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool ShowConfirmationMessage(string message)
        {
            return MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static void ClearControls(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is TextBox textBox)
                    textBox.Clear();
                else if (ctrl is ComboBox comboBox)
                    comboBox.SelectedIndex = -1;
                else if (ctrl is DateTimePicker dateTimePicker)
                    dateTimePicker.Value = DateTime.Now;
                else if (ctrl is NumericUpDown numericUpDown)
                    numericUpDown.Value = 0;
                else if (ctrl is CheckBox checkBox)
                    checkBox.Checked = false;

                ClearControls(ctrl);
            }
        }
    }
}
