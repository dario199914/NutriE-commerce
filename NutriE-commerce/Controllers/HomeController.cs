using Npgsql;
using System.Web.Mvc;

namespace NutriE_commerce.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Producto(int cat_id, int prv_id, string pro_codigo, string pro_nombre, int pro_stock, double pro_precio)
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            conn.ConnectionString = "Server=localhost;Port=5432;User Id=postgres;Password=123456789;Database=Prueba";
            string sentencia = "INSERT INTO public.tbl_producto(cat_id,prv_id, pro_codigo,pro_nombre,pro_stock,pro_precio)	VALUES('" + cat_id + "', '" + prv_id + "', '" + pro_codigo + "','" + pro_nombre + "','" + pro_stock + "','" + pro_precio + "'); ";
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(sentencia, conn);
            cmd.ExecuteNonQuery();

            return View();


        }


    }
}