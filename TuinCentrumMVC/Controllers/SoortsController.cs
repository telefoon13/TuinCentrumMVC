using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TuinCentrumMVC.Models;

namespace TuinCentrumMVC.Controllers
{
    public class SoortsController : Controller
    {
        private MVCTuinCentrumEntities db = new MVCTuinCentrumEntities();

        // GET: Soorts
        public ActionResult Index()
        {
            return View(db.Soorts.ToList());
        }

        // GET: Soorts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soort soort = db.Soorts.Find(id);
            if (soort == null)
            {
                return HttpNotFound();
            }
            return View(soort);
        }

        // GET: Soorts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Soorts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoortNr,Naam,MagazijnNr")] Soort soort)
        {
            if (ModelState.IsValid)
            {
                db.Soorts.Add(soort);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(soort);
        }

        // GET: Soorts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soort soort = db.Soorts.Find(id);
            if (soort == null)
            {
                return HttpNotFound();
            }
            return View(soort);
        }

        // POST: Soorts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoortNr,Naam,MagazijnNr")] Soort soort)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soort).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(soort);
        }

        // GET: Soorts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Soort soort = db.Soorts.Find(id);
            if (soort == null)
            {
                return HttpNotFound();
            }
            return View(soort);
        }

        // POST: Soorts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Soort soort = db.Soorts.Find(id);
            db.Soorts.Remove(soort);
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
