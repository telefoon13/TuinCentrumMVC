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
    public class LeveranciersController : Controller
    {
        private MVCTuinCentrumEntities db = new MVCTuinCentrumEntities();

        // GET: Leveranciers
        public ActionResult Index()
        {
            return View(db.Leveranciers.ToList());
        }

        // GET: Leveranciers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leverancier leverancier = db.Leveranciers.Find(id);
            if (leverancier == null)
            {
                return HttpNotFound();
            }
            return View(leverancier);
        }

        // GET: Leveranciers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Leveranciers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LevNr,Naam,Adres,PostNr,Woonplaats")] Leverancier leverancier)
        {
            if (ModelState.IsValid)
            {
                db.Leveranciers.Add(leverancier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leverancier);
        }

        // GET: Leveranciers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leverancier leverancier = db.Leveranciers.Find(id);
            if (leverancier == null)
            {
                return HttpNotFound();
            }
            return View(leverancier);
        }

        // POST: Leveranciers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LevNr,Naam,Adres,PostNr,Woonplaats")] Leverancier leverancier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leverancier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leverancier);
        }

        // GET: Leveranciers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leverancier leverancier = db.Leveranciers.Find(id);
            if (leverancier == null)
            {
                return HttpNotFound();
            }
            return View(leverancier);
        }

        // POST: Leveranciers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leverancier leverancier = db.Leveranciers.Find(id);
            db.Leveranciers.Remove(leverancier);
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
