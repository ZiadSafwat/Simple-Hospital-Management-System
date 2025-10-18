using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [StringLength(20)]
        public string Status { get; set; } = "Scheduled";

        public string Notes { get; set; }
        public string Diagnosis { get; set; }
        public string Prescription { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }

        public string PatientName => Patient?.Name;
        public string DoctorName => Doctor?.Name;
    }
}
