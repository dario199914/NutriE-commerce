using NutriE_commerce.Models;

namespace NutriE_commerce.Datos
{
    public class Venta
    {
        public void Guardar(tblVenta modelo)
        {
            using (nutriecommerceEntities11 contexto = new nutriecommerceEntities11())
            {
                contexto.tblVenta.Add(modelo);


            }
        }
    }
}