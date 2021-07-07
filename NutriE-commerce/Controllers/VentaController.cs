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
            // obtenerPrecio();
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
            string fecha = DateTime.Now.ToString();


            if (reader.Read())
            {
                precio = (double)reader.GetDecimal(6);
                proStock = reader.GetInt32(4);
                total = precio * cantidad;
                reader.Close();

                if (proStock < cantidadVenta)
                {
                    // se meustra un error 
                    Response.Write("<script language=javascript>alert('ERROR ');</script>");

                }
                else {
                    //ingresamos venta a la base de datos
                    string sentencia1 = "INSERT INTO tblVenta (proId,fechaVenta,cantidadVenta,totalVenta) VALUES ('" + proId + "','" + fecha + "','" + cantidadVenta + "','" + total + "')";
                    SqlCommand cmd1 = new SqlCommand(sentencia1, conn);
                    cmd1.ExecuteNonQuery();
                    // controlar el stock 


                    conn.Close();

                    return RedirectToAction("Index", "Venta");
                }

                
            }
            else
            {
                return RedirectToAction("Index", "Venta"); ;
            }
            conn.Close();
            return RedirectToAction("Index", "Venta");

        }


        public List<SelectListItem> ObtenerListado()
        {

            return new List<SelectListItem>() {
             new SelectListItem(){

             Text="sI",
             Value="1"}
            };

        }
        public DataSet Consulta(string strSql)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=DESKTOP-5FUIGH5\SQLEXPRESS;Initial Catalog=nutriecommerce;Integrated Security=True;";
            conn.Open();
            SqlCommand cmd = new SqlCommand(strSql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            return ds;
        }
        private void IniciarLlenarDropDrow()
        {

        }
        public ActionResult Create()
        {
            ViewBag.catId = new SelectList(db.tblProducto, "proId", "proNombre");
            return View();
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