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
        public ActionResult Index(int id)
        {
            Account account = db.Accounts.Find(id);

            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }        
    }
}