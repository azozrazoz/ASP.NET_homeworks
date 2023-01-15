using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        MovieContext db = new MovieContext();
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            SelectList movies = new SelectList(db.Movies, "Author", "Title");
            ViewBag.Movies = movies;
            return View();
        }

        [HttpPost]
        public string Index(string[] countries)
        {
            string result = "";
            foreach(string country in countries)
            {
                result += country + "\n";
            }
            return "Your choce: " + result;
        }

        public ActionResult TwoButtons(string action)
        {
            if(action == "Add")
            {

            }
            else if (action == "Delete")
            {

            }
            return View();
        }
    }
}