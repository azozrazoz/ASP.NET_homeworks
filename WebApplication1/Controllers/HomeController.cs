using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        MovieContext db = new MovieContext();

        public ActionResult Index()
        {
            IEnumerable<Movie> movies = db.Movies;
            ViewBag.Movies = movies;

            return View();
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.MovieId = id;

            return View();
        }

        [HttpPost]
        public string Buy(BuyTickets buyTickets)
        {
            buyTickets.Date = DateTime.Now;
            buyTickets.Price = 800;
            buyTickets.Email = "qwerty@gmail.com";
            buyTickets.Person = "John";

            db.Tickets.Add(buyTickets);
            db.SaveChanges();

            return "Thank you for purchasing our ticket" + buyTickets.Person + "!";
        }

        private DateTime getToday()
        {
            return DateTime.Now;
        }

        public string Square(int a, int h)
        {
            double s = a * h;

            return $"<h2>{ s }</h2>";
        }

        public ActionResult GetHtml()
        {
            return new HtmlResult("<h1>Heeey</h1>");
        }
    }
}