using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Util;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        MovieContext db = new MovieContext();

        // Синхронный метод
        public ActionResult Index()
        {
            IEnumerable<Movie> movies = db.Movies;
            ViewBag.Movies = movies;
            ViewBag.Message = "This is a partial view";

            Session["name"] = "Tom";
            return View();
        }

        public async Task<ActionResult> MovieList()
        {
            IEnumerable<Movie> movies = await db.Movies.ToListAsync();  
            ViewBag.Movies = movies;

            return View();
        }

        public string GetSessionName()
        {
            var val = Session["name"];

            return val.ToString();
        }

        public ViewResult SomeMethod()
        {
            ViewData["Head"] = "Hi IT step!";
            ViewBag.Head = "Hi SDP-211!";
            return View("Index");
        }

        public RedirectResult SwipeLink()
        {
            // return Redirect("Home\\Index"); // Временная переадресация

            return RedirectPermanent("Home\\Index"); // Постоянная переадресация
        }

        public RedirectToRouteResult SwipelinkV2()
        {
            // return RedirectToRoute(new { controller = "Home", action= "Index" });

            return RedirectToAction("Buy", "Home", new {id = 2});
        }

        // Получение файла через путь
        public FileResult Getfile()
        {
            string file_path = Server.MapPath("~\\Files\\l298.pdf");
            string file_type = "application/pdf";
            string file_name = "I298.pdf";

            return File(file_path, file_type, file_name);
        }

        // Получение файла через байты
        public FileResult GetBytes()
        {
            string file_path = Server.MapPath("~\\Files\\l298.pdf");
            byte[] mas = System.IO.File.ReadAllBytes(file_path);
            string file_type = "application/pdf";
            string file_name = "I298.pdf";

            return File(mas, file_type, file_name);
        }

        // Получение файлы через поток
        public FileResult GetStreams()
        {
            string file_path = Server.MapPath("~\\Files\\l298.pdf");
            FileStream fs = new FileStream(file_path, FileMode.Open, FileAccess.Read);

            string file_type = "application/pdf";
            string file_name = "I298.pdf";

            return File(fs, file_type, file_name);
        }

        public string GetContext()
        {
            HttpContext.Response.Write("<h1>Welcome to HttpContext</h1>");

            string browser = HttpContext.Request.Browser.Browser;
            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;

            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;

            return $"<p>Browser: {browser}</p>" +
                $"<p>User-Agent: {user_agent}</p>" +
                $"<p>URL: {url}</p>" +
                $"<p>IP: {ip}</p>" +
                $"<p>Referrer: {referrer}</p>";
        }

        public void ContextResponse()
        {
            HttpContext.Response.Write("<h1>Welcome to HttpContext</h1>");
        }

        public string IsUser()
        {
            bool isAdmin = HttpContext.User.IsInRole("admin");
            bool isAuth = HttpContext.User.Identity.IsAuthenticated;
            string login = HttpContext.User.Identity.Name;


            if (isAuth)
            {
                if (isAdmin) { return "Hello admin!"; }

                return login;
            }

            return "dasvidaniya";
        }

        public string Cookies()
        {
            HttpContext.Response.Cookies["id"].Value = "az-01w";
            string id = HttpContext.Request.Cookies["id"].Value;

            return id;
        }

        public ActionResult Check(int age)
        {
            if (age < 21)
            {
                // return new HttpStatusCodeResult(404);
                return HttpNotFound();
            }
            return View();
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            /*if (id > 3)
            {
                return Redirect("Home\\Index");
            }*/

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

        public ActionResult GetImg()
        {
            string path = "~\\Images\\chelover_pauk.jpg";

            return new ImageResult(path);
        }

        public ActionResult Partial()
        {
            ViewBag.Message = "This is a partial view";

            return PartialView();
        }

        [HttpGet]
        public ActionResult EditMovie(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Movie movie = db.Movies.Find(id);

            if(movie != null)
            {
                return View(movie);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditMovie(Movie movie)
        {
            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateMovie(Movie movie)
        {
            db.Entry(movie).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteMovie(int movieId)
        {
            Movie movie = new Movie { Id = movieId };
            db.Entry(movie).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int movieId)
        {
            Movie movie = db.Movies.Find(movieId);
            if (movie != null)
            {
                db.Movies.Remove(movie);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        /*[HttpPost]
        public ActionResult Delete(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie != null)
            {
                db.Movies.Remove(movie);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }*/
    }
}