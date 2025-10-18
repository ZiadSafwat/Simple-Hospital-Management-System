using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    public class DoctorService
    {
        public List<Doctor> GetAllDoctors()
        {
            using (var context = new HospitalDbContext())
            {
                return context.Doctors.OrderBy(d => d.Name).ToList();
            }
        }

        public Doctor GetDoctorById(int id)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Doctors.Find(id);
            }
        }

        public List<Doctor> GetDoctorsBySpecialization(string specialization)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Doctors
                    .Where(d => d.Specialization == specialization && d.IsAvailable)
                    .ToList();
            }
        }

        public List<string> GetAllSpecializations()
        {
            using (var context = new HospitalDbContext())
            {
                return context.Doctors
                    .Select(d => d.Specialization)
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();
            }
        }

        public void AddDoctor(Doctor doctor)
        {
            using (var context = new HospitalDbContext())
            {
                context.Doctors.Add(doctor);
                context.SaveChanges();
            }
        }

        public void UpdateDoctor(Doctor doctor)
        {
            using (var context = new HospitalDbContext())
            {
                context.Entry(doctor).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void DeleteDoctor(int doctorId)
        {
            using (var context = new HospitalDbContext())
            {
                var doctor = context.Doctors.Find(doctorId);
                if (doctor != null)
                {
                    context.Doctors.Remove(doctor);
                    context.SaveChanges();
                }
            }
        }

        public void SetDoctorAvailability(int doctorId, bool isAvailable)
        {
            using (var context = new HospitalDbContext())
            {
                var doctor = context.Doctors.Find(doctorId);
                if (doctor != null)
                {
                    doctor.IsAvailable = isAvailable;
                    context.SaveChanges();
                }
            }
        }
    }
}
