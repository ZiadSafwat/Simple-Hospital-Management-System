using System;
using System.Data.Entity;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Services
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<HospitalDbContext>
    {
        protected override void Seed(HospitalDbContext context)
        {
            
            context.Users.AddRange(new[]
            {
                new User { Username = "admin", Password = "admin123", Role = "Admin" },
                new User { Username = "doctor", Password = "doctor123", Role = "Doctor" },
                new User { Username = "reception", Password = "reception123", Role = "Reception" }
            });

            
            context.Doctors.AddRange(new[]
            {
                new Doctor {
                    Name = "Dr. Ahmed Mohamed",
                    Specialization = "Cardiology",
                    Phone = "01012345678",
                    Email = "ahmed@hospital.com",
                    Department = "Heart Center",
                    Qualification = "MD Cardiology",
                    ExperienceYears = 15,
                    ConsultationFee = 200.00m
                },
                new Doctor {
                    Name = "Dr. Sara Mahmoud",
                    Specialization = "Pediatrics",
                    Phone = "01023456789",
                    Email = "sara@hospital.com",
                    Department = "Children Health",
                    Qualification = "MD Pediatrics",
                    ExperienceYears = 10,
                    ConsultationFee = 150.00m
                },
                new Doctor {
                    Name = "Dr. Michael Nabil",
                    Specialization = "Orthopedics",
                    Phone = "01034567890",
                    Email = "michael@hospital.com",
                    Department = "Bone Center",
                    Qualification = "MS Orthopedics",
                    ExperienceYears = 12,
                    ConsultationFee = 180.00m
                }
            });

            
            context.Patients.AddRange(new[]
            {
                new Patient {
                    Name = "Mohamed Ali",
                    Phone = "01111111111",
                    Email = "mohamed.ali@email.com",
                    DateOfBirth = new DateTime(1985, 3, 15),
                    Gender = "Male",
                    Address = "Cairo, Egypt",
                    EmergencyContact = "01222222222",
                    BloodType = "O+"
                },
                new Patient {
                    Name = "Fatma Mahmoud",
                    Phone = "01111111112",
                    Email = "fatma.m@email.com",
                    DateOfBirth = new DateTime(1990, 7, 22),
                    Gender = "Female",
                    Address = "Alexandria, Egypt",
                    EmergencyContact = "01222222223",
                    BloodType = "A+"
                }
            });

            context.SaveChanges();
        }
    }
}
