using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models;

public partial class VwAppointmentDetail
{
    public int Id { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string PatientName { get; set; } = null!;

    public string PatientPhone { get; set; } = null!;

    public string DoctorName { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public string? Status { get; set; }

    public string? Notes { get; set; }
}
