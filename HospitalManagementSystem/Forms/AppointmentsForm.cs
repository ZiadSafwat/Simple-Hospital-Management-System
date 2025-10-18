using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utilities;
namespace HospitalManagementSystem.Forms
{
    public partial class AppointmentsForm : Form
    {
        private AppointmentService _appointmentService;
        private PatientService _patientService;
        private DoctorService _doctorService;
        private BindingList<Appointment> _appointments;
        private Appointment _currentAppointment;

        public AppointmentsForm()
        {
            InitializeComponent();
            _appointmentService = new AppointmentService();
            _patientService = new PatientService();
            _doctorService = new DoctorService();
            LoadAppointments();
            LoadComboBoxes();
        }

        private void LoadAppointments()
        {
            var appointments = _appointmentService.GetAllAppointments();
            _appointments = new BindingList<Appointment>(appointments);
            dataGridViewAppointments.DataSource = _appointments;
            UpdateAppointmentCount();
            FormatGrid();
        }

        private void LoadComboBoxes()
        {
            var patients = _patientService.GetAllPatients();
            cmbPatient.DataSource = patients;
            cmbPatient.DisplayMember = "Name";
            cmbPatient.ValueMember = "Id";

            var doctors = _doctorService.GetAllDoctors();
            cmbDoctor.DataSource = doctors;
            cmbDoctor.DisplayMember = "Name";
            cmbDoctor.ValueMember = "Id";

            var specializations = _doctorService.GetAllSpecializations();
            cmbSpecialization.Items.Clear();
            cmbSpecialization.Items.Add("All Specializations");
            cmbSpecialization.Items.AddRange(specializations.ToArray());
            cmbSpecialization.SelectedIndex = 0;

            cmbStatus.Items.AddRange(new string[] { "Scheduled", "Completed", "Cancelled", "NoShow" });
            cmbStatus.SelectedIndex = 0;
        }

        private void UpdateAppointmentCount()
        {
            lblAppointmentCount.Text = $"Total Appointments: {_appointments.Count}";
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (ValidateAppointmentForm())
            {
                var appointment = new Appointment
                {
                    PatientId = (int)cmbPatient.SelectedValue,
                    DoctorId = (int)cmbDoctor.SelectedValue,
                    AppointmentDate = dtpAppointmentDate.Value,
                    Status = cmbStatus.Text,
                    Notes = txtNotes.Text.Trim()
                };

                try
                {
                    string result = _appointmentService.BookAppointment(appointment);
                    if (result == "Success")
                    {
                        FormHelper.ShowSuccessMessage("Appointment booked successfully!");
                        LoadAppointments();
                        ClearForm();
                    }
                    else
                    {
                        FormHelper.ShowErrorMessage(result);
                    }
                }
                catch (Exception ex)
                {
                    FormHelper.ShowErrorMessage($"Error booking appointment: {ex.Message}");
                }
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (_currentAppointment == null)
            {
                FormHelper.ShowErrorMessage("Please select an appointment to update.");
                return;
            }

            try
            {
                _appointmentService.UpdateAppointmentStatus(_currentAppointment.Id, cmbStatus.Text);
                FormHelper.ShowSuccessMessage("Appointment status updated successfully!");
                LoadAppointments();
                ClearForm();
                _currentAppointment = null;
            }
            catch (Exception ex)
            {
                FormHelper.ShowErrorMessage($"Error updating appointment: {ex.Message}");
            }
        }

        private void btnAddDiagnosis_Click(object sender, EventArgs e)
        {
            if (_currentAppointment == null)
            {
                FormHelper.ShowErrorMessage("Please select an appointment to add diagnosis.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDiagnosis.Text))
            {
                FormHelper.ShowErrorMessage("Please enter diagnosis.");
                return;
            }

            try
            {
                _appointmentService.UpdateAppointmentDiagnosis(
                    _currentAppointment.Id,
                    txtDiagnosis.Text.Trim(),
                    txtPrescription.Text.Trim());

                FormHelper.ShowSuccessMessage("Diagnosis and prescription added successfully!");
                LoadAppointments();
                ClearForm();
                _currentAppointment = null;
            }
            catch (Exception ex)
            {
                FormHelper.ShowErrorMessage($"Error adding diagnosis: {ex.Message}");
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
                var allAppointments = _appointmentService.GetAllAppointments();
                var results = allAppointments.Where(a =>
                    (a.PatientName != null && a.PatientName.ToLower().Contains(searchTerm.ToLower())) ||
                    (a.DoctorName != null && a.DoctorName.ToLower().Contains(searchTerm.ToLower())) ||
                    (a.Status != null && a.Status.ToLower().Contains(searchTerm.ToLower()))).ToList();

                _appointments = new BindingList<Appointment>(results);
                dataGridViewAppointments.DataSource = _appointments;
                UpdateAppointmentCount();
            }
            else
            {
                LoadAppointments();
            }
            FormatGrid();

        }
        private void cmbSpecialization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSpecialization.SelectedIndex > 0)
            {
                string specialization = cmbSpecialization.SelectedItem.ToString();
                var doctors = _doctorService.GetDoctorsBySpecialization(specialization);
                cmbDoctor.DataSource = doctors;
            }
            else
            {
                var doctors = _doctorService.GetAllDoctors();
                cmbDoctor.DataSource = doctors;
            }
        }

        private void dataGridViewAppointments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _currentAppointment = _appointments[e.RowIndex];
                LoadAppointmentToForm(_currentAppointment);
            }
        }

        private void LoadAppointmentToForm(Appointment appointment)
        {
            cmbPatient.SelectedValue = appointment.PatientId;
            cmbDoctor.SelectedValue = appointment.DoctorId;
            dtpAppointmentDate.Value = appointment.AppointmentDate;
            cmbStatus.Text = appointment.Status;
            txtNotes.Text = appointment.Notes;
            txtDiagnosis.Text = appointment.Diagnosis;
            txtPrescription.Text = appointment.Prescription;
            FormatGrid();
        }

        private void ClearForm()
        {
            FormHelper.ClearControls(this);
            _currentAppointment = null;
            cmbStatus.SelectedIndex = 0;
            btnBook.Enabled = true;
            btnUpdateStatus.Enabled = false;
            btnAddDiagnosis.Enabled = false;
        }

        private bool ValidateAppointmentForm()
        {
            if (cmbPatient.SelectedValue == null)
            {
                FormHelper.ShowErrorMessage("Please select a patient.");
                return false;
            }

            if (cmbDoctor.SelectedValue == null)
            {
                FormHelper.ShowErrorMessage("Please select a doctor.");
                return false;
            }

            if (dtpAppointmentDate.Value < DateTime.Now)
            {
                FormHelper.ShowErrorMessage("Appointment date cannot be in the past.");
                return false;
            }

          
            if (dtpAppointmentDate.Value.Hour < 9 || dtpAppointmentDate.Value.Hour >= 17)
            {
                FormHelper.ShowErrorMessage("Appointments can only be scheduled between 9 AM and 5 PM.");
                return false;
            }

            return true;
        }

        private void dataGridViewAppointments_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewAppointments.CurrentRow != null && dataGridViewAppointments.CurrentRow.Index > 0)
            {
                _currentAppointment = _appointments[dataGridViewAppointments.CurrentRow.Index];
                btnBook.Enabled = false;
                btnUpdateStatus.Enabled = true;
                btnAddDiagnosis.Enabled = true;
            }
        }

        private void btnTodaysAppointments_Click(object sender, EventArgs e)
        {
            var todaysAppointments = _appointmentService.GetTodaysAppointments();
            _appointments = new BindingList<Appointment>(todaysAppointments);
            dataGridViewAppointments.DataSource = _appointments;
            UpdateAppointmentCount();
            FormHelper.ShowSuccessMessage($"Showing {todaysAppointments.Count} appointments for today.");
        }
        private void FormatGrid()
        {
            dataGridViewAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
         
            dataGridViewAppointments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewAppointments.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridViewAppointments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewAppointments.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewAppointments.EnableHeadersVisualStyles = false;

        
            dataGridViewAppointments.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dataGridViewAppointments.DefaultCellStyle.BackColor = Color.White;
            dataGridViewAppointments.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewAppointments.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewAppointments.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewAppointments.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

        
            dataGridViewAppointments.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

      
            dataGridViewAppointments.RowHeadersVisible = false;
            dataGridViewAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewAppointments.MultiSelect = false;
            dataGridViewAppointments.AllowUserToAddRows = false;
            dataGridViewAppointments.AllowUserToResizeRows = false;
            dataGridViewAppointments.ReadOnly = true;
            dataGridViewAppointments.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewAppointments.GridColor = Color.LightGray;

           
            if (dataGridViewAppointments.Columns["Id"] != null)
                dataGridViewAppointments.Columns["Id"].Visible = false;

         
            if (dataGridViewAppointments.Columns["DateOfBirth"] != null)
                dataGridViewAppointments.Columns["DateOfBirth"].DefaultCellStyle.Format = "yyyy-MM-dd";

        }
    }
}
