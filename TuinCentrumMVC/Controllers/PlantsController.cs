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
    public class PlantsController : Controller
    {
        private MVCTuinCentrumEntities db = new MVCTuinCentrumEntities();

        // GET: Plants
        public ActionResult Index()
        {
            var plants = db.Plants.Include(p => p.Leverancier).Include(p => p.Soort);
            return View(plants.ToList());
        }

        // GET: Plants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // GET: Plants/Create
        public ActionResult Create()
        {
            ViewBag.Levnr = new SelectList(db.Leveranciers, "LevNr", "Naam");
            ViewBag.SoortNr = new SelectList(db.Soorts, "SoortNr", "Naam");
            return View();
        }

        // POST: Plants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlantNr,Naam,SoortNr,Levnr,Kleur,VerkoopPrijs")] Plant plant)
        {
            if (ModelState.IsValid)
            {
                db.Plants.Add(plant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Levnr = new SelectList(db.Leveranciers, "LevNr", "Naam", plant.Levnr);
            ViewBag.SoortNr = new SelectList(db.Soorts, "SoortNr", "Naam", plant.SoortNr);
            return View(plant);
        }

        // GET: Plants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            ViewBag.Levnr = new SelectList(db.Leveranciers, "LevNr", "Naam", plant.Levnr);
            ViewBag.SoortNr = new SelectList(db.Soorts, "SoortNr", "Naam", plant.SoortNr);
            return View(plant);
        }

        // POST: Plants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlantNr,Naam,SoortNr,Levnr,Kleur,VerkoopPrijs")] Plant plant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Levnr = new SelectList(db.Leveranciers, "LevNr", "Naam", plant.Levnr);
            ViewBag.SoortNr = new SelectList(db.Soorts, "SoortNr", "Naam", plant.SoortNr);
            return View(plant);
        }

        // GET: Plants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // POST: Plants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plant plant = db.Plants.Find(id);
            db.Plants.Remove(plant);
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
