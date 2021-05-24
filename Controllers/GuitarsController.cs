using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GuitarBazar.Data;
using GuitarBazar.Models;

namespace GuitarBazar.Controllers
{
    public class GuitarsController : Controller
    {
        private GuitarBazarContext db = new GuitarBazarContext();

        public ActionResult Index()
        {
            db.log.Add((string)Session["ComputerName"], "Guitars/Index");

            return View(db.Guitars.ToList());
        }

        // GET: Guitars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guitar guitar = db.Guitars.Find(id);
            db.log.Add((string)Session["ComputerName"], "Guitars/Details: " + guitar.Model);

            if (guitar == null)
            {
                return HttpNotFound();
            }
            return View(guitar);
        }
        public ActionResult Test()
        {
            ViewBag.Sellers = db.SellersToSelectList();
            return View(new Guitar());
        }


        // GET: Guitars/Create
        public ActionResult Create() 
        {
            ViewBag.Aujourdhui = DateTime.Now;

            ViewBag.Sellers = db.SellersToSelectList();
            return View(new Guitar());
        }

        // POST: Guitars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "Seller")] Guitar guitar)
        {

            if (ModelState.IsValid)
            {
                db.log.Add((string)Session["ComputerName"], "Guitars/Create: " + guitar.Model);
                db.Guitars.Add(guitar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Sellers = new SelectList(db.Sellers, "Id", "Name", guitar.SellerId);
            return View(guitar);
        }

        // GET: Guitars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guitar guitar = db.Guitars.Find(id);
            if (guitar == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sellers = db.SellersToSelectList(true);
            return View(guitar);
        }

        // POST: Guitars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "Seller")] Guitar guitar)
        {
            if (ModelState.IsValid)
            {
                db.log.Add((string)Session["ComputerName"], "Guitars/Edit: " + guitar.Model);
                db.Entry(guitar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Sellers = db.SellersToSelectList(true);
            return View(guitar);
        }

        // GET: Guitars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guitar guitar = db.Guitars.Find(id);
            if (guitar == null)
            {
                return HttpNotFound();
            }
            return View(guitar);
        }

        // POST: Guitars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guitar guitar = db.Guitars.Find(id);
            db.log.Add((string)Session["ComputerName"], "Guitars/Delete: " + guitar.Model);
            db.Guitars.Remove(guitar);
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

        public ActionResult About()
        {
            return View();
        }

    }
}
