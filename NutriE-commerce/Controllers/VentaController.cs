using NutriE_commerce.Datos;
using NutriE_commerce.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web.UI.WebControls;
using NutriE_commerce.Models.ViewModels;

namespace NutriE_commerce.Controllers
{
    public class VentaController : Controller
    {
        private nutriecommerceEntities8 db = new nutriecommerceEntities8();
        // GET: Venta
        int proID;
        public ActionResult Venta()
        {

            llenarDropDownList();

            return View();
        }
        public ActionResult Paso()
        {

            

            return View();
        }

        public void llenarDropDownList()
        {
            List<TablaViewModel> lst = null;
            using (Models.nutriecommerceEntities8 db = new Models.nutriecommerceEntities8())
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
        public ActionResult Venta(int proId, int cantidadVenta)
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

            double total;
            string fecha = DateTime.Now.ToString();


            if (reader.Read())
            {
                precio = (double)reader.GetDecimal(6);

                total = precio * cantidad;
                reader.Close();


                string sentencia1 = "INSERT INTO tblVenta (proId,fechaVenta,cantidadVenta,totalVenta) VALUES ('" + proId + "','" + fecha + "','" + cantidadVenta + "','" + total + "')";
                SqlCommand cmd1 = new SqlCommand(sentencia1, conn);
                cmd1.ExecuteNonQuery();

                conn.Close();

                return RedirectToAction("Paso","Venta");
            }
            else
            {
                return RedirectToAction("Venta", "Venta"); ;
            }
            conn.Close();
            return View();

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
    }
}