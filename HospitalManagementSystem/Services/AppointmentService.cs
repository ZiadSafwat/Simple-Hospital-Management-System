using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    public class AppointmentService
    {
        public List<Appointment> GetAllAppointments()
        {
            using (var context = new HospitalDbContext())
            {
                return context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .OrderByDescending(a => a.AppointmentDate)
                    .ToList();
            }
        }

        public Appointment GetAppointmentById(int id)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .FirstOrDefault(a => a.Id == id);
            }
        }

        public List<Appointment> GetAppointmentsByDoctor(int doctorId)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Appointments
                    .Include(a => a.Patient)
                    .Where(a => a.DoctorId == doctorId)
                    .OrderBy(a => a.AppointmentDate)
                    .ToList();
            }
        }

        public List<Appointment> GetTodaysAppointments()
        {
            using (var context = new HospitalDbContext())
            {
                DateTime today = DateTime.Today;
                return context.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .Where(a => DbFunctions.TruncateTime(a.AppointmentDate) == today)
                    .OrderBy(a => a.AppointmentDate)
                    .ToList();
            }
        }

        public bool IsTimeSlotAvailable(int doctorId, DateTime appointmentDate)
        {
            using (var context = new HospitalDbContext())
            {
                return !context.Appointments.Any(a =>
                    a.DoctorId == doctorId &&
                    a.AppointmentDate == appointmentDate &&
                    a.Status != "Cancelled");
            }
        }

        public string BookAppointment(Appointment appointment)
        {
            using (var context = new HospitalDbContext())
            {
                var doctor = context.Doctors.Find(appointment.DoctorId);
                if (doctor == null || !doctor.IsAvailable)
                    return "Doctor not available";

                if (!IsTimeSlotAvailable(appointment.DoctorId, appointment.AppointmentDate))
                    return "Time slot not available";

                context.Appointments.Add(appointment);
                context.SaveChanges();
                return "Success";
            }
        }

        public void UpdateAppointmentStatus(int appointmentId, string status)
        {
            using (var context = new HospitalDbContext())
            {
                var appointment = context.Appointments.Find(appointmentId);
                if (appointment != null)
                {
                    appointment.Status = status;
                    context.SaveChanges();
                }
            }
        }

        public void UpdateAppointmentDiagnosis(int appointmentId, string diagnosis, string prescription)
        {
            using (var context = new HospitalDbContext())
            {
                var appointment = context.Appointments.Find(appointmentId);
                if (appointment != null)
                {
                    appointment.Diagnosis = diagnosis;
                    appointment.Prescription = prescription;
                    appointment.Status = "Completed";
                    context.SaveChanges();
                }
            }
        }

        public List<DateTime> GetAvailableTimeSlots(int doctorId, DateTime date)
        {
            var bookedSlots = GetDoctorAppointments(doctorId, date)
                .Where(a => a.Status != "Cancelled")
                .Select(a => a.AppointmentDate)
                .ToList();

            var availableSlots = new List<DateTime>();
            DateTime startTime = date.Date.AddHours(9);
            DateTime endTime = date.Date.AddHours(17);

            for (DateTime slot = startTime; slot < endTime; slot = slot.AddMinutes(30))
            {
                if (!bookedSlots.Any(booked => booked == slot))
                    availableSlots.Add(slot);
            }

            return availableSlots;
        }

        public List<Appointment> GetDoctorAppointments(int doctorId, DateTime date)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Appointments
                    .Include(a => a.Patient)
                    .Where(a => a.DoctorId == doctorId &&
                               a.AppointmentDate.Date == date.Date)
                    .OrderBy(a => a.AppointmentDate)
                    .ToList();
            }
        }
    }
}
