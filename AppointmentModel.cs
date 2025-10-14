using HospitalManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

public class Appointment
{
    public int Id { get; set; }

    public int PatientId { get; set; }
    public int DoctorId { get; set; }

    public DateTime AppointmentDate { get; set; }

    [StringLength(20)]
    public string Status { get; set; }

    public string Notes { get; set; }

    // Navigation properties
    public virtual Patient Patient { get; set; }
    public virtual Doctor Doctor { get; set; }
}
