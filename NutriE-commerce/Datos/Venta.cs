using NutriE_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutriE_commerce.Datos
{
    public class Venta
    {
        public void Guardar(tblVenta modelo) 
        {
            using (nutriecommerceEntities8 contexto = new nutriecommerceEntities8())
            {
                contexto.tblVenta.Add(modelo);


            }
        }
    }
}