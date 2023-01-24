using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public Student()
        {
            Courses = new List<Course>();
        }
    }

    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public Course()
        {
            Students = new List<Student>();
        }
    }

    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / PageSize);
            }
        }
    }

    public class IndexViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public PageInfo pageInfo { get; set; }
    }

    public class StudentsListViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public SelectList Courses { get; set; }
        public SelectList Faculty { get; set; }
    }
}