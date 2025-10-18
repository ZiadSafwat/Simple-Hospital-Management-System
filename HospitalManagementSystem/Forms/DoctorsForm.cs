using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utilities;

namespace HospitalManagementSystem.Forms
{
    public partial class DoctorsForm : Form
    {
        private DoctorService _doctorService;
        private BindingList<Doctor> _doctors;
        private Doctor _currentDoctor;

        public DoctorsForm()
        {
            InitializeComponent();
            _doctorService = new DoctorService();
            LoadDoctors();
            LoadSpecializations();
        }

        private void LoadDoctors()
        {
            var doctors = _doctorService.GetAllDoctors();
            _doctors = new BindingList<Doctor>(doctors);
            dataGridViewDoctors.DataSource = _doctors;
            UpdateDoctorCount();
            FormatGrid();
        }

        private void LoadSpecializations()
        {
            var specializations = _doctorService.GetAllSpecializations();
            cmbSpecializationFilter.Items.Clear();
            cmbSpecializationFilter.Items.Add("All Specializations");
            cmbSpecializationFilter.Items.AddRange(specializations.ToArray());
            cmbSpecializationFilter.SelectedIndex = 0;
        }

        private void UpdateDoctorCount()
        {
            lblDoctorCount.Text = $"Total Doctors: {_doctors.Count}";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateDoctorForm())
            {
                var doctor = new Doctor
                {
                    Name = txtName.Text.Trim(),
                    Specialization = cmbSpecialization.Text,
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Department = txtDepartment.Text.Trim(),
                    Qualification = txtQualification.Text.Trim(),
                    ExperienceYears = string.IsNullOrEmpty(txtExperience.Text) ? (int?)null : int.Parse(txtExperience.Text),
                    ConsultationFee = string.IsNullOrEmpty(txtFee.Text) ? (decimal?)null : decimal.Parse(txtFee.Text),
                    IsAvailable = chkAvailable.Checked
                };

                try
                {
                    _doctorService.AddDoctor(doctor);
                    FormHelper.ShowSuccessMessage("Doctor added successfully!");
                    LoadDoctors();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    FormHelper.ShowErrorMessage($"Error adding doctor: {ex.Message}");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_currentDoctor == null)
            {
                FormHelper.ShowErrorMessage("Please select a doctor to update.");
                return;
            }

            if (ValidateDoctorForm())
            {
                _currentDoctor.Name = txtName.Text.Trim();
                _currentDoctor.Specialization = cmbSpecialization.Text;
                _currentDoctor.Phone = txtPhone.Text.Trim();
                _currentDoctor.Email = txtEmail.Text.Trim();
                _currentDoctor.Department = txtDepartment.Text.Trim();
                _currentDoctor.Qualification = txtQualification.Text.Trim();
                _currentDoctor.ExperienceYears = string.IsNullOrEmpty(txtExperience.Text) ? (int?)null : int.Parse(txtExperience.Text);
                _currentDoctor.ConsultationFee = string.IsNullOrEmpty(txtFee.Text) ? (decimal?)null : decimal.Parse(txtFee.Text);
                _currentDoctor.IsAvailable = chkAvailable.Checked;

                try
                {
                    _doctorService.UpdateDoctor(_currentDoctor);
                    FormHelper.ShowSuccessMessage("Doctor updated successfully!");
                    LoadDoctors();
                    ClearForm();
                    _currentDoctor = null;
                }
                catch (Exception ex)
                {
                    FormHelper.ShowErrorMessage($"Error updating doctor: {ex.Message}");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_currentDoctor == null)
            {
                FormHelper.ShowErrorMessage("Please select a doctor to delete.");
                return;
            }

            if (FormHelper.ShowConfirmationMessage($"Are you sure you want to delete doctor: {_currentDoctor.Name}?"))
            {
                try
                {
                    _doctorService.DeleteDoctor(_currentDoctor.Id);
                    FormHelper.ShowSuccessMessage("Doctor deleted successfully!");
                    LoadDoctors();
                    ClearForm();
                    _currentDoctor = null;
                }
                catch (Exception ex)
                {
                    FormHelper.ShowErrorMessage($"Error deleting doctor: {ex.Message}");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var allDoctors = _doctorService.GetAllDoctors();
                var results = allDoctors.Where(d =>
                    (d.Name != null && d.Name.ToLower().Contains(searchTerm.ToLower())) ||
                    (d.Specialization != null && d.Specialization.ToLower().Contains(searchTerm.ToLower())) ||
                    (d.Phone != null && d.Phone.Contains(searchTerm)) ||
                    (d.Email != null && d.Email.ToLower().Contains(searchTerm.ToLower()))).ToList();

                _doctors = new BindingList<Doctor>(results);
                dataGridViewDoctors.DataSource = _doctors;
                UpdateDoctorCount();
            }
            else
            {
                LoadDoctors();
            }
            FormatGrid();
        }
        private void cmbSpecializationFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSpecializationFilter.SelectedIndex > 0)
            {
                string specialization = cmbSpecializationFilter.SelectedItem.ToString();
                var filteredDoctors = _doctorService.GetDoctorsBySpecialization(specialization);
                _doctors = new BindingList<Doctor>(filteredDoctors);
                dataGridViewDoctors.DataSource = _doctors;
                UpdateDoctorCount();
            }
            else
            {
                LoadDoctors();
            }
        }

        private void dataGridViewDoctors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _currentDoctor = _doctors[e.RowIndex];
                LoadDoctorToForm(_currentDoctor);
            }
        }

        private void LoadDoctorToForm(Doctor doctor)
        {
            txtName.Text = doctor.Name;
            cmbSpecialization.Text = doctor.Specialization;
            txtPhone.Text = doctor.Phone;
            txtEmail.Text = doctor.Email;
            txtDepartment.Text = doctor.Department;
            txtQualification.Text = doctor.Qualification;
            txtExperience.Text = doctor.ExperienceYears?.ToString();
            txtFee.Text = doctor.ConsultationFee?.ToString();
            chkAvailable.Checked = doctor.IsAvailable;
        }

        private void ClearForm()
        {
            FormHelper.ClearControls(this);
            _currentDoctor = null;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private bool ValidateDoctorForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                FormHelper.ShowErrorMessage("Please enter doctor name.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbSpecialization.Text))
            {
                FormHelper.ShowErrorMessage("Please select specialization.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                FormHelper.ShowErrorMessage("Please enter phone number.");
                return false;
            }

            return true;
        }

        private void dataGridViewDoctors_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewDoctors.CurrentRow != null && dataGridViewDoctors.CurrentRow.Index >= 0)
            {
                _currentDoctor = _doctors[dataGridViewDoctors.CurrentRow.Index];
                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void FormatGrid()
        {
            dataGridViewDoctors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            
            dataGridViewDoctors.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewDoctors.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridViewDoctors.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewDoctors.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewDoctors.EnableHeadersVisualStyles = false;

           
            dataGridViewDoctors.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dataGridViewDoctors.DefaultCellStyle.BackColor = Color.White;
            dataGridViewDoctors.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewDoctors.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewDoctors.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewDoctors.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

           
            dataGridViewDoctors.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

           
            dataGridViewDoctors.RowHeadersVisible = false;
            dataGridViewDoctors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDoctors.MultiSelect = false;
            dataGridViewDoctors.AllowUserToAddRows = false;
            dataGridViewDoctors.AllowUserToResizeRows = false;
            dataGridViewDoctors.ReadOnly = true;
            dataGridViewDoctors.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewDoctors.GridColor = Color.LightGray;

    
            if (dataGridViewDoctors.Columns["Id"] != null)
                dataGridViewDoctors.Columns["Id"].Visible = false;

   
            if (dataGridViewDoctors.Columns["DateOfBirth"] != null)
                dataGridViewDoctors.Columns["DateOfBirth"].DefaultCellStyle.Format = "yyyy-MM-dd";

        }

        private void btnExportcsv_Click(object sender, EventArgs e)
        {
            if (dataGridViewDoctors.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "doctors.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();

          
                var headers = dataGridViewDoctors.Columns.Cast<DataGridViewColumn>()
                    .Where(c => c.Visible)
                    .Select(c => c.HeaderText);
                sb.AppendLine(string.Join(",", headers));

          
                foreach (DataGridViewRow row in dataGridViewDoctors.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        var cells = row.Cells.Cast<DataGridViewCell>()
                            .Where(c => c.OwningColumn.Visible)
                            .Select(c => c.Value?.ToString()?.Replace(",", " ") ?? "");
                        sb.AppendLine(string.Join(",", cells));
                    }
                }

                File.WriteAllText(sfd.FileName, sb.ToString());
                MessageBox.Show("Data exported successfully.");
            }
        }
    }
}
