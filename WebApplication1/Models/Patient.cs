using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Patient : Account
    {
        public Doctor doctor { get; set; }
    }

    public class Doctor : Account
    {
        public string Specialty { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
    }
}