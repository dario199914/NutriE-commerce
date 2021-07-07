using NutriE_commerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace NutriE_commerce.Datos
{
    public class Producto
    {
        public List<tblProducto> Consultar()
        {
            using (nutriecommerceEntities11 contexto = new nutriecommerceEntities11())
            {
                return contexto.tblProducto.AsNoTracking().ToList();
            }
        }
    }
}