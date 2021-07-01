using NutriE_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutriE_commerce.Datos
{
    public class Producto
    {
        public List<tblProducto> Consultar()
        {
            using (nutriecommerceEntities8 contexto= new nutriecommerceEntities8())
            {
                return contexto.tblProducto.AsNoTracking().ToList();
            }
        }
    }
}