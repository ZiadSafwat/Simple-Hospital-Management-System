using System;
using System.Collections.Generic;
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
                return context.Patients.OrderBy(p => p.Id).ToList();
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
        public void UpdatePatient(Patient updatedPatient)
        {
            using (var context = new HospitalDbContext())
            {
                var existingPatient = context.Patients.FirstOrDefault(p => p.Id == updatedPatient.Id);

                if (existingPatient == null)
                    throw new Exception("Patient not found in database.");
                existingPatient.Name = updatedPatient.Name;
                existingPatient.Phone = updatedPatient.Phone;
                existingPatient.Email = updatedPatient.Email;
                existingPatient.DateOfBirth = updatedPatient.DateOfBirth;
                existingPatient.Gender = updatedPatient.Gender;
                existingPatient.Address = updatedPatient.Address;
                existingPatient.EmergencyContact = updatedPatient.EmergencyContact;
                existingPatient.BloodType = updatedPatient.BloodType;

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
                       .Where(p =>
                        p.Name.StartsWith(searchTerm))
                       .OrderBy(p => p.Id)
                       .ToList();
            }
        }
    }
}
