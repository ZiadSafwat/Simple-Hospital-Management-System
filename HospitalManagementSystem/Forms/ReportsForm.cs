using System;
using System.Linq;
using System.Windows.Forms;
using HospitalManagementSystem.Services;
using HospitalManagementSystem.Utilities;

namespace HospitalManagementSystem.Forms
{
    public partial class ReportsForm : Form
    {
        private ReportService _reportService;
        private PatientService _patientService;
        private DoctorService _doctorService;
        private AppointmentService _appointmentService;

        public ReportsForm()
        {
            InitializeComponent();
            _reportService = new ReportService();
            _patientService = new PatientService();
            _doctorService = new DoctorService();
            _appointmentService = new AppointmentService();
            LoadReports();
        }

        private void LoadReports()
        {
            LoadStatistics();
            LoadAppointmentStats();
            LoadTodaysAppointments();
        }

        private void LoadStatistics()
        {
            int totalPatients = _reportService.GetTotalPatients();
            int totalDoctors = _reportService.GetTotalDoctors();
            int totalAppointments = _reportService.GetTotalAppointments();

            lblTotalPatients.Text = totalPatients.ToString();
            lblTotalDoctors.Text = totalDoctors.ToString();
            lblTotalAppointments.Text = totalAppointments.ToString();

            
            chart1.Series["Statistics"].Points.Clear();
            chart1.Series["Statistics"].Points.AddXY("Patients", totalPatients);
            chart1.Series["Statistics"].Points.AddXY("Doctors", totalDoctors);
            chart1.Series["Statistics"].Points.AddXY("Appointments", totalAppointments);
        }

        private void LoadAppointmentStats()
        {
            var stats = _reportService.GetAppointmentStats();
            dataGridViewStats.DataSource = stats;

          
            chart2.Series["Appointments"].Points.Clear();
            foreach (var stat in stats)
            {
                chart2.Series["Appointments"].Points.AddXY(stat.DoctorName, stat.AppointmentCount);
            }
        }

        private void LoadTodaysAppointments()
        {
            var todaysAppointments = _appointmentService.GetTodaysAppointments();
            dataGridViewTodaysAppointments.DataSource = todaysAppointments.Select(a => new
            {
                Time = a.AppointmentDate.ToString("hh:mm tt"),
                Patient = a.Patient.Name,
                Doctor = a.Doctor.Name,
                Status = a.Status
            }).ToList();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Export Appointments";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _reportService.ExportAppointmentsToCsv(saveFileDialog.FileName);
                        FormHelper.ShowSuccessMessage($"Appointments exported successfully to {saveFileDialog.FileName}");
                    }
                    catch (Exception ex)
                    {
                        FormHelper.ShowErrorMessage($"Error exporting appointments: {ex.Message}");
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReports();
            FormHelper.ShowSuccessMessage("Reports refreshed successfully!");
        }

        private void btnSearchPatients_Click(object sender, EventArgs e)
        {
            string searchTerm = txtPatientSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var results = _patientService.SearchPatients(searchTerm);
                dataGridViewSearchResults.DataSource = results;
                lblSearchResults.Text = $"Found {results.Count} patients";
            }
            else
            {
                dataGridViewSearchResults.DataSource = null;
                lblSearchResults.Text = "Search results will appear here";
            }
        }

        private void btnShowAllPatients_Click(object sender, EventArgs e)
        {
            var allPatients = _patientService.GetAllPatients();
            dataGridViewSearchResults.DataSource = allPatients;
            lblSearchResults.Text = $"Showing all {allPatients.Count} patients";
        }
    }
}
