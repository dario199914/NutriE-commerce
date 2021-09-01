using NutriE_commerce.Extensions;
using NutriE_commerce.Models;
using NutriE_commerce.Models.ViewModels;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace NutriE_commerce.Controllers
{
    public class VentaController : Controller
    {
        int pro_Id;
        private nutriecommerceEntities11 db = new nutriecommerceEntities11();
        // GET: Venta
        int proID;
        public ActionResult Venta()
        {

            llenarDropDownList();
            
            return View();
        }
        public ActionResult Index()
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=DESKTOP-5FUIGH5\SQLEXPRESS;Initial Catalog=nutriecommerce;Integrated Security=True;";

            DataTable dataTable = new DataTable();

            string sentencia = " SELECT P.proNombre,P.proPrecio, v.fechaVenta,v.cantidadVenta ,v.totalVenta FROM tblVenta V join tblProducto P on v.proId= p.proId";
            conn.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter(sentencia, conn);
            sqlDa.Fill(dataTable);

            return View(dataTable);
        }

        public void llenarDropDownList()
        {
            List<TablaViewModel> lst = null;
            using (Models.nutriecommerceEntities11 db = new Models.nutriecommerceEntities11())
            {
                lst = (from d in db.tblProducto
                       select new TablaViewModel
                       {
                           proId = d.proId,

                           proNombre = d.proNombre
                       }).ToList();

            }
            List<SelectListItem> items = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.proNombre.ToString(),
                    Value = d.proId.ToString(),
                    Selected = false
                };
            });

            ViewBag.items = items;



        }

        [HttpPost]
        public JsonResult obtenerPrecio(string nombreP)
        {
            var nombre = nombreP;
            return Json(nombre);
        }

        public void generarTabla(double total)
        {

            ViewBag.total = total;
        }

        [HttpPost]
        public ActionResult Venta(string proId, int cantidadVenta)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=DESKTOP-5FUIGH5\SQLEXPRESS;Initial Catalog=nutriecommerce;Integrated Security=True;";
            string sentencia = "SELECT *FROM tblProducto WHERE proId='" + proId + "'";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sentencia, conn);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();


            double precio;
            double cantidad = Convert.ToDouble(cantidadVenta);
            int proStock;
            double total;
            string fecha = DateTime.Now.ToString("yyyy/MM/dd");
           


            if (reader.Read())
            {
                precio = (double)reader.GetDecimal(6);
                proStock = reader.GetInt32(4);
                total = precio * cantidad;
                reader.Close();

                if (proStock < cantidadVenta)
                {
                    // se meustra un error 
                    this.AddNotification("Cantidad ingresada, excede a la cantidad del Stock del Producto..!!", NotificationType.INFO);
                    return RedirectToAction("Index", "Venta");
                }
                else {
                    //ingresamos venta a la base de datos
                    int nuevoStock = proStock - Convert.ToInt32( cantidadVenta);
                    string sentencia1 = "INSERT INTO tblVenta (proId,fechaVenta,cantidadVenta,totalVenta) VALUES ('" + proId + "','" + fecha + "','" + cantidadVenta + "','" + total + "')";
                    SqlCommand cmd1 = new SqlCommand(sentencia1, conn);
                    cmd1.ExecuteNonQuery();
                    // controlar el stock 

                    string sentencia2 = "UPDATE tblProducto SET proStock = '"+nuevoStock+"' WHERE proId='"+ proId + "';";
                    SqlCommand cmd2 = new SqlCommand(sentencia2, conn);
                    cmd2.ExecuteNonQuery();
                    conn.Close();

                    this.AddNotification("Venta Generada con Exito..!!", NotificationType.INFO);
                    return RedirectToAction("Index", "Venta");
                }

                
            }
            else
            {
                this.AddNotification("Ha ocurrido un error..!!", NotificationType.INFO);
                return RedirectToAction("Index", "Venta"); ;
            }
            conn.Close();
            return RedirectToAction("Index", "Venta");

        }


       
       
       
       
        public ActionResult Report()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=DESKTOP-5FUIGH5\SQLEXPRESS;Initial Catalog=nutriecommerce;Integrated Security=True;";

            DataTable dataTable = new DataTable();

            string sentencia = " SELECT P.proNombre,P.proPrecio, v.fechaVenta,v.cantidadVenta ,v.totalVenta FROM tblVenta V join tblProducto P on v.proId= p.proId";
            conn.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter(sentencia, conn);
            sqlDa.Fill(dataTable);

            return View(dataTable);
        }
        public ActionResult Print()
        {
            return new ActionAsPdf("Report")
            { FileName = "Reporte.pdf" };
        }
    }
}