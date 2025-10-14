using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

public class AppointmentService
{
    public List<HospitalManagementSystem.Models.Appointment> GetAppointmentsWithDetails()
    {
        using (var context = new HospitalManagementSystemContext())
        {
            return context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToList();
        }
    }

    public bool BookAppointment(HospitalManagementSystem.Models.Appointment appointment)
    {
        using (var context = new HospitalManagementSystemContext())
        {
            // Check for time conflict
            bool conflict = context.Appointments
                .Any(a => a.DoctorId == appointment.DoctorId
                       && a.AppointmentDate == appointment.AppointmentDate
                       && a.Status != "Cancelled");

            if (!conflict)
            {
                context.Appointments.Add(appointment);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
