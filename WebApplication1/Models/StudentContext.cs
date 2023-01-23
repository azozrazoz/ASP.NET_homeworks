using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Course>()
                .HasMany(c => c.Students)
                .WithMany(c => c.Courses)
                .Map(t => t
                .MapLeftKey("CourseId")
                .MapRightKey("StudentId")
                .ToTable("CourseStudent"));

            base.OnModelCreating(modelBuilder);
        }
    }
}