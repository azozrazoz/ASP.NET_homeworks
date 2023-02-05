using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Web.UI;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private LogContext logDb = new LogContext();
        private AccountContext db = new AccountContext();
        private PatientContext db_patients = new PatientContext();

        public ActionResult Logs()
        {
            return View(logDb.ExceptionDetails.ToList());
        }

        // GET: Admin
        public async Task<ActionResult> Index()
        {
            return View(await db.Accounts.ToListAsync());
        }

        [ExceptionFilter]
        public ActionResult Test(int id)
        {
            if (id > 3)
            {
                int[] arr = new int[2];
                arr[4] = 3;
            }
            else if (id < 3)
            {
                throw new Exception("id can not less than 3");
            }
            else
            {
                throw new Exception("Incorrect info");
            }

            return View();
        }

        public async Task<ActionResult> Patients(int page = 1)
        {
            int pageSize = 5;

            IEnumerable<Patient> patients = await db_patients.Patients
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = await db_patients.Patients.CountAsync()
            };

            PatientsViewModel indexView = new PatientsViewModel
            {
                pageInfo = pageInfo,
                Patients = patients
                
            };

            return View(indexView);
        }

        public async Task<ActionResult> AllUsers(int page = 1)
        {
            int pageSize = 10;   

            IEnumerable<Patient> patients = await db_patients.Patients
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            IEnumerable<Doctor> doctors = await db_patients.Doctors
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            List<Account> users = new List<Account>();

            foreach(var doctor in doctors)
            {
                users.Add(new Account 
                { 
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Gender = doctor.Gender,
                });
            }

            foreach (var patient in patients)
            {
                users.Add(new Account
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Gender = patient.Gender,
                });
            }

            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = await db_patients.Patients.CountAsync() + await db_patients.Doctors.CountAsync()
            };

            UsersViewModel indexView = new UsersViewModel
            {
                pageInfo = pageInfo,
                accounts = users
            };

            return View(indexView);
        }

        // GET: Admin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Email,Password,Gender,CreatedDate,IsAdmin")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Admin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Admin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Email,Password,Gender,CreatedDate,IsAdmin")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Admin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = await db.Accounts.FindAsync(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Account account = await db.Accounts.FindAsync(id);
            db.Accounts.Remove(account);
            await db.SaveChangesAsync();
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
    }
}
