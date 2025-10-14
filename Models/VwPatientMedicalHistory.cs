using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models;

public partial class VwPatientMedicalHistory
{
    public string PatientName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public DateTime? RecordDate { get; set; }

    public string DoctorName { get; set; } = null!;

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }
}
