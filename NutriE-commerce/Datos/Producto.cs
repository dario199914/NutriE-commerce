using NutriE_commerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace NutriE_commerce.Datos
{
    public class Producto
    {
        public List<tblProducto> Consultar()
        {
            using (nutriecommerceEntities10 contexto = new nutriecommerceEntities10())
            {
                return contexto.tblProducto.AsNoTracking().ToList();
            }
        }
    }
}