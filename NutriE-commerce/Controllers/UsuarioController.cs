using NutriE_commerce.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NutriE_commerce.Controllers
{
    public class UsuarioController : Controller
    {
        private nutriecommerceEntities10 db = new nutriecommerceEntities10();

        // GET: Usuario
        public ActionResult Index()
        {
            var tblUsuario = db.tblUsuario.Include(t => t.tblRol);
            return View(tblUsuario.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            if (tblUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tblUsuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.rolId = new SelectList(db.tblRol, "rolId", "rolNombre");
            return View();
        }

        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "usuId,rolId,usuNombre,usuCedula,usuTelefono,usuCorreo,usuContra,usuEstado")] tblUsuario tblUsuario)
        {
            if (ModelState.IsValid)
            {
                db.tblUsuario.Add(tblUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.rolId = new SelectList(db.tblRol, "rolId", "rolNombre", tblUsuario.rolId);
            return View(tblUsuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            if (tblUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.rolId = new SelectList(db.tblRol, "rolId", "rolNombre", tblUsuario.rolId);
            return View(tblUsuario);
        }

        // POST: Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "usuId,rolId,usuNombre,usuCedula,usuTelefono,usuCorreo,usuContra,usuEstado")] tblUsuario tblUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rolId = new SelectList(db.tblRol, "rolId", "rolNombre", tblUsuario.rolId);
            return View(tblUsuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            if (tblUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tblUsuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUsuario tblUsuario = db.tblUsuario.Find(id);
            db.tblUsuario.Remove(tblUsuario);
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
