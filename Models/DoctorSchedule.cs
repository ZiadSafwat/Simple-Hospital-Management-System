using System;
using System.Collections.Generic;

namespace HospitalManagementSystem.Models;

public partial class DoctorSchedule
{
    public int Id { get; set; }

    public int DoctorId { get; set; }

    public int DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int SlotDurationMinutes { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;
}
