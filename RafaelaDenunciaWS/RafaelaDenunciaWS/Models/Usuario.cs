using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RafaelaDenunciaWS.Models
{
    public class Usuario 
    {
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidoUsuario { get; set; }
        public string mail { get; set; }
        public string contraseña { get; set; }
        public string direccion { get; set; }
        public string fecha { get; set; }
        
    }
}