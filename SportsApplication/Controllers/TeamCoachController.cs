using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportsApplication;

namespace SportsApplication.Controllers
{
    [Authorize(Roles ="TeamCoach")]
    public class TeamCoachController : Controller
    {
        private IndiaDBEntities db = new IndiaDBEntities();

        // GET: TeamCoach
        public ActionResult Index()
        { 
            return View(db.Registrations.ToList());
        }

        // GET: TeamCoach/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: TeamCoach/Create
        public ActionResult Create()
        {
            List<string> list = new List<string>() { "Captain", "Player" };
            ViewBag.list = list;
            return View();
        }

        // POST: TeamCoach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Registration registration)
        {
            int a = db.Registrations.Count();

            if(a >= 16)
            {
                ModelState.AddModelError("","You can not enter more than 15 records.");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Registrations.Add(registration);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(registration);
        }

        // GET: TeamCoach/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: TeamCoach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,MatchPlayed,Contact,EmailId,DOB,Height,Weight,Password,UserType,Status")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(registration);
        }

        // GET: TeamCoach/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: TeamCoach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
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
