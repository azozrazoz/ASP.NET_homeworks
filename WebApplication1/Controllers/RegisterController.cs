using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegisterController : Controller
    {
        AccountContext db = new AccountContext();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register(int id)
        {
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        public ActionResult Register(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Register(Patient account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}