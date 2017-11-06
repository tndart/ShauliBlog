using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShauliBlog.DAL;
using ShauliBlog.Models;
using ShauliBlog.Code;

namespace ShauliBlog.Controllers
{
    public class FansClubController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: FansClub
        public ActionResult Index()
        {
            ViewBag.selected = "Fans";
            return View(db.Fans.ToList());
        }

        // Post: Search Fan
        public ActionResult FansByCountry()
        {
            var fansByCountry = db.Fans
                   .GroupBy(f => f.Country)
                   .Select(g => new { name = g.Key, count = g.Count() });

            List<FansByCountry> fanbcountry = new List<FansByCountry>();

            foreach (var item in fansByCountry)
            {
                fanbcountry.Add(new FansByCountry(item.name,item.count));
            }

            ViewBag.MyList = fanbcountry;
            return View("FansByCountry",fanbcountry);
        }

        // Post: Search Fan
        public ActionResult SearchFanByCountry(string Country)
        {
            // Query the all Fans
            IQueryable<Fan> filteredFans = db.Fans.AsQueryable();

            // Query by parameters
            if (Country != "")
                filteredFans = filteredFans.Where(m => m.Country.Contains(Country));

            return View("Index", filteredFans);
        }

        // Post: Search Fan
        public ActionResult SearchFan(string Name, string Country, DateTime? Birthday)
        {
            // Query the all Fans
            IQueryable<Fan> filteredFans = db.Fans.AsQueryable();

            // Query by parameters
            if (Name != "")
                filteredFans = filteredFans.Where(m => m.FirstName.Contains(Name) || (m.LastName.Contains(Name)));
            if (Country != "")
                filteredFans = filteredFans.Where(m => m.Country.Contains(Country));
            if (Birthday != null)
                filteredFans = filteredFans.Where(m => m.Birthday > Birthday);

            return View("Index", filteredFans);
        }

        // GET: FansClub/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // GET: FansClub/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FansClub/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Gender,Birthday,Pazam,Username,Password")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Fans.Add(fan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fan);
        }

        // GET: FansClub/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: FansClub/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Gender,Birthday,Pazam")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fan);
        }

        // GET: FansClub/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                return HttpNotFound();
            }
            return View(fan);
        }

        // POST: FansClub/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fan fan = db.Fans.Find(id);
            db.Fans.Remove(fan);
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
    }
}
