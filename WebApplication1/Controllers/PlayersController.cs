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

namespace WebApplication1.Controllers
{
    public class PlayersController : Controller
    {
        private SoccerContext db = new SoccerContext();

        // GET: Players
        public async Task<ActionResult> Index()
        {
            var players = db.Players.Include(p => p.Team);
            return View(await players.ToListAsync());
        }

        [Authorize]
        // GET: Players/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = await db.Players.FindAsync(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name");
            return View();
        }

        // POST: Players/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Email,Password,PasswordConfirm,Age,Position,Check,TeamId")] Player player)
        {
            if (string.IsNullOrEmpty(player.Name))
            {
                ModelState.AddModelError("Name", "Incorrect name");
            }
            else if (player.Name.Length < 3)
            {
                ModelState.AddModelError("Name", "Incorrect length of name");
            }
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Non Valid";
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = await db.Players.FindAsync(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // POST: Players/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Email,Password,PasswordConfirm,Age,Position,Check,TeamId")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = await db.Players.FindAsync(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Player player = await db.Players.FindAsync(id);
            db.Players.Remove(player);
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

        public async Task<ActionResult> FilterExample(int? team, string position)
        {
            IQueryable<Player> players = db.Players.AsNoTracking().Include(p => p.Team);

            if (team != null && team != 0)
            {
                players = players.Where(p => p.TeamId == team);
            }

            if (string.IsNullOrEmpty(position) && position.Equals("All"))
            {
                players = players.Where(p => p.Position == position);
            }

            List<Team> teams = db.Teams.ToList();
            teams.Insert(0, new Team { Name = "All", Id = 0 });

            PlayersListViewModel playersListView = new PlayersListViewModel
            {
                Players = players.ToList(),
                Team = new SelectList(teams, "Id", "Name"),
                Position = new SelectList(new List<string>()
                {
                    "All",
                    "Forward",
                    "Midfielder",
                    "Defender",
                    "Goalkeeper",
                }),
            };

            return View(playersListView);
        }

        public ActionResult Menu()
        {
            List<MenuItem> menuItems = new List<MenuItem>();
            return PartialView(menuItems);
        }

        [HttpGet]
        public JsonResult Check(string check)
        {
            var result = !(check == "qwerty");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
