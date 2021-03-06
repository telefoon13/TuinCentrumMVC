﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TuinCentrumMVC.Models;
using TuinCentrumMVC.Filters;

namespace TuinCentrumMVC.Controllers
{
    //ActionFilter met Controller scope
    //[StatistiekActionFilter(Order = 3)]
    //GlobalFilter uitschakelen voor bepaalde Controller
    //[OverrideActionFilters]
    //Zelf error type en view kiezen controller of action scope
    //[HandleError(ExceptionType = typeof(EntityException), View = "DatabaseError")]
    public class PlantsController : Controller
    {
        private MVCTuinCentrumEntities db = new MVCTuinCentrumEntities();

        // GET: Plants
        //ActionFilter met Action scope
        //[StatistiekActionFilter(Order = 6)]
        //GlobalFilter uitschakelen voor bepaalde Action
        //[OverrideActionFilters]
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

        //Zelf geschreven

        public ViewResult UpLoaden(int id)
        {
            return View(id);
        }

        [HttpPost]
        public ActionResult FotoUploaden(int id)
        {
            if (Request.Files.Count > 0)
            {
                var foto = Request.Files[0];
                var absoluutPadNaarDir = this.HttpContext.Server.MapPath("~/Content/Images/Photos");
                var absoluutPadNaarPhoto = Path.Combine(absoluutPadNaarDir, id + ".jpg");
                foto.SaveAs(absoluutPadNaarPhoto);
            }
            return RedirectToAction("Index");
        }

        public ContentResult ImageOrDefault(int id)
        {
            var imagePath = "/Content/Images/Photos/" + id + ".jpg";
            var imageSrc = System.IO.File.Exists(HttpContext.Server.MapPath("~/" + imagePath)) ? imagePath : "/Content/Images/Photos/default.jpg";
            return Content(imageSrc);
        }

        public ActionResult FindPlantsBySoortNaam(string soortnaam)
        {
            List<Plant> plantenLijst = new List<Plant>();
            plantenLijst = (from plant in db.Plants.Include("Soort") where plant.Soort.Naam.StartsWith(soortnaam) select plant).ToList();
            return View(plantenLijst);
        }

        public ActionResult FindPlantenByLeverancier(int? levnr)
        {
            List<Plant> plantenLijst = new List<Plant>();
            plantenLijst = (from plant in db.Plants.Include("Leverancier") where plant.Leverancier.LevNr == levnr select plant).ToList();
            return View(plantenLijst);
        }

        public ActionResult FindPlantenBetweenPrijzen(decimal minPrijs, decimal maxPrijs)
        {
            List<Plant> plantenLijst = new List<Plant>();
            plantenLijst = (from plant in db.Plants where plant.VerkoopPrijs >= minPrijs && plant.VerkoopPrijs <= maxPrijs select plant).ToList();
            ViewBag.minprijs = minPrijs;
            ViewBag.maxprijs = maxPrijs;
            return View(plantenLijst);
        }

        public ActionResult FindPlantenVanEenKleur(string kleur)
        {
            List<Plant> plantenLijst = new List<Plant>();
            plantenLijst = (from plant in db.Plants where plant.Kleur == kleur select plant).ToList();
            ViewBag.kleur = kleur;
            return View(plantenLijst);
        }

        [Route("plantinfo/{id:int}")]
        public ActionResult FindPlantById(int id)
        {
            var plant = db.Plants.Find(id);
            if (plant != null)
            {
                return View("Details", plant);
            }
            else
            {
                var planten = db.Plants.Include(p => p.Leverancier).Include(p => p.Soort);
                return View("Index", planten.ToList());
            }
        }

        [Route("plantinfo/{naam}")]
        //[Route("plantinfo/{naam:alpha:maxlength(20)}")]
        public ActionResult FindPlantByName(string naam)
        {
            var plant = (from p in db.Plants where p.Naam == naam select p).FirstOrDefault();
            if (plant != null)
            {
                return View("Details", plant);
            }
            else
            {
                var planten = db.Plants.Include(p => p.Leverancier).Include(p => p.Soort);
                return View("Index", planten.ToList());
            }
        }

    }
}
