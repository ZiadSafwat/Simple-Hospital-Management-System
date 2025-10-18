using System;
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
    public partial class PatientsForm : Form
    {
        private PatientService _patientService;
        private AppointmentService _appointmentService = new AppointmentService();
        private System.ComponentModel.BindingList<Patient> _patients;
        private Patient _currentPatient;

        public PatientsForm()
        {
            InitializeComponent();
            _patientService = new PatientService();
            LoadPatients();
        }

  

        private void LoadPatients()
        {
            var patients = _patientService.GetAllPatients();
            var appointments = _appointmentService.GetAllAppointments();

            var displayList = patients.Select(p => new
            {
                p.Id,
                p.Name,
                p.Phone,
                p.Email,
                p.Gender,
                p.Address,
                p.BloodType,
                p.EmergencyContact,
                p.DateOfBirth,
                p.CreatedDate,
                p.LastUpdated,
                Age = CalculateAge(p.DateOfBirth),
                AppointmentCount = appointments.Count(a => a.PatientId == p.Id),
                AppointmentSummary = string.Join(" | ", appointments
                    .Where(a => a.PatientId == p.Id)
                    .OrderByDescending(a => a.AppointmentDate)
                    .Take(2)
                    .Select(a => $"{a.AppointmentDate:dd/MM/yyyy} - {a.Status}"))
            }).ToList();

            dataGridViewPatients.DataSource = displayList;
            UpdatePatientCount(displayList.Count);
            FormatGrid();
        }
        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age;
        }
        private void UpdatePatientCount(int count)
        {
            lblPatientCount.Text = $"Total Patients: {count}";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidatePatientForm())
            {
                var patient = new Patient
                {
                    Name = txtName.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    DateOfBirth = dtpDateOfBirth.Value,
                    Gender = cmbGender.Text,
                    Address = txtAddress.Text.Trim(),
                    EmergencyContact = txtEmergencyContact.Text.Trim(),
                    BloodType = cmbBloodType.Text
                };

                try
                {
                    _patientService.AddPatient(patient);
                    FormHelper.ShowSuccessMessage("Patient added successfully!");
                    LoadPatients();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    FormHelper.ShowErrorMessage($"Error adding patient: {ex.Message}");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_currentPatient == null)
            {
                FormHelper.ShowErrorMessage("Please select a patient to update.");
                return;
            }
            if (ValidatePatientForm())
            {
                var patientInDb = _patientService.GetPatientById(_currentPatient.Id);

                if (patientInDb == null)
                {
                    FormHelper.ShowErrorMessage("Patient not found in database.");
                    return;
                }
                patientInDb.Name = txtName.Text.Trim();
                patientInDb.Phone = txtPhone.Text.Trim();
                patientInDb.Email = txtEmail.Text.Trim();
                patientInDb.DateOfBirth = dtpDateOfBirth.Value;
                patientInDb.Gender = cmbGender.Text;
                patientInDb.Address = txtAddress.Text.Trim();
                patientInDb.EmergencyContact = txtEmergencyContact.Text.Trim();
                patientInDb.BloodType = cmbBloodType.Text;

                try
                {
                    _patientService.UpdatePatient(patientInDb);
                    FormHelper.ShowSuccessMessage("Patient updated successfully!");
                    LoadPatients();
                    ClearForm();
                    _currentPatient = null;
                }
                catch (Exception ex)
                {
                    FormHelper.ShowErrorMessage($"Error updating patient: {ex.Message}");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_currentPatient == null)
            {
                FormHelper.ShowErrorMessage("Please select a patient to delete.");
                return;
            }

            if (FormHelper.ShowConfirmationMessage($"Are you sure you want to delete patient: {_currentPatient.Name}?"))
            {
                try
                {
                    _patientService.DeletePatient(_currentPatient.Id);
                    FormHelper.ShowSuccessMessage("Patient deleted successfully!");
                    LoadPatients();
                    ClearForm();
                    _currentPatient = null;
                }
                catch (Exception ex)
                {
                    FormHelper.ShowErrorMessage($"Error deleting patient: {ex.Message}");
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
            var appointments = _appointmentService.GetAllAppointments();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var results = _patientService.SearchPatients(searchTerm);

                var displayList = results.Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Phone,
                    p.Email,
                    p.Gender,
                    p.BloodType,
                    p.Address,
                    p.EmergencyContact,
                    p.DateOfBirth,
                    p.CreatedDate,
                    p.LastUpdated,
                    Age = CalculateAge(p.DateOfBirth),
                    AppointmentCount = appointments.Count(a => a.PatientId == p.Id),
                    AppointmentSummary = string.Join(" | ", appointments
                        .Where(a => a.PatientId == p.Id)
                        .OrderByDescending(a => a.AppointmentDate)
                        .Take(2)
                        .Select(a => $"{a.AppointmentDate:dd/MM/yyyy} - {a.Status}"))
                }).ToList();

                dataGridViewPatients.DataSource = displayList;
                UpdatePatientCount(displayList.Count);
                FormatGrid();
            }
            else
            {
                LoadPatients();
            }

            FormatGrid();
        }


        private void dataGridViewPatients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridViewPatients.Rows[e.RowIndex];
                var id = Convert.ToInt32(row.Cells["Id"].Value);
                var name = row.Cells["Name"].Value?.ToString();
                var phone = row.Cells["Phone"].Value?.ToString();
                var email = row.Cells["Email"].Value?.ToString();
                var gender = row.Cells["Gender"].Value?.ToString();
                var address = row.Cells["Address"].Value?.ToString();
                var bloodType = row.Cells["BloodType"].Value?.ToString();
                var emergencyContact = row.Cells["EmergencyContact"].Value?.ToString();
                var dob = Convert.ToDateTime(row.Cells["DateOfBirth"].Value);

                _currentPatient = new Patient
                {
                    Id = id,
                    Name = name,
                    Phone = phone,
                    Email = email,
                    Gender = gender,
                    Address = address,
                    BloodType = bloodType,
                    EmergencyContact = emergencyContact,
                    DateOfBirth = dob
                };

                LoadPatientToForm(_currentPatient);
            }
        }

        private void LoadPatientToForm(Patient patient)
        {
            txtName.Text = patient.Name;
            txtPhone.Text = patient.Phone;
            txtEmail.Text = patient.Email;
            dtpDateOfBirth.Value = patient.DateOfBirth;
            cmbGender.Text = patient.Gender;
            txtAddress.Text = patient.Address;
            txtEmergencyContact.Text = patient.EmergencyContact;
            cmbBloodType.Text = patient.BloodType;
            FormatGrid();
        }

        private void ClearForm()
        {
            FormHelper.ClearControls(this);
            _currentPatient = null;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private bool ValidatePatientForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                FormHelper.ShowErrorMessage("Please enter patient name.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                FormHelper.ShowErrorMessage("Please enter phone number.");
                return false;
            }

            if (dtpDateOfBirth.Value > DateTime.Now)
            {
                FormHelper.ShowErrorMessage("Date of birth cannot be in the future.");
                return false;
            }

            return true;
        }

        private void dataGridViewPatients_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewPatients.CurrentRow != null)
            {
                var row = dataGridViewPatients.CurrentRow;

                _currentPatient = new Patient
                {
                    Id = Convert.ToInt32(row.Cells["Id"].Value),
                    Name = row.Cells["Name"].Value?.ToString(),
                    Phone = row.Cells["Phone"].Value?.ToString(),
                    Email = row.Cells["Email"].Value?.ToString(),
                    Gender = row.Cells["Gender"].Value?.ToString(),
                    BloodType = row.Cells["BloodType"].Value?.ToString(),
                    EmergencyContact = row.Cells["EmergencyContact"].Value?.ToString(),
                    DateOfBirth = Convert.ToDateTime(row.Cells["DateOfBirth"].Value)
                };

                btnAdd.Enabled = false;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void FormatGrid()
        {
            dataGridViewPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;

            dataGridViewPatients.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewPatients.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridViewPatients.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewPatients.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPatients.EnableHeadersVisualStyles = false;

            dataGridViewPatients.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dataGridViewPatients.DefaultCellStyle.BackColor = Color.White;
            dataGridViewPatients.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewPatients.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewPatients.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewPatients.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridViewPatients.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

            dataGridViewPatients.RowHeadersVisible = false;
            dataGridViewPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPatients.MultiSelect = false;
            dataGridViewPatients.AllowUserToAddRows = false;
            dataGridViewPatients.AllowUserToResizeRows = false;
            dataGridViewPatients.ReadOnly = true;
            dataGridViewPatients.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewPatients.GridColor = Color.LightGray;

            if (dataGridViewPatients.Columns["Id"] != null)
                dataGridViewPatients.Columns["Id"].Visible = false;
            if (dataGridViewPatients.Columns["DateOfBirth"] != null)
                dataGridViewPatients.Columns["DateOfBirth"].DefaultCellStyle.Format = "yyyy-MM-dd";
            if (dataGridViewPatients.Columns["CreatedDate"] != null)
                dataGridViewPatients.Columns["CreatedDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            if (dataGridViewPatients.Columns["LastUpdated"] != null)
                dataGridViewPatients.Columns["LastUpdated"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            if (dataGridViewPatients.Columns["Address"] != null)
                dataGridViewPatients.Columns["Address"].HeaderText = "Address";
            if (dataGridViewPatients.Columns["AppointmentCount"] != null)
                dataGridViewPatients.Columns["AppointmentCount"].HeaderText = "Appointments";
            if (dataGridViewPatients.Columns["AppointmentSummary"] != null)
                dataGridViewPatients.Columns["AppointmentSummary"].HeaderText = "Appointment Summary";
            if (dataGridViewPatients.Columns["Age"] != null)
                dataGridViewPatients.Columns["Age"].HeaderText = "Age";
        }
        private void btnExportcsv_Click(object sender, EventArgs e)
        {
            if (dataGridViewPatients.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "Patients.csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();


                var headers = dataGridViewPatients.Columns.Cast<DataGridViewColumn>()
                    .Where(c => c.Visible)
                    .Select(c => c.HeaderText);
                sb.AppendLine(string.Join(",", headers));


                foreach (DataGridViewRow row in dataGridViewPatients.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        var cells = row.Cells.Cast<DataGridViewCell>()
                            .Where(c => c.OwningColumn.Visible)
                            .Select(c => {
                                if (c.Value is DateTime dt)
                                    return dt.ToString("yyyy-MM-dd HH:mm");

                                return c.Value?.ToString()?.Replace(",", " ") ?? "";
                            });

                        sb.AppendLine(string.Join(",", cells));
                    }
                }

                try
                {
                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Data exported successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting data: {ex.Message}");
                }
            }
        }

    }
}
