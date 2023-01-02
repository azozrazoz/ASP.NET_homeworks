using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        AccountContext db = new AccountContext();
        // GET: Account
        public ActionResult Index()
        {
            IEnumerable<Account> accounts = db.Accounts;
            ViewBag.Accounts = accounts;

            return View();
        }
    }
}