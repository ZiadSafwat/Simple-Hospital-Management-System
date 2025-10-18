using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Entity;  
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    public class ReportService
    {
        public class AppointmentStats
        {
            public string DoctorName { get; set; }
            public int AppointmentCount { get; set; }
        }

        
        public List<AppointmentStats> GetAppointmentStats()
        {
            using (var context = new HospitalDbContext())
            {
                return context.Appointments
                    .Include(a => a.Doctor)
                    .GroupBy(a => a.Doctor.Name)
                    .Select(g => new AppointmentStats
                    {
                        DoctorName = g.Key,
                        AppointmentCount = g.Count()
                    })
                    .ToList();
            }
        }

        
        public int GetTotalPatients()
        {
            using (var context = new HospitalDbContext())
            {
                return context.Patients.Count();
            }
        }

        
        public int GetTotalDoctors()
        {
            using (var context = new HospitalDbContext())
            {
                return context.Doctors.Count();
            }
        }

        
        public int GetTotalAppointments()
        {
            using (var context = new HospitalDbContext())
            {
                return context.Appointments.Count();
            }
        }

        
        public void ExportAppointmentsToCsv(string filePath)
        {
            using (var context = new HospitalDbContext())
            {
                var appointments = context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .ToList();

                var csv = new StringBuilder();
                csv.AppendLine("Date,Patient,Doctor,Status,Notes");

                foreach (var app in appointments)
                {
                    string date = app.AppointmentDate.ToString("yyyy-MM-dd");
                    string patient = app.Patient?.Name ?? "N/A";
                    string doctor = app.Doctor?.Name ?? "N/A";
                    string status = app.Status ?? "Unknown";
                    string notes = app.Notes?.Replace(",", " ") ?? "";

                    csv.AppendLine($"\"{date}\",\"{patient}\",\"{doctor}\",\"{status}\",\"{notes}\"");
                }

                File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8);
            }
        }
    }
}
