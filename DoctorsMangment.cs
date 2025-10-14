namespace HospitalManagementSystem.Models
{



    public partial class DoctorsMangment : Form
    {
        private readonly HospitalManagementSystemContext _context;
        private int _selectedDoctorId = 0;
        private int _selectedScheduleId = 0;

        public DoctorsMangment()
        {
            _context = new HospitalManagementSystemContext();
            InitializeComponent();
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;
            btnAddSchedule.Click += btnAddSchedule_Click;
            btnDeleteSchedule.Click += btnDeleteSchedule_Click;
            dgvDoctors.CellClick += dgvDoctors_CellClick;
            Load += DoctorsMangment_Load;
        }

        private void DoctorsMangment_Load(object sender, EventArgs e)
        {
            LoadDoctors();
            LoadDaysOfWeek();
        }

        private void LoadDoctors()
        {
            dgvDoctors.DataSource = _context.Doctors
                .Select(d => new
                {
                    d.Id,
                    d.Name,
                    d.Specialization,
                    d.Phone,
                    d.Email
                })
                .ToList();
        }

        private void LoadDaysOfWeek()
        {
            cbDayOfWeek.Items.Clear();
            cbDayOfWeek.Items.AddRange(new object[]
            {
                "Sunday (0)",
                "Monday (1)",
                "Tuesday (2)",
                "Wednesday (3)",
                "Thursday (4)",
                "Friday (5)",
                "Saturday (6)"
            });
        }

        private void ClearDoctorForm()
        {
            txtName.Clear();
            txtSpecialty.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            _selectedDoctorId = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var doctor = new Doctor
            {
                Name = txtName.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            LoadDoctors();
            ClearDoctorForm();
            MessageBox.Show("✅ Doctor added successfully");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedDoctorId == 0)
            {
                MessageBox.Show("Please select a doctor to update.");
                return;
            }

            var doctor = _context.Doctors.Find(_selectedDoctorId);
            if (doctor != null)
            {
                doctor.Name = txtName.Text;
                doctor.Phone = txtPhone.Text;
                doctor.Email = txtEmail.Text;

                _context.SaveChanges();
                LoadDoctors();
                MessageBox.Show("✏️ Doctor updated successfully");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedDoctorId == 0)
            {
                MessageBox.Show("Select a doctor to delete.");
                return;
            }

            var doctor = _context.Doctors.Find(_selectedDoctorId);
            if (doctor != null)
            {
                var confirm = MessageBox.Show("Are you sure you want to delete this doctor?",
                    "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _context.Doctors.Remove(doctor);
                    _context.SaveChanges();
                    LoadDoctors();
                    ClearDoctorForm();
                    MessageBox.Show("🗑️ Doctor deleted");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearDoctorForm();
        }

        private void dgvDoctors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvDoctors.Rows[e.RowIndex];
                _selectedDoctorId = (int)row.Cells["Id"].Value;
                txtName.Text = row.Cells["Name"].Value?.ToString();
                txtSpecialty.Text = row.Cells["Specialization"].Value?.ToString();
                txtPhone.Text = row.Cells["Phone"].Value?.ToString();
                txtEmail.Text = row.Cells["Email"].Value?.ToString();

                LoadDoctorSchedule(); // تحميل جدول الشيفتات لو موجود
            }
        }

        // ---------------------------------------
        // Schedule Tab Events
        // ---------------------------------------
        private void LoadDoctorSchedule()
        {
            if (_selectedDoctorId == 0)
            {
                dgvSchedule.DataSource = null;
                return;
            }

            dgvSchedule.DataSource = _context.DoctorSchedules
                .Where(s => s.DoctorId == _selectedDoctorId)
                .Select(s => new
                {
                    s.Id,
                    s.DayOfWeek,
                    s.StartTime,
                    s.EndTime,
                    s.SlotDurationMinutes
                })
                .ToList();
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            if (_selectedDoctorId == 0)
            {
                MessageBox.Show("Select a doctor first.");
                return;
            }

            if (cbDayOfWeek.SelectedIndex == -1)
            {
                MessageBox.Show("Select a day.");
                return;
            }

            var start = TimeOnly.FromTimeSpan(dtStart.Value.TimeOfDay);
            var end = TimeOnly.FromTimeSpan(dtEnd.Value.TimeOfDay);

            if (end <= start)
            {
                MessageBox.Show("End time must be after start time.");
                return;
            }

            var schedule = new DoctorSchedule
            {
                DoctorId = _selectedDoctorId,
                DayOfWeek = cbDayOfWeek.SelectedIndex,
                StartTime = start,
                EndTime = end,
                SlotDurationMinutes = (int)numSlot.Value
            };

            _context.DoctorSchedules.Add(schedule);
            _context.SaveChanges();
            LoadDoctorSchedule();
            MessageBox.Show("✅ Schedule added");
        }

        private void dgvSchedule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _selectedScheduleId = (int)dgvSchedule.Rows[e.RowIndex].Cells["Id"].Value;
            }
        }

        private void btnDeleteSchedule_Click(object sender, EventArgs e)
        {
            if (_selectedScheduleId == 0)
            {
                MessageBox.Show("Select a schedule to delete.");
                return;
            }

            var schedule = _context.DoctorSchedules.Find(_selectedScheduleId);
            if (schedule != null)
            {
                var confirm = MessageBox.Show("Delete this schedule?", "Confirm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    _context.DoctorSchedules.Remove(schedule);
                    _context.SaveChanges();
                    LoadDoctorSchedule();
                    _selectedScheduleId = 0;
                }
            }
        }



    }
}
