using NutriE_commerce.Extensions;
using NutriE_commerce.Models;
using Rotativa;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace NutriE_commerce.Controllers
{
    public class ProductoController : Controller
    {
        private nutriecommerceEntities11 db = new nutriecommerceEntities11();

        // GET: Producto
        public ActionResult Index()
        {
            var tblProducto = db.tblProducto.Include(t => t.tblCategoria);
            return View(tblProducto.ToList());
        }

        // GET: Producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProducto tblProducto = db.tblProducto.Find(id);
            if (tblProducto == null)
            {
                return HttpNotFound();
            }
            return View(tblProducto);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.catId = new SelectList(db.tblCategoria, "catId", "catNombre");
            return View();
        }

        // POST: Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "proId,catId,proCodigo,proNombre,proStock,proDesc,proPrecio,proObser,proFecha,proImagen,proEstado")] tblProducto tblProducto)
        {
            HttpPostedFileBase FileBase = Request.Files[0];

            WebImage image = new WebImage(FileBase.InputStream);

            tblProducto.proImagen = image.GetBytes();


            if (ModelState.IsValid)
            {
                db.tblProducto.Add(tblProducto);
                db.SaveChanges();
                this.AddNotification("Producto Registrado..!!", NotificationType.INFO);
                return RedirectToAction("Index");
            }

            ViewBag.catId = new SelectList(db.tblCategoria, "catId", "catNombre", tblProducto.catId);
            return View(tblProducto);
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProducto tblProducto = db.tblProducto.Find(id);
            if (tblProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.catId = new SelectList(db.tblCategoria, "catId", "catNombre", tblProducto.catId);
            return View(tblProducto);
        }

        // POST: Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "proId,catId,proCodigo,proNombre,proStock,proDesc,proPrecio,proObser,proFecha,proEstado")] tblProducto tblProducto)
        {
            byte[] imagenActual = null;

            HttpPostedFileBase FileBase = Request.Files[0];
            if (FileBase == null)
            {
                imagenActual = db.tblProducto.SingleOrDefault(p => p.proId == tblProducto.proId).proImagen;
            }
            else
            {
                WebImage image = new WebImage(FileBase.InputStream);

                tblProducto.proImagen = image.GetBytes();
            }

            if (ModelState.IsValid)
            {
                db.Entry(tblProducto).State = EntityState.Modified;
                db.SaveChanges();
                this.AddNotification("Producto Editado..!!", NotificationType.INFO);
                return RedirectToAction("Index");
            }
            ViewBag.catId = new SelectList(db.tblCategoria, "catId", "catNombre", tblProducto.catId);
            return View(tblProducto);
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProducto tblProducto = db.tblProducto.Find(id);
            if (tblProducto == null)
            {
                return HttpNotFound();
            }
            return View(tblProducto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProducto tblProducto = db.tblProducto.Find(id);
            db.tblProducto.Remove(tblProducto);
            db.SaveChanges();
            this.AddNotification("Producto Eliminado..!!", NotificationType.INFO);
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

        public ActionResult getImagen(int id)
        {
            tblProducto tblProducto = db.tblProducto.Find(id);
            byte[] byteImagen = tblProducto.proImagen;

            MemoryStream memoryStream = new MemoryStream(byteImagen);
            Image imagen = Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            imagen.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
        }
        public ActionResult Report()
        {
            var tblProducto = db.tblProducto.Include(t => t.tblCategoria);
            return View(tblProducto.ToList());
        }
        public ActionResult Print()
        {
            return new ActionAsPdf("Report")
            { FileName = "test1.pdf" };
        }
    }
}
