using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NutriE_commerce.Models
{
    public class productoVentaModel
    {
        public tblVenta Venta { get; set; }
        public IEnumerable<SelectListItem> ListadoProductos { get; set; }
    }
}