//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NutriE_commerce.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblUsuario
    {
        public int usuId { get; set; }
        public Nullable<int> rolId { get; set; }
        public string usuNombre { get; set; }
        public Nullable<int> usuCedula { get; set; }
        public string usuTelefono { get; set; }
        public string usuCorreo { get; set; }
        public string usuContra { get; set; }
        public string usuEstado { get; set; }
    
        public virtual tblRol tblRol { get; set; }
    }
}
