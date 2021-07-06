using NutriE_commerce.Models;

namespace NutriE_commerce.Datos
{
    public class Venta
    {
        public void Guardar(tblVenta modelo)
        {
            using (nutriecommerceEntities10 contexto = new nutriecommerceEntities10())
            {
                contexto.tblVenta.Add(modelo);


            }
        }
    }
}