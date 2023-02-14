using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        private StudentContext db = new StudentContext();
        // GET: Student
        public ActionResult Index(int page = 1)
        {
            int pageSize = 3;

            IEnumerable<Student> students = db.Students
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            PageInfo pageInfo = new PageInfo 
            { 
                PageNumber = page, 
                PageSize = pageSize, 
                TotalItems = db.Students.Count() 
            };

            IndexViewModel indexView = new IndexViewModel
            {
                pageInfo = pageInfo,
                Students = students
            };

            return View(indexView);
        }

        public ActionResult TemplateExample()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            Student student = db.Students.Find(id);
            if (student != null)
            {
                return PartialView(student);
            }

            return HttpNotFound();
        }

        public ActionResult Edit(int? id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.Courses = db.Courses.ToList();

            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student, int[] selectedcourses)
        {
            Student new_student = db.Students.Find(student.Id);
            new_student.Name = student.Name;

            new_student.Courses.Clear();

            if (selectedcourses != null)
            {
                foreach (var i in db.Courses.Where(c => selectedcourses.Contains(c.Id)))
                {
                    new_student.Courses.Add(i);
                }
            }

            db.Entry(new_student).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Input()
        {
            return View();
        }

        [HttpPost]
        public string Input(List<string> names)
        {
            string fin = "";

            for (int i = 0; i < names.Count; i++)
            {
                fin += names[i] + "; ";
            }

            return fin;
        }

        [HttpGet]
        public ActionResult AddStudent()
        {
            return View(db.Students.ToList());
        }

        [HttpPost]
        public string AddStudent(List<Student> student)
        {
            string studentsNames = "";

            for (int i = 0; i < student.Count; i++)
            {
                studentsNames += student[i].Name.ToString() + " ";
            }

            return studentsNames;
        }

        [HttpGet]
        public ActionResult AddStudentV2()
        {
            Student firstStudent = db.Students
                .ToList()
                .FirstOrDefault();

            return View(db.Students.ToList());
        }

        [HttpPost]
        public string AddStudentV2(Student student, Student student1)
        {
            string studentsNames = "";

            /*for (int i = 0; i < student.Count; i++)
            {
                studentsNames += student[i].Name.ToString() + " ";
            }*/

            return studentsNames;
        }

        [HttpGet]
        public ActionResult AddStudentV3()
        {
            Student firstStudent = db.Students
                .ToList()
                .FirstOrDefault();

            return View(db.Students.ToList());
        }

        [HttpPost]
        public string AddStudentV3(Student student, Student student1)
        {
            string studentsNames = "";

            /*for (int i = 0; i < student.Count; i++)
            {
                studentsNames += student[i].Name.ToString() + " ";
            }*/

            return studentsNames;
        }

        [HttpGet]
        public ActionResult GetCourses()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetCourses(Course course)
        {
            return View();
        }

        public ActionResult FilterExample()
        {
            // IQueryable<Student> students = db.Students.Include(p => p.Courses);

            return View();
        }

        public ActionResult StudentText()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StudentSearch(string name)
        {
            var allStudents = db.Students.Where(c => c.Name.Contains(name)).ToList();

            if (allStudents.Count <= 0)
            {
                return HttpNotFound();
            }

            return PartialView(allStudents);
        }

        public ActionResult StudentName()
        {
            Student student = db.Students.FirstOrDefault();
            return PartialView(student);
        }

        public JsonResult JsonSearch(string studentName)
        {
            var jsonData = db.Students.Where(a => a.Name.Contains(studentName)).ToList();
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}