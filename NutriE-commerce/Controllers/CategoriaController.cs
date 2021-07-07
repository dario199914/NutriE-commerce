using NutriE_commerce.Extensions;
using NutriE_commerce.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NutriE_commerce.Controllers
{
    public class CategoriaController : Controller
    {
        private nutriecommerceEntities11 db = new nutriecommerceEntities11();

        // GET: Categoria
        public ActionResult Index()
        {
          
            return View(db.tblCategoria.ToList());
        }

        // GET: Categoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoria tblCategoria = db.tblCategoria.Find(id);
            if (tblCategoria == null)
            {
                return HttpNotFound();
            }
            
            return View(tblCategoria);
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
           

            return View();
        }

        // POST: Categoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "catId,catNombre,catDesc,catEstado")] tblCategoria tblCategoria)
        {
            if (ModelState.IsValid)
            {
                db.tblCategoria.Add(tblCategoria);
                db.SaveChanges();
                this.AddNotification("Categoria Registrada..!!", NotificationType.INFO);
                return RedirectToAction("Index");
            }
           
            return View(tblCategoria);
        }

        // GET: Categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoria tblCategoria = db.tblCategoria.Find(id);
            if (tblCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tblCategoria);
        }

        // POST: Categoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "catId,catNombre,catDesc,catEstado")] tblCategoria tblCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCategoria).State = EntityState.Modified;
                db.SaveChanges();
                this.AddNotification("Categoria Editada..!!", NotificationType.INFO);
                return RedirectToAction("Index");
            }
            return View(tblCategoria);
        }

        // GET: Categoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCategoria tblCategoria = db.tblCategoria.Find(id);
            if (tblCategoria == null)
            {
                return HttpNotFound();
            }
            return View(tblCategoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCategoria tblCategoria = db.tblCategoria.Find(id);
            db.tblCategoria.Remove(tblCategoria);
            db.SaveChanges();
            this.AddNotification("Categoria Eliminada..!!", NotificationType.INFO);
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
