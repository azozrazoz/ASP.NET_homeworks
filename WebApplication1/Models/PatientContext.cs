using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PatientContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Doctor>()
                .HasMany(c => c.Patients)
                .WithMany(c => c.Doctors)
                .Map(t => t
                .MapLeftKey("PatientId")
                .MapRightKey("DoctorId")
                .ToTable("PatientDoctor"));

            base.OnModelCreating(modelBuilder);
        }
    }
}