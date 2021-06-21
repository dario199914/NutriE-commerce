using Npgsql;
using System;
using System.Collections.Generic;
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
            NpgsqlConnection conn = new NpgsqlConnection();
            conn.ConnectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123456789;Database=Prueba";
                         string sentencia = "SELECT *FROM tbl_usuario WHERE usu_nombre='" + User + "' AND usu_pass='" + Pass + "'; ";
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conn);
            NpgsqlDataReader reader;
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