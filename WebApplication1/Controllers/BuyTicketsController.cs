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
    public class BuyTicketsController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: BuyTickets
        public async Task<ActionResult> Index()
        {
            return View(await db.Tickets.ToListAsync());
        }

        // GET: BuyTickets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyTickets buyTickets = await db.Tickets.FindAsync(id);
            if (buyTickets == null)
            {
                return HttpNotFound();
            }
            return View(buyTickets);
        }

        // GET: BuyTickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BuyTickets/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Email,Person,Price,Date")] BuyTickets buyTickets)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(buyTickets);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(buyTickets);
        }

        // GET: BuyTickets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyTickets buyTickets = await db.Tickets.FindAsync(id);
            if (buyTickets == null)
            {
                return HttpNotFound();
            }
            return View(buyTickets);
        }

        // POST: BuyTickets/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,Person,Price,Date")] BuyTickets buyTickets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buyTickets).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(buyTickets);
        }

        // GET: BuyTickets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyTickets buyTickets = await db.Tickets.FindAsync(id);
            if (buyTickets == null)
            {
                return HttpNotFound();
            }
            return View(buyTickets);
        }

        // POST: BuyTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BuyTickets buyTickets = await db.Tickets.FindAsync(id);
            db.Tickets.Remove(buyTickets);
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
