using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    public class PatientService
    {
        public List<Patient> GetAllPatients()
        {
            using (var context = new HospitalDbContext())
            {
                return context.Patients.OrderBy(p => p.Name).ToList();
            }
        }
        
        public Patient GetPatientById(int id)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Patients.Find(id);
            }
        }
        
        public void AddPatient(Patient patient)
        {
            using (var context = new HospitalDbContext())
            {
                context.Patients.Add(patient);
                context.SaveChanges();
            }
        }
        
        public void UpdatePatient(Patient patient)
        {
            using (var context = new HospitalDbContext())
            {
                patient.LastUpdated = DateTime.Now;
                context.Entry(patient).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        
        public void DeletePatient(int patientId)
        {
            using (var context = new HospitalDbContext())
            {
                var patient = context.Patients.Find(patientId);
                if (patient != null)
                {
                    context.Patients.Remove(patient);
                    context.SaveChanges();
                }
            }
        }

        public List<Patient> SearchPatients(string searchTerm)
        {
            using (var context = new HospitalDbContext())
            {
                return context.Patients
                    .Where(p => p.Name.Contains(searchTerm) 
                             || p.Phone.Contains(searchTerm)
                             || p.Email.Contains(searchTerm))
                    .ToList();
            }
        }
    }
}
