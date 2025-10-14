using HospitalManagementSystem.Models;

namespace HospitalManagementSystem
{
    public partial class Form1 : Form
    {
        private int selectedDoctorId;
        private HospitalManagementSystemContext context = new HospitalManagementSystemContext();

        public Form1()
        {
            InitializeComponent();
            DoctorCB.SelectedIndexChanged += DoctorCB_SelectedIndexChanged;
            monthCalendar1.DateChanged += monthCalendar1_DateChanged;
            btnBook.Click += btnBook_Click;
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load doctors
            var doctors = context.Doctors.ToList();
            DoctorCB.DataSource = doctors;
            DoctorCB.DisplayMember = "Name";
            DoctorCB.ValueMember = "Id";

            // Load patients
            var patients = context.Patients.ToList();
            PatientCB.Items.Clear();
            PatientCB.Items.AddRange(patients.Select(p => p.Name).ToArray());

            monthCalendar1.MaxSelectionCount = 1;

            // Set the first doctor as default (if exists)
            if (doctors.Any())
            {
                DoctorCB.SelectedIndex = 0;
            }
        }

        private void UpdateBoldedDates()
        {
            if (selectedDoctorId == 0)
            {
                monthCalendar1.BoldedDates = Array.Empty<DateTime>();
                return;
            }

            var datesWithAppointments = context.Appointments
                .Where(a => a.DoctorId == selectedDoctorId)
                .Select(a => a.AppointmentDate.Date)
                .Distinct()
                .ToArray();

            monthCalendar1.BoldedDates = datesWithAppointments;
        }

        private void DoctorCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(DoctorCB.SelectedValue?.ToString(), out int id))
            {
                selectedDoctorId = id;
                UpdateBoldedDates();
                monthCalendar1_DateChanged(this, new DateRangeEventArgs(monthCalendar1.SelectionStart, monthCalendar1.SelectionStart));
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (selectedDoctorId == 0)
            {
                cmbTimes.DataSource = null;
                lblStatus.Text = "Please select a doctor first";
                lblStatus.ForeColor = Color.Red;
                cmbTimes.Enabled = false;
                return;
            }

            var selectedDate = e.Start.Date;
            var availableAppointments = context.Appointments
                .Where(a => a.DoctorId == selectedDoctorId &&
                            a.AppointmentDate.Date == selectedDate &&
                            a.Status != "Booked")
                .OrderBy(a => a.AppointmentDate)
                .ToList();

            cmbTimes.DataSource = null;

            if (!availableAppointments.Any())
            {
                lblStatus.Text = "No available appointments for this day ❌";
                lblStatus.ForeColor = Color.Red;
                cmbTimes.Enabled = false;
                return;
            }

            cmbTimes.DataSource = availableAppointments
                .Select(a => new
                {
                    a.Id,
                    Time = a.AppointmentDate.ToString("hh:mm tt")
                })
                .ToList();
            cmbTimes.DisplayMember = "Time";
            cmbTimes.ValueMember = "Id";
            cmbTimes.Enabled = true;
            cmbTimes.SelectedIndex = 0;

            lblStatus.Text = "Day is available ✅";
            lblStatus.ForeColor = Color.Green;
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (cmbTimes.SelectedValue == null)
            {
                MessageBox.Show("Please select a time first");
                return;
            }

            // Ensure a patient is selected
            if (PatientCB.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a patient first");
                return;
            }

            // Get selected patient
            var patientName = PatientCB.SelectedItem.ToString();
            var patient = context.Patients.FirstOrDefault(p => p.Name == patientName);
            if (patient == null)
            {
                MessageBox.Show("Patient not found");
                return;
            }

            var appointmentId = (int)cmbTimes.SelectedValue;
            var appointment = context.Appointments.Find(appointmentId);
            if (appointment != null)
            {
                appointment.Status = "Booked";
                appointment.PatientId = patient.Id;
                appointment.Notes = txtNotes.Text;
                context.SaveChanges();
                MessageBox.Show("Appointment booked successfully ✅");

                // Update display
                monthCalendar1_DateChanged(this, new DateRangeEventArgs(appointment.AppointmentDate.Date, appointment.AppointmentDate.Date));
                UpdateBoldedDates();
            }
        }
    }
}
