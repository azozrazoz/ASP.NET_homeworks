using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Gender { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
        public Patient()
        {
            Doctors = new List<Doctor>();
        }
    }

    public class Doctor
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Gender { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Specialty { get; set; }

        public ICollection<Patient> Patients { get; set; }
        public Doctor()
        {
            Patients = new List<Patient>();
        }
    }

    public class PatientsViewModel
    {
        public IEnumerable<Patient> Patients { get; set; }
        public PageInfo pageInfo { get; set; }
    }
}