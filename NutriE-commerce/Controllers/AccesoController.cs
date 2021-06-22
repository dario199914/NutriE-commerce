using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NutriE_commerce.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Login(string User, string Pass) 
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=DESKTOP-5FUIGH5\SQLEXPRESS;Initial Catalog=nutriecommerce;Integrated Security=True;";
                         string sentencia = "SELECT *FROM tblUsuario WHERE usuNombre='" + User + "' AND usuContra='" + Pass + "'; ";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sentencia, conn);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();

            string user;

            if (reader.Read())
            {
                user = reader.GetString(2);
                return RedirectToAction("Index","Home"); ;


            }
            else
            {
                return RedirectToAction("Login", "Acceso"); ;
            }
            conn.Close();
            return View();
        }
    }
}