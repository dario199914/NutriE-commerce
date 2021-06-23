using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NutriE_commerce.Models;

namespace NutriE_commerce.Controllers
{
    public class VentaController : Controller
    {
        private nutriecommerceEntities1 db = new nutriecommerceEntities1();

        // GET: Venta
        public ActionResult Index()
        {
            var tblVenta = db.tblVenta.Include(t => t.tblProducto);
            return View(tblVenta.ToList());
        }

        // GET: Venta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVenta tblVenta = db.tblVenta.Find(id);
            if (tblVenta == null)
            {
                return HttpNotFound();
            }
            return View(tblVenta);
        }

        // GET: Venta/Create
        public ActionResult Create()
        {
            ViewBag.proId = new SelectList(db.tblProducto, "proId", "proCodigo");
            return View();
        }

        // POST: Venta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVenta,proId,fechaVenta,cantidadVenta")] tblVenta tblVenta)
        {
            if (ModelState.IsValid)
            {
                db.tblVenta.Add(tblVenta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.proId = new SelectList(db.tblProducto, "proId", "proCodigo", tblVenta.proId);
            return View(tblVenta);
        }

        // GET: Venta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVenta tblVenta = db.tblVenta.Find(id);
            if (tblVenta == null)
            {
                return HttpNotFound();
            }
            ViewBag.proId = new SelectList(db.tblProducto, "proId", "proCodigo", tblVenta.proId);
            return View(tblVenta);
        }

        // POST: Venta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVenta,proId,fechaVenta,cantidadVenta")] tblVenta tblVenta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblVenta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.proId = new SelectList(db.tblProducto, "proId", "proCodigo", tblVenta.proId);
            return View(tblVenta);
        }

        // GET: Venta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVenta tblVenta = db.tblVenta.Find(id);
            if (tblVenta == null)
            {
                return HttpNotFound();
            }
            return View(tblVenta);
        }

        // POST: Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblVenta tblVenta = db.tblVenta.Find(id);
            db.tblVenta.Remove(tblVenta);
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
